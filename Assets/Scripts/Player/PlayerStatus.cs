using System;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerStatus : MonoBehaviour
    {
        public Image hungerFill, waterFill, healthFill;

        private int _startTime, _currentTime;

        private void Start()
        {
            _startTime = DateTime.Now.Minute;
        }

        private void Update()
        {
            _currentTime = DateTime.Now.Minute;
            if (_currentTime > _startTime)
            {
                hungerFill.fillAmount -= 0.5f;
                waterFill.fillAmount -= 0.5f;
                _startTime = _currentTime;
            }
        }
    }
}