using System;
using UnityEngine;
using UnityEngine.UI;

namespace Utility
{
    // require box collider for this script
    [RequireComponent(typeof(BoxCollider))]
    public class Food : MonoBehaviour
    {
        public Image hungerFill;
        public Image healthFill;
        public bool isPoisonous;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                hungerFill.fillAmount += 0.1f;
                if (isPoisonous)
                {
                    healthFill.fillAmount -= 0.1f;
                }
                else
                {
                    healthFill.fillAmount += 0.1f;
                }
                Destroy(gameObject);
            }
        }
    }
}
