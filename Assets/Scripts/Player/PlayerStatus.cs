using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using Utility;

namespace Player
{
    public class PlayerStatus : MonoBehaviour
    {
        public Image hungerFill, waterFill, healthFill;
        public GameObject gameOverUI;
        public GameObject playerStatusUI;
        public GameObject playerInventoryUI;
        public GameObject playerCraftingUI;
        public GameObject cursor;
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
                hungerFill.fillAmount -= 0.2f;
                waterFill.fillAmount -= 0.2f;
                _startTime = _currentTime;
            }
            else if (_currentTime < _startTime)
            {
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
                StaticMethods.ShowCursor();
                cursor.SetActive(false);
                gameObject.SetActive(false);
                playerStatusUI.SetActive(false);
                playerInventoryUI.SetActive(false);
                playerCraftingUI.SetActive(false);
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Water"))
            {
                waterFill.fillAmount += 0.1f;
            }
        }
    }
}