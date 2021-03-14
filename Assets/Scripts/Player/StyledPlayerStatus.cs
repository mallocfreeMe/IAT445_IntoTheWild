using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

namespace Player
{
    public class StyledPlayerStatus : MonoBehaviour
    {
        public Image hungerFill, waterFill, healthFill;
        public GameObject gameOverUI;
        public new GameObject camera;

        private int _startTime, _currentTime;

        private void Start()
        {
            _startTime = DateTime.Now.Minute;
        }

        private void Update()
        {
            CheckTimePass();
            CheckHealth();
            CheckGameOver();
        }

        private void CheckTimePass()
        {
            _currentTime = DateTime.Now.Minute;
            if (_currentTime > _startTime)
            {
                hungerFill.fillAmount -= 0.5f;
                waterFill.fillAmount -= 0.5f;
                _startTime = _currentTime;
            }
        }

        private void CheckHealth()
        {
            if (hungerFill.fillAmount == 0 | waterFill.fillAmount == 0)
            {
                healthFill.fillAmount -= 0.5f;
            }
        }

        private void CheckGameOver()
        {
            if (healthFill.fillAmount == 0)
            {
                gameOverUI.SetActive(true);
                camera.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                gameObject.SetActive(false);
            }
        }
    }
}