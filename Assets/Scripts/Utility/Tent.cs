using System;
using UnityEngine;

namespace Utility
{
    public class Tent : MonoBehaviour
    {
        private DayCounter _dayCounter;

        private void Start()
        {
            _dayCounter = GameObject.Find("World Light").GetComponent<DayCounter>();
        }

        private void FixedUpdate()
        {
            if (_dayCounter != null)
            {
                var layerMask = LayerMask.GetMask("Default");
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit,
                        20, layerMask) && Input.GetMouseButton(0) && _dayCounter.isNight &&
                    hit.collider.gameObject.CompareTag("Tent"))
                {
                    _dayCounter.time = 10;
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}