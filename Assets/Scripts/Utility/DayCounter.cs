using System;
using UnityEngine;

namespace Utility
{
    public class DayCounter : MonoBehaviour
    {
        public Material dayMaterial;
        public Material nightMaterial;

        private float _time;

        private void Start()
        {
            RenderSettings.skybox = dayMaterial;
        }

        private void Update()
        {
            // rotate the direction light based on the delta time, this snippet of code was taken from, and I made some modifications
            // https://answers.unity.com/questions/1675917/count-days-using-in-game-time-cycle-count-light-ro.html
            transform.RotateAround(Vector3.zero, Vector3.right, 15f * Time.deltaTime);
            transform.LookAt(Vector3.zero);
            _time += 15 * Time.deltaTime;
            var day = _time / 360;
            
            // only keep the digits for the day variable, and compare it with 0.5
            // to decide which skybox material to use
            var result = day - Math.Truncate(day);
            RenderSettings.skybox = result > 0.5 ? nightMaterial : dayMaterial;
        }
    }
}