using System;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Menu : MonoBehaviour
    {
        [Header("Menu Elements")] public new GameObject camera;
        public GameObject menu;
        public Button playButton;
        public Button exitButton;

        [Header("Player")] public GameObject playerStatus;
        public GameObject inventory;
        public GameObject player;

        private void Start()
        {
            playButton.onClick.AddListener(Play);
            exitButton.onClick.AddListener(Exit);
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
            menu.SetActive(false);
            camera.SetActive(false);
            player.SetActive(true);
            playerStatus.SetActive(true);
            gameObject.GetComponent<CraftRecipe>().enabled = true;
        }

        // the exit button click event
        // quit the application 
        private static void Exit()
        {
            Application.Quit();
        }
    }
}