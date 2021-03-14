using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StyledMenu : MonoBehaviour
    {
        [Header("Menu Elements")] public new GameObject camera;
        public GameObject menu;
        public Button playButton;
        public Button exitButton;

        [Header("Player")] public GameObject playerStatus;
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
        // 4. show the player status
        // 5. enable the craft system
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