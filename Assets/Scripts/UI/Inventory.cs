using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using Utility;
using Image = UnityEngine.UI.Image;

namespace UI
{
    public class Inventory : MonoBehaviour
    {
        public GameObject inventoryUI;
        public Dictionary<string, int> bag;
        public GameObject[] itemsShowOnScreen;
        public GameObject playerHand;
        public GameObject[] toolsShowOnScreen;

        private Image _hunger, _thirst, _health;
        private int _highlightedSlotIndex;
        private Image _previousHighlightedSlot;
        private Sprite _highlightedSlotSprite;
        private Sprite _notHighlightedSlotSprite;

        private void Start()
        {
            bag = new Dictionary<string, int>();

            foreach (Transform child in inventoryUI.transform)
            {
                // learn how to add event listen with a parameter
                // from https://docs.unity3d.com/2019.1/Documentation/ScriptReference/UI.Button-onClick.html
                // learn how to use GetSiblingIndex()
                // from https://docs.unity3d.com/ScriptReference/Transform.GetSiblingIndex.html
                child.gameObject.GetComponent<Button>().onClick.AddListener(delegate
                {
                    InteractWithInventoryItem(child.GetSiblingIndex());
                });
            }

            _hunger = GameObject.Find("/Canvas/Player Status/Hunger/Hunger Fill").GetComponent<Image>();
            _thirst = GameObject.Find("/Canvas/Player Status/Water/Water Fill").GetComponent<Image>();
            _health = GameObject.Find("/Canvas/Player Status/Health/Health Fill").GetComponent<Image>();
            _highlightedSlotSprite = Resources.Load<Sprite>("Selected Slot");
            _notHighlightedSlotSprite = Resources.Load<Sprite>("Unselected Slot");
        }

        private void Update()
        {
            // ToggleInventoryUI();
            ShowHighlightedItemOnScreen();
            if (inventoryUI.activeSelf)
            {
                ShowInventoryItems();
                ScrollInventoryItems();
                InteractWithHighlightedSlot();
            }
        }

        private void ToggleInventoryUI()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (inventoryUI.activeSelf)
                {
                    inventoryUI.SetActive(false);
                }
                else
                {
                    inventoryUI.SetActive(true);
                }
            }
        }

        private void ShowInventoryItems()
        {
            if (bag.Count > 0)
            {
                var keys = bag.Keys;
                var arr = keys.ToArray();
                var size = arr.Length;
                var index = 0;

                foreach (Transform child in inventoryUI.transform)
                {
                    if (size != 0)
                    {
                        if (bag[arr[index]] == 0)
                        {
                            bag.Remove(arr[index]);
                        }

                        if (index < bag.Count && bag.ContainsKey(arr[index]))
                        {
                            // Do a string convention, ensure every name is correct, the first letter is capital
                            var imageName = arr[index];
                            char[] a = imageName.ToCharArray();
                            a[0] = char.ToUpper(a[0]);
                            imageName = new string(a);

                            // load the correspond image file in the resources folder
                            child.gameObject.GetComponent<Image>().sprite =
                                Resources.Load<Sprite>(imageName);
                            child.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text =
                                bag[arr[index]].ToString();
                        }
                        else
                        {
                            inventoryUI.transform.GetChild(index).GetComponent<Image>().sprite = null;
                            child.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "";
                        }

                        index++;
                        size--;
                    }
                }
            }
        }

        // a click event for each item in the inventory system
        private void InteractWithInventoryItem(int index)
        {
            if (bag.Count > 0)
            {
                var keys = bag.Keys;
                var arr = keys.ToArray();
                if (arr.Length > index)
                {
                    if (bag[arr[index]] > 0)
                    {
                        switch (arr[index])
                        {
                            case "Apple":
                                _hunger.fillAmount += 0.1f;
                                _thirst.fillAmount += 0.1f;
                                _health.fillAmount += 0.1f;
                                bag[arr[index]]--;
                                break;
                            case "Pear":
                                _hunger.fillAmount += 0.1f;
                                _thirst.fillAmount += 0.1f;
                                _health.fillAmount += 0.1f;
                                bag[arr[index]]--;
                                break;
                            case "Pepper":
                                _hunger.fillAmount += 0.1f;
                                _thirst.fillAmount -= 0.1f;
                                _health.fillAmount += 0.1f;
                                bag[arr[index]]--;
                                break;
                            case "Mushroom":
                                _hunger.fillAmount += 0.1f;
                                _thirst.fillAmount += 0.1f;
                                _health.fillAmount -= 0.3f;
                                bag[arr[index]]--;
                                break;
                        }
                    }
                }
            }
        }

        private void ScrollInventoryItems()
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (_highlightedSlotIndex < 9)
                {
                    _highlightedSlotIndex++;
                }
                else
                {
                    _highlightedSlotIndex = 0;
                }
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (_highlightedSlotIndex > 0)
                {
                    _highlightedSlotIndex--;
                }
                else
                {
                    _highlightedSlotIndex = 9;
                }
            }

            if (_previousHighlightedSlot != null)
            {
                _previousHighlightedSlot.sprite = _notHighlightedSlotSprite;
            }

            inventoryUI.transform.GetChild(_highlightedSlotIndex).GetChild(1).gameObject.GetComponent<Image>().sprite =
                _highlightedSlotSprite;
            _previousHighlightedSlot =
                inventoryUI.transform.GetChild(_highlightedSlotIndex).GetChild(1).gameObject.GetComponent<Image>();
        }

        private void ShowHighlightedItemOnScreen()
        {
            if (_previousHighlightedSlot != null)
            {
                var keys = bag.Keys;
                var arr = keys.ToArray();
                if (_highlightedSlotIndex < arr.Length)
                {
                    playerHand.SetActive(false);
                    if (arr[_highlightedSlotIndex] == toolsShowOnScreen[0].name || arr[_highlightedSlotIndex] == toolsShowOnScreen[1].name)
                    {
                        foreach (var item in itemsShowOnScreen)
                        {
                            item.SetActive(false);
                        }
                        
                        if (arr[_highlightedSlotIndex] == toolsShowOnScreen[0].name)
                        {
                            toolsShowOnScreen[0].SetActive(true);
                        }
                        else
                        {
                            toolsShowOnScreen[1].SetActive(true);
                        }
                    }
                    else
                    {
                        toolsShowOnScreen[0].SetActive(false);
                        toolsShowOnScreen[1].SetActive(false);
                        
                        foreach (var item in itemsShowOnScreen)
                        {
                            if (item.name == arr[_highlightedSlotIndex])
                            {
                                item.SetActive(true);
                            }
                            else
                            {
                                item.SetActive(false);
                            }
                        }   
                    }
                }
                else
                {
                    playerHand.SetActive(true);
                    foreach (var item in itemsShowOnScreen)
                    {
                        item.SetActive(false);
                    }
                    toolsShowOnScreen[0].SetActive(false);
                    toolsShowOnScreen[1].SetActive(false);
                }
            }
        }

        private void InteractWithHighlightedSlot()
        {
            if (_previousHighlightedSlot != null && Input.GetMouseButtonDown(1))
            {
                inventoryUI.transform.GetChild(_highlightedSlotIndex).gameObject.GetComponent<Button>().onClick
                    .Invoke();
            }
        }
    }
}