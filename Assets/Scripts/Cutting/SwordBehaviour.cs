using System.Collections;
using System.Collections.Generic;
using MeshCutting;
using UnityEngine;
using globalInfo;
using UnityEngine.UI;


public class SwordBehaviour : MonoBehaviour
{

    public Material cubeMaterial;
    public Material missileMaterial;
    private Material capMaterial;
    public GameObject remains;

    public AudioClip swordSound;
    public AudioClip cutSound;

    private Rigidbody rb;

    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;
    private System.Random alea;
    string message = "0";

    Quaternion rotationLast; //The value of the rotation at the previous update

    // Use this for initialization
    void Start()
    {
        alea = new System.Random();
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        this.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
    }

    // Update is called once per frame
    void OnTriggerExit(Collider collision)
    {
        capMaterial = collision.gameObject.GetComponent<Renderer>().material;

        /* Cut in half */
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Plane")
        {
            /*
            if (collision.gameObject.tag == "Restart")
            {
                // Restart spawn
                GameObject.Find("HeadTrack").GetComponent<Spawn>().enabled = true;
                GameObject.Find("HeadTrack").GetComponent<Spawn>().spawnTime = 80f;
            
                // Reset lives
                GameObject.Find("HeadTrack").GetComponent<Gameur>().lifePoints = 3;

                // Reset scene manager
                GameObject.Find("SManager").GetComponent<SceneManager>().score = -1;
            }
             */

            int rng = alea.Next(0, 2);
            if (rng == 0)
                source.PlayOneShot(cutSound, 0.9f);
            else
                source.PlayOneShot(swordSound, 0.9f);

            message = collision.gameObject.ToString();
            GameObject victim = collision.gameObject;
            GameObject[] pieces = MeshCut.Cut(victim, transform.position, transform.right, capMaterial);

            // Add rigidbody to the newly created piece
            if (!pieces[1].GetComponent<Rigidbody>())
                pieces[1].AddComponent<Rigidbody>();

            // Add gravity and remove collider from old gameobject
            pieces[0].GetComponent<Rigidbody>().useGravity = true;
            Destroy(collision);

            // Add forces to the pieces
            for (int i = 0; i < 2; i++)
            {
                Vector3 force = (i == 0 ? 5*transform.right : -5*transform.right);
                //Vector3 force = new Vector3((i == 0 ? -30 : 40), -5, 15);
                pieces[i].GetComponent<Rigidbody>().AddForce(force);
                //pieces[i].tag = "";
            }

            Destroy(pieces[1], 1);
            Destroy(pieces[0], 1);

            if (GameObject.Find("HeadTrack").GetComponent<Spawn>().enabled) //if fruits still spawning, update score
            {
                message = (++SceneManager.Instance.score).ToString();
                Text t = GameObject.Find("SCORE").GetComponent<Text>();
                t.text = message;
                Canvas.ForceUpdateCanvases();

            }
          
        }
    }
}
