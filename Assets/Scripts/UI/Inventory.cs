using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using Image = UnityEngine.UI.Image;

namespace UI
{
    public class Inventory : MonoBehaviour
    {
        public GameObject inventoryUI;
        public Dictionary<string, int> bag;

        private Image _hunger, _thirst, _health;

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
        }

        private void Update()
        {
            ToggleInventoryUI();
            ShowInventoryItems();
        }

        private void ToggleInventoryUI()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (inventoryUI.activeSelf)
                {
                    inventoryUI.SetActive(false);
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
                else
                {
                    inventoryUI.SetActive(true);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
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
                            child.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(arr[index]);
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
                                _health.fillAmount += 0.1f;
                                break;
                            case "Mushroom":
                                _hunger.fillAmount += 0.1f;
                                _health.fillAmount -= 0.3f;
                                break;
                        } 
                        bag[arr[index]]--;
                    }
                }
            }
        }
    }
}