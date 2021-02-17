using System;
using UnityEngine;
using UnityEngine.UI;

namespace Utility
{    
    // require box collider for this script
    [RequireComponent(typeof(BoxCollider))]
    public class StyledWater : MonoBehaviour
    {
        public Image waterFill;

        private void OnCollisionEnter(Collision other)
        {
            // if player drinks water, then his water status goes up
            // after 1 sec, remove the water object
            if (other.gameObject.CompareTag("Player"))
            {
                waterFill.fillAmount += 0.1f;
                Destroy(gameObject, 1);
            }
        }
    }
}
