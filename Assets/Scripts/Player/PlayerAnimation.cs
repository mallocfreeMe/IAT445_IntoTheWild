using System;
using UI;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        public Inventory inventory;
        public bool playerIsChopping;
        public bool playerIsMining;

        private Animator _armAnimator;
        private Animator _itemAnimator;
        private Animator _toolAnimator;
        float timer = 40;
        bool eat = false;
        private void Start()
        {
            _armAnimator = transform.GetChild(0).GetChild(0).gameObject.GetComponent<Animator>();
            _itemAnimator = transform.GetChild(0).GetChild(1).GetChild(0).gameObject.GetComponent<Animator>();
            _toolAnimator = transform.GetChild(0).GetChild(2).gameObject.GetComponent<Animator>();
        }

        IEnumerator DelayAction(float delayTime)
        {
            //Wait for the specified delay time before continuing.
            yield return new WaitForSeconds(delayTime);
            _itemAnimator.SetBool("Eat", false);
            //Do the action after the delay time has finished.
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
                    playerIsChopping = true;
                    playerIsMining = false;
                }
                else if (inventory.toolsShowOnScreen[1].activeSelf)
                {
                    _toolAnimator.SetBool("Axe Swing", false);
                    _toolAnimator.SetBool("Pickaxe Swing", true);
                    playerIsChopping = false;
                    playerIsMining = true;
                }
                else
                {
                    _toolAnimator.SetBool("Pickaxe Swing", false);
                    _toolAnimator.SetBool("Axe Swing", false);
                    playerIsChopping = false;
                    playerIsMining = false;
                    
                    // arm -> arm swing
                    //_armAnimator.SetBool("Arm Swing", true);
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _toolAnimator.SetBool("Pickaxe Swing", false);
                _toolAnimator.SetBool("Axe Swing", false);
                playerIsChopping = false;
                playerIsMining = false;
                
                //_armAnimator.SetBool("Arm Swing", false);
            }

            // food -> eating animation
            var itemsOnHand = true;
            if (Input.GetMouseButtonDown(1))
            {
                
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
                        //Debug.Log("eating");
                        
                        _itemAnimator.SetBool("Eat", true);
                    }
                }
            }
            else if (Input.GetMouseButtonUp(1))
            {
                StartCoroutine(DelayAction(0.4f));
                itemsOnHand = false;
            }


        }


    }




}