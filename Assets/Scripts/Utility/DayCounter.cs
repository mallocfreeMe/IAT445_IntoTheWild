using System;
using UnityEngine;
using UnityEngine.UI;

namespace Utility
{
    public class DayCounter : MonoBehaviour
    {
        public bool isNight;
        public GameObject dayCounter;
        public Material dayMaterial;
        public Material nightMaterial;
        public int dayCountDown;

        private float _time;
        private bool _oneDayPass;

        private void Start()
        {
            RenderSettings.skybox = dayMaterial;
        }

        private void Update()
        {
            // rotate the direction light based on the delta time, this snippet of code was taken from, and I made some modifications
            // https://answers.unity.com/questions/1675917/count-days-using-in-game-time-cycle-count-light-ro.html
            transform.RotateAround(Vector3.zero, Vector3.right, Time.deltaTime * 4);
            transform.LookAt(Vector3.zero);
            _time += Time.deltaTime * 4;
            var day = _time / 360;

            // only keep the digits for the day variable, and compare it with 0.5
            // to decide which skybox material to use
            var result = day - Math.Truncate(day);
            if (result > 0.5)
            {
                isNight = true;
                if (!_oneDayPass)
                {
                    dayCountDown++;
                    _oneDayPass = true;
                }

                RenderSettings.skybox = nightMaterial;
                foreach (Transform child in gameObject.transform)
                {
                    child.gameObject.GetComponent<Light>().intensity = 0.2f;
                }
            }
            else
            {
                if (_oneDayPass)
                {
                    dayCounter.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Day " + (3 - dayCountDown);
                    _oneDayPass = false;
                }

                isNight = false;
                RenderSettings.skybox = dayMaterial;
                foreach (Transform child in gameObject.transform)
                {
                    child.gameObject.GetComponent<Light>().intensity = 1f;
                }
            }
        }
    }
}