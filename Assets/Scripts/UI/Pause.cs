using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utility;

namespace UI
{
    public class Pause : MonoBehaviour
    {
        public GameObject cross;
            
        private Button continueButton;
        private Button quitButton;

        private void Start()
        {
            continueButton = transform.GetChild(0).GetChild(1).gameObject.GetComponent<Button>();
            quitButton = transform.GetChild(0).GetChild(2).gameObject.GetComponent<Button>();
            continueButton.onClick.AddListener(Continue);
            quitButton.onClick.AddListener(Quit);
        }

        private void Continue()
        {
            transform.GetChild(0).gameObject.SetActive(false);
            StaticMethods.HideCursor();
            cross.SetActive(true);
        }

        private void Quit()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!transform.GetChild(0).gameObject.activeSelf)
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                    StaticMethods.ShowCursor();
                    cross.SetActive(false);
                }
                else
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    StaticMethods.HideCursor();
                    cross.SetActive(true);
                }
            }
        }
    }
}
