using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UI;
using UnityEngine;

namespace Player
{
    public class PlayerPickUP : MonoBehaviour
    {
        public Inventory inventory;

        private void FixedUpdate()
        {
            var layerMask = LayerMask.GetMask("Default");

            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,
                    Mathf.Infinity, layerMask) && Input.GetMouseButton(0) && inventory.bag.Count < 10 &&
                hit.collider.gameObject.CompareTag("Item"))
            {
                var key = hit.collider.gameObject.name;

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