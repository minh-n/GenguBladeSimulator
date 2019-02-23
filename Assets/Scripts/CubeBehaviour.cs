using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using globalInfo;
using UnityEngine.UI;

public class CubeBehaviour : MonoBehaviour {

    public bool activated;
    public float moveSpeed;
    public Vector3 target;
    private System.Random rng;
    private Vector3 vectorRotation;
    private AudioSource source;
    public AudioClip hitSound;

    private float minDist = 0.2f;

    // Use this for initialization
    void Start() {
        rng = new System.Random();
        vectorRotation = new Vector3(rng.Next(0, 360), rng.Next(0, 360), rng.Next(0, 360));
        source = GetComponent<AudioSource>();
    }

    string message = "";

    void OnTriggerEnter(Collider otherObjective)
    {
       if (this.tag == "Food" && otherObjective.tag == "Player")
       {
            source.PlayOneShot(hitSound, 1.2f);
            if (--SceneManager.Instance.player.lifePoints == 0)
            {
                message = "";

                GameOver[] gameover = Resources.FindObjectsOfTypeAll<GameOver>();
                if (gameover.Length > 0)
                    gameover[0].gameObject.SetActive(true);

                GameObject.Find("HeadTrack").GetComponent<Spawn>().enabled = false;

                //SceneManager.Instance.GOPanel.SetActive(true);
                //SceneManager.Instance.gameOverText.text = "GAME OVER";
            }
            else
            {
                message = (SceneManager.Instance.player.lifePoints == 3 ? "III" : SceneManager.Instance.player.lifePoints == 2 ? "II" : "I");
            }
            Text t = GameObject.Find("LIFE").GetComponent<Text>();
            t.text = message;
            Canvas.ForceUpdateCanvases();
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 200, 20), message);
    }

    public void SetTarget(Vector3 target) {
        this.target = target;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (activated) {
            transform.LookAt(target);

            if (Vector3.Distance(transform.position, target) >= minDist) {
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
                transform.Rotate(vectorRotation);
            }
            else {
                message = "";
                Destroy(gameObject);
            }
        }
    }
}
