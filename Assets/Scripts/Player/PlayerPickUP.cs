using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerPickUP : MonoBehaviour
    {
        public Inventory inventory;
        private void FixedUpdate()
        {
            var layerMask = LayerMask.GetMask("Default");
            RaycastHit hit;
            
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit,
                    20, layerMask) && Input.GetMouseButton(0) && inventory.bag.Count < 10 &&
                hit.collider.gameObject.CompareTag("Item"))
            {
                var key = hit.collider.gameObject.name;
                char[] a = key.ToCharArray();
                a[0] = char.ToUpper(a[0]);
                key = new string(a);

                if (!inventory.bag.ContainsKey(key))
                {
                    inventory.bag.Add(key, 1);
                }
                else
                {
                    var values = inventory.bag[key];
                    values++;
                    inventory.bag[key] = values;
                }

                Destroy(hit.collider.gameObject);
            }
        }
    }
}