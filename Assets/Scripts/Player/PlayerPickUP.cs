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

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.collider.gameObject.CompareTag("Item"))
                    {
                        Destroy(hit.collider.gameObject);
        
                        var key = hit.collider.gameObject.name;

                        if (!inventory.bag.ContainsKey(key))
                        {
                            inventory.bag.Add(hit.collider.gameObject.name, 1);
                        }
                        else
                        {
                            var values = inventory.bag[key];
                            values++;
                            inventory.bag[hit.collider.gameObject.name] = values;
                        }
                    }
                }
            }
        }
    }
}