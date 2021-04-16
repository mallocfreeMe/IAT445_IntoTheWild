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
        public float timeFactor = 4;
        public double dayNightThreshold = 0.5;
        public float time;
        public float lightVal;
        public float timeTracker;
        private double _result;
        private bool _oneDayPass;
        //private float exposure = 1;

        [Header("Water body material day/night adjustements")]
        public GameObject lake1;
        public GameObject lake2;
        public GameObject pond1;
        public GameObject sea1;
        public Material nightLake, nightPond, nightSea, dayLake, dayPond, daySea;

        float dynamicChanger;

        private float _dayLightTrans, _secLights, _botLights;
        //private float dayToNight = 0f, nightToDay = 0.9999f, dayLightVal = 1f, nightLightVal = 0.32f;  //main lighting

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
            _dayLightTrans = 0.5f;
            _secLights = 0.05f;
            _botLights = 0.6f;

            //= map(_dayLightTrans, dayLightVal, 0, nightLightVal, _result); 
            if (_result >= 0 && _result <= 0.125)
            {
                _botLights = 0.4f;
                _secLights = 0.04f;
                _dayLightTrans = 0.625f + (float)(_result) * 3f;

                RenderSettings.skybox.Lerp(nightMaterial, dayMaterial, _dayLightTrans * 8f);
            }
            else if (_result > 0.125 && _result <= 0.25)
            {
                _botLights = 0.6f;
                _secLights = 0.06f;
                _dayLightTrans = 1f;
                RenderSettings.skybox.Lerp(dayMaterial, dayMaterial, _dayLightTrans * 8f);

            }
            else if (_result > 0.25 && _result <= 0.625)
            {

                if(_result < 0.5)
                {
                    dynamicChanger = 1.3f;
                }
                else
                {
                    dynamicChanger = 1.82f;
                }
                _botLights = 0.3f;
                _secLights = 0.03f;
                _dayLightTrans = Math.Abs(1.325f - (float)(_result)* dynamicChanger);
                RenderSettings.skybox.Lerp(dayMaterial, nightMaterial, _dayLightTrans * 8f);

            }
            else if (_result > 0.625 && _result <= 0.75)
            {
                _botLights = 0.3f;
                _secLights = 0.03f;
                _dayLightTrans = 0.1875f;
                RenderSettings.skybox.Lerp(nightMaterial, nightMaterial, _dayLightTrans * 8f);
            }
            else if (_result > 0.75 && _result <= 1)
            {
                _botLights = 0.33f;
                _secLights = 0.033f;
                _dayLightTrans = Math.Abs((float)(_result) / 4f);
                RenderSettings.skybox.Lerp(nightMaterial, dayMaterial, _dayLightTrans*8f);
            }

            timeTracker = (float)(_result);
            lightVal = _dayLightTrans;

            if (_result > dayNightThreshold)
            {
                isNight = true;
                if (!_oneDayPass)
                {
                    dayCountDown++;
                    _oneDayPass = true;
                }


                lake1.GetComponent<Renderer>().material = nightLake;
                lake2.GetComponent<Renderer>().material = nightLake;
                pond1.GetComponent<Renderer>().material = nightPond;
                sea1.GetComponent<Renderer>().material = nightSea;

                RenderSettings.skybox = nightMaterial;

                // night time light intensity changes

            }
            else
            {
                if (_oneDayPass)
                {
                    dayCounter.transform.GetChild(0).gameObject.GetComponent<Text>().text = (3 - dayCountDown) + " days";
                    _oneDayPass = false;
                }
                lake1.GetComponent<Renderer>().material = dayLake;
                lake2.GetComponent<Renderer>().material = dayLake;
                pond1.GetComponent<Renderer>().material = dayPond;
                sea1.GetComponent<Renderer>().material = daySea;
                isNight = false;
                RenderSettings.skybox = dayMaterial;

                // day time light intensity changes

            }

            //Debug.Log("_Exposure:  " + __Exposure);
            //Debug.Log("time:  " + _result);
            //Debug.Log("light:  " + _dayLightTrans);
           
            transform.GetChild(0).GetComponent<Light>().intensity = _dayLightTrans;  //0.32f
            transform.GetChild(1).GetComponent<Light>().intensity = _secLights;
            transform.GetChild(2).GetComponent<Light>().intensity = _botLights;


        }



        /*
        public float map(float value, float start1, float stop1, float start2, float stop2)
        {
            float outgoing = start2 + (stop2 - start2) * ((value - start1) / (stop1 - start1));
            return outgoing;

        }
        */
    }
}