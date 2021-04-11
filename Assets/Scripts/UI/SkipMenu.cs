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
        public int loadingDelay = 500;
        public bool counter;
        [SerializeField] public string sceneName;

        private void Start()
        {
            playButton.onClick.AddListener(Play);
            exitButton.onClick.AddListener(Exit);
        }

        // the play button click event
        private void Play()
        {
            tutorial.SetActive(true);
            counter = true;
            menu.SetActive(false);
        }

        private void Update()
        {

            if (counter)
            {
                loadingDelay -= 1;
            }

            if (loadingDelay <= 0)
            {
                counter = false;
                SceneManager.LoadScene(sceneName);
            }
        }
        
        private static void Exit()
        {
            Application.Quit();
        }
    }
}