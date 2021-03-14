using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Player
{
    public class PlayerPickUP : MonoBehaviour
    {
        public Inventory inventory;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Item"))
            {
                var key = other.gameObject.name;

                if (!inventory.bag.ContainsKey(key))
                {
                    inventory.bag.Add(other.gameObject.name, 1);
                }
                else
                {
                    var values = inventory.bag[key];
                    values++;
                    inventory.bag[other.gameObject.name] = values;
                }
            }
        }
    }
}