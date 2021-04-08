using System;
using Player;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using UnityEngine.SceneManagement;

namespace UI
{
    public class SkipMenu : MonoBehaviour
    {
        [Header("Menu Elements")] public new GameObject camera;
        public GameObject menu;
        public Button playButton;
        public Button exitButton;
        public GameObject tutorial;
        public int LoadingDelay = 500;
        bool counter = false;
        [SerializeField] public string sceneName;
        //public GameObject cross;
        // public Pause pausePanel;
        //public GameObject inventory;

        //[Header("Player")] public GameObject playerStatus;
        //public GameObject player;

        private void Start()
        {
            playButton.onClick.AddListener(Play);
            exitButton.onClick.AddListener(Exit);
            Debug.Log("Loading Scene: " + sceneName);
        }

        // the play button click event
        // 1. hide the menu
        // 2. disable the main camera
        // 3. enable the player
        // 4. enable the player status
        // 5. enable the craft system
        // 6. enable the inventory system
        private void Play()
        {
            // menu.SetActive(false);
            //camera.SetActive(false);
            //player.SetActive(true);
            //playerStatus.SetActive(true);
            //gameObject.GetComponent<CraftRecipe>().enabled = true;
            //gameObject.GetComponent<Inventory>().enabled = true;
            //StaticMethods.HideCursor();
            //cross.SetActive(true);
            //pausePanel.enabled = true;
            //inventory.SetActive(true);
            tutorial.SetActive(true);
            counter = true;
            menu.SetActive(false);

        }

        private void Update()
        {

            if (counter == true)
            {
                Debug.Log(LoadingDelay);
               LoadingDelay -= 1;
            }

            if (LoadingDelay <= 0)
            {
                counter = false;
                Debug.Log("Loading Scene: " + sceneName);
                SceneManager.LoadScene(sceneName);
            }
        }

        // the exit button click event
        // quit the application 
        private static void Exit()
        {
            Application.Quit();
        }
    }
}