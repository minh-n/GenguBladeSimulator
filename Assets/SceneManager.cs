using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace globalInfo {

    public class SceneManager : MonoBehaviour
    {
        // Declare any public variables that you want to be able 
        // to access throughout your scene
        public Gameur player;
        public static SceneManager Instance { get; private set; } // static singleton
        public int score = 0;
        //public GameObject GOPanel;
        //public Text gameOverText;

        void Awake()
        {
            if (Instance == null) { Instance = this; }
            else { Destroy(gameObject); }
            // Cache references to all desired variables
            player = FindObjectOfType<Gameur>();
            //GOPanel.SetActive(false);
        }
    }
}
