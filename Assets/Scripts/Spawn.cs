using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public List<GameObject> enemies;
    public float spawnTime = 80f;            // How long between each spawn.
    private int time = 0;
    public int countLevel = 0;
    private System.Random rng;
    private int listSize = 0;
    private bool isSpawning = true;

    void DeactivateSpawn()
    {
        this.isSpawning = false;
    }

    // Use this for initialization
    void Start()
    {
        rng = new System.Random();
        listSize = enemies.Count;
    }

    void FixedUpdate()
    {
        if (isSpawning)
        {
            if (++time % spawnTime == 0)
            {
                Vector3 position = new Vector3(Random.Range(-5.0f, 3.0f), (float)this.transform.position.y, -10);
                //int x = rng.Next(-1, 1);
                CreateCube(position, new Vector3((float)this.transform.position.x, (float)this.transform.position.y, (float)this.transform.position.z)); // - ((float)this.transform.position.y / 10f)

                if (++countLevel == 3)
                {
                    if (spawnTime >= 20)
                    {
                        countLevel = 0;
                        spawnTime -= 1f;
                    }
                }
            }
        }
    }

    public GameObject CreateCube(Vector3 position, Vector3 target)
    {
        int x = rng.Next(0, 3);
        GameObject newCube = Instantiate(enemies[rng.Next(0, listSize)], position, Quaternion.identity) as GameObject;
        newCube.SendMessage("SetTarget", target);

        return newCube;
    }
}
