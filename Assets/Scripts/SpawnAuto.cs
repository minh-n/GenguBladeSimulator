using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAuto : MonoBehaviour {

    public List<GameObject> enemies;
	public float spawnTime = 80f;            // How long between each spawn.
	public int time = 0;
	public int countLevel = 0;
    private System.Random rng;

	// Use this for initialization
	void Start () {
        rng = new System.Random();


	}

    void FixedUpdate ()
    {
		if (++time % spawnTime == 0) {
			Vector3 position = new Vector3(Random.Range(-10.0f, 10.0f), 3f, Random.Range(10.0f, 20.0f));
			//Instantiate(cube, position, Quaternion.identity);
			int x = rng.Next(-2, 3);
			CreateCube(position, new Vector3(x , (float) this.transform.position.y ,-17f));

			if (++countLevel == 10) {
				if (spawnTime >= 20) {
					countLevel = 0;
					spawnTime -= 1f;
				}
			}
		}
    }

    public GameObject CreateCube(Vector3 position, Vector3 target) {
		int x = rng.Next(0, 3);
		GameObject newCube = Instantiate(enemies[rng.Next(0, 15)], position, Quaternion.identity) as GameObject;
        newCube.SendMessage("SetTarget", target);

        return newCube;
    }
}
