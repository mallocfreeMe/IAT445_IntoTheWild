using System;
using System.Collections;
using TMPro;
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
        public DayCounter dayCounter;
        public GameObject cursor;
        public new GameObject camera;
        public bool closeToCampFire;

        private int _startTime, _currentTime;

        private void Start()
        {
            _startTime = DateTime.Now.Minute;
        }

        private void Update()
        {
            CheckTimePass();
            CheckGameOver();
        }

        private void CheckTimePass()
        {
            _currentTime = DateTime.Now.Minute;
            if (_currentTime > _startTime)
            {
                if (!dayCounter.isNight)
                {
                    if (hungerFill.fillAmount == 0 || waterFill.fillAmount == 0)
                    {
                        healthFill.fillAmount -= 0.2f;
                    }
                    else
                    {
                        hungerFill.fillAmount -= 0.3f;
                        waterFill.fillAmount -= 0.1f;
                    }
                }
                else
                {
                    if (!closeToCampFire)
                    {
                        healthFill.fillAmount -= 0.2f;
                    }
                    else
                    {
                        if (hungerFill.fillAmount == 0 || waterFill.fillAmount == 0)
                        {
                            healthFill.fillAmount -= 0.2f;
                        }
                        else
                        {
                            hungerFill.fillAmount -= 0.3f;
                            waterFill.fillAmount -= 0.1f;
                        }
                    }
                }

                _startTime = _currentTime;
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

            if (dayCounter.dayCountDown == 3 && !dayCounter.isNight)
            {
                gameOverUI.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text =
                    "You finish the game";
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
                healthFill.fillAmount -= 0.1f;
            }
        }
    }
}