using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Utility
{
    public class UnderWaterEffect : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.transform.GetChild(0).GetComponent<PostProcessVolume>().enabled = true;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.transform.GetChild(0).GetComponent<PostProcessVolume>().enabled = false;
            }
        }
    }
}
