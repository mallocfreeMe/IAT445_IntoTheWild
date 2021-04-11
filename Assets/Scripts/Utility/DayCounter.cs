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
        [Header("Day Night Controls")] public int dayCountDown;
        public int timeFactor = 4;
        public double dayNightThreshold = 0.5;
        public float time;

        private double _result;
        private bool _oneDayPass;

        private void Start()
        {
            RenderSettings.skybox = dayMaterial;
        }

        private void Update()
        {
            // rotate the direction light based on the delta time, this snippet of code was taken from, and I made some modifications
            // https://answers.unity.com/questions/1675917/count-days-using-in-game-time-cycle-count-light-ro.html
            transform.RotateAround(Vector3.zero, Vector3.right, Time.deltaTime * timeFactor);
            transform.LookAt(Vector3.zero);
            time += Time.deltaTime * timeFactor;
            var day = time / 360;
            // 360 -> around 3 minutes -> 1.5 minutes for a shift 

            // only keep the digits for the day variable, and compare it with 0.5
            // to decide which skybox material to use
            _result = day - Math.Truncate(day);
            if (_result > dayNightThreshold)
            {
                isNight = true;
                if (!_oneDayPass)
                {
                    dayCountDown++;
                    _oneDayPass = true;
                }

                RenderSettings.skybox = nightMaterial;
                
                // night time light intensity changes
                transform.GetChild(0).GetComponent<Light>().intensity = 0.3f;
                transform.GetChild(1).GetComponent<Light>().intensity = 0f;
                transform.GetChild(2).GetComponent<Light>().intensity = 0f;
                transform.GetChild(3).GetComponent<Light>().intensity = 0f;
                transform.GetChild(4).GetComponent<Light>().intensity = 0f;
            }
            else
            {
                if (_oneDayPass)
                {
                    dayCounter.transform.GetChild(0).gameObject.GetComponent<Text>().text = (3 - dayCountDown) + "days";
                    _oneDayPass = false;
                }

                isNight = false;
                RenderSettings.skybox = dayMaterial;

                // day time light intensity changes
                transform.GetChild(0).GetComponent<Light>().intensity = 1f;
                transform.GetChild(1).GetComponent<Light>().intensity = 0.05f;
                transform.GetChild(2).GetComponent<Light>().intensity = 0.05f;
                transform.GetChild(3).GetComponent<Light>().intensity = 0.6f;
                transform.GetChild(4).GetComponent<Light>().intensity = 0.6f;
            }
        }
    }
}