using System;
using UI;
using UnityEngine;

namespace Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        public Inventory inventory;

        private Animator _armAnimator;
        private Animator _itemAnimator;
        private Animator _toolAnimator;

        private void Start()
        {
            _armAnimator = transform.GetChild(0).GetChild(0).gameObject.GetComponent<Animator>();
            _itemAnimator = transform.GetChild(0).GetChild(1).GetChild(0).gameObject.GetComponent<Animator>();
            _toolAnimator = transform.GetChild(0).GetChild(2).gameObject.GetComponent<Animator>();
        }

        private void Update()
        {
            // tools -> swing animation
            if (Input.GetMouseButtonDown(0))
            {
                if (inventory.toolsShowOnScreen[0].activeSelf)
                {
                    _toolAnimator.SetBool("Axe Swing", true);
                    _toolAnimator.SetBool("Pickaxe Swing", false);
                }
                else if (inventory.toolsShowOnScreen[1].activeSelf)
                {
                    _toolAnimator.SetBool("Pickaxe Swing", true);
                    _toolAnimator.SetBool("Axe Swing", false);
                }
                else
                {
                    _toolAnimator.SetBool("Pickaxe Swing", true);
                    _toolAnimator.SetBool("Axe Swing", false);
                    
                    // arm -> arm swing
                    _armAnimator.SetBool("Arm Swing", true);
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _toolAnimator.SetBool("Pickaxe Swing", false);
                _toolAnimator.SetBool("Axe Swing", false);
                _armAnimator.SetBool("Arm Swing", false);
            }

            // food -> eating animation
            if (Input.GetMouseButtonDown(1))
            {
                var itemsOnHand = false;
                string itemName = "";

                foreach (var item in inventory.itemsShowOnScreen)
                {
                    if (item.activeSelf)
                    {
                        itemsOnHand = true;
                        itemName = item.name;
                        break;
                    }
                }
                
                if (itemsOnHand)
                {
                    string[] food = {"Apple", "Pear", "Mushroom", "Pepper"};
                    var pos = Array.IndexOf(food, itemName);
                    if (pos > -1)
                    {
                        _itemAnimator.SetBool("Eat", true);
                    }
                }
            }
            else if (Input.GetMouseButtonUp(1))
            {
                _itemAnimator.SetBool("Eat", false);
            }
        }
    }
}