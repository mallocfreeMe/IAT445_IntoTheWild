using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class StyledGameOver : MonoBehaviour
    {
        public GameObject gameOverUI;
        public Button restartButton;
        public Button exitButton;

        private void Start()
        {
            restartButton.onClick.AddListener(Restart);
            exitButton.onClick.AddListener(Exit);
        }

        // the restart button click event
        // 1. hide the game over UI
        // 2. reload the current scene
        private void Restart()
        {
            gameOverUI.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // the exit button click event
        // quit the application 
        private static void Exit()
        {
            Application.Quit();
        }
    }
}