using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using Utility;

namespace UI
{
    public class CraftRecipe : MonoBehaviour
    {
        public GameObject craftRecipeUI;
        public GameObject craftCostUI;
        public Inventory inventory;
        public GameObject cursor;
        public AudioClip interfacePopUpAudioClip;
        public AudioClip craftItemCompletedAudioClip;
        [Header("Prefab")] public GameObject axePrefab;
        public GameObject campFirePrefab;
        public GameObject tentPrefab;

        private GameObject _player;
        private Vector3 _playerPosition;
        private int _craftDistance;
        private Vector3 _craftPos;
        private int _craftRecipeUIHighlightedIndex;
        private Dictionary<string, int> _axeRecipe;
        private Dictionary<string, int> _campFireRecipe;
        private Dictionary<string, int> _tentRecipe;
        private AudioSource _audioSource;

        private void Start()
        {
            _player = GameObject.Find("Player");
            _audioSource = GetComponent<AudioSource>();
            _playerPosition = _player.transform.position;
            _craftDistance = 5;

            // hide the UI when game starts
            craftRecipeUI.SetActive(false);

            // add click events to all the crafting slot
            foreach (Transform child in craftRecipeUI.transform)
            {
                child.gameObject.GetComponent<Button>().onClick.AddListener(delegate
                {
                    InteractWithCraftRecipeUI(child.GetSiblingIndex());
                });
            }

            // add click event to the build button inside the 
            craftCostUI.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate
            {
                InteractWithCraftCostUI(craftCostUI.GetComponent<Image>().sprite.name);
            });

            _axeRecipe = new Dictionary<string, int>();
            _axeRecipe.Add("Stone", 2);
            _axeRecipe.Add("Branch", 3);

            _campFireRecipe = new Dictionary<string, int>();
            _campFireRecipe.Add("Branch", 10);
            _campFireRecipe.Add("Stone", 5);

            _tentRecipe = new Dictionary<string, int>();
            _tentRecipe.Add("Log", 3);
            _tentRecipe.Add("Stone", 4);
            _tentRecipe.Add("Reed", 7);
        }

        private void Update()
        {
            ToggleCraftRecipeUI();
        }

        private void LateUpdate()
        {
            _playerPosition = _player.transform.position;
            _craftPos = _playerPosition + _player.transform.forward * _craftDistance;
            _craftPos.y = Terrain.activeTerrain.SampleHeight(_craftPos);
        }

        // if the player presses down b key, toggle the craft recipe UI
        private void ToggleCraftRecipeUI()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (craftRecipeUI.activeSelf)
                {
                    _player.GetComponent<FirstPersonController>().ResetSensitivity();
                    craftRecipeUI.SetActive(false);
                    craftCostUI.SetActive(false);
                    StaticMethods.HideCursor();
                    cursor.SetActive(true);
                }
                else
                {
                    _player.GetComponent<FirstPersonController>().SetSensitivityToZero();
                    _audioSource.PlayOneShot(interfacePopUpAudioClip);
                    craftRecipeUI.SetActive(true);
                    StaticMethods.ShowCursor();
                    cursor.SetActive(false);
                }
            }
        }

        private void InteractWithCraftRecipeUI(int index)
        {
            if (_craftRecipeUIHighlightedIndex == index && craftCostUI.activeSelf)
            {
                craftCostUI.SetActive(false);
            }
            else
            {
                switch (index)
                {
                    case 0:
                        craftCostUI.GetComponent<Image>().sprite = Resources.Load<Sprite>("Axe Cost");
                        break;
                    case 1:
                        craftCostUI.GetComponent<Image>().sprite = Resources.Load<Sprite>("Camp Fire Cost");
                        break;
                    case 2:
                        craftCostUI.GetComponent<Image>().sprite = Resources.Load<Sprite>("Tent Cost");
                        break;
                }

                if (CheckCraftCost(index))
                {
                    craftCostUI.transform.GetChild(0).GetComponent<Image>().color = Color.yellow;
                }
                else
                {
                    craftCostUI.transform.GetChild(0).GetComponent<Image>().color = Color.grey;
                }

                craftCostUI.SetActive(true);
            }

            _craftRecipeUIHighlightedIndex = index;
        }

        private bool CheckCraftCost(int index)
        {
            var recipe = _axeRecipe;
            switch (index)
            {
                case 0:
                    recipe = _axeRecipe;
                    break;
                case 1:
                    recipe = _campFireRecipe;
                    break;
                case 2:
                    recipe = _tentRecipe;
                    break;
            }

            var count = 0;
            foreach (var item in recipe)
            {
                if (inventory.bag.ContainsKey(item.Key))
                {
                    if (inventory.bag[item.Key] >= item.Value)
                    {
                        count++;
                    }
                }
            }

            if (count == recipe.Count)
            {
                return true;
            }

            return false;
        }

        private void InteractWithCraftCostUI(string name)
        {
            if (craftCostUI.transform.GetChild(0).GetComponent<Image>().color == Color.yellow)
            {
                var recipe = _axeRecipe;
                var index = 0;
                
                switch (name)
                {
                    case "Axe Cost":
                        recipe = _axeRecipe;
                        index = 0;
                        var objA = Instantiate(axePrefab,new Vector3(_craftPos.x,_craftPos.y + 1,_craftPos.z), Quaternion.identity);
                        objA.name = "Iron Axe";
                        break;
                    case "Camp Fire Cost":
                        recipe = _campFireRecipe;
                        index = 1;
                        var objC =Instantiate(campFirePrefab,_craftPos, Quaternion.identity);
                        objC.name = "CampFire";
                        break;
                    case "Tent Cost":
                        recipe = _tentRecipe;
                        index = 2;
                        var objT = Instantiate(tentPrefab,new Vector3(_craftPos.x,_craftPos.y + 1,_craftPos.z), Quaternion.identity);
                        objT.name = "Tent";
                        break;
                }
                
                foreach (var item in recipe)
                {
                    inventory.bag[item.Key] -= item.Value;
                }

                _audioSource.PlayOneShot(craftItemCompletedAudioClip);
                
                if (CheckCraftCost(index))
                {
                    craftCostUI.transform.GetChild(0).GetComponent<Image>().color = Color.yellow;
                }
                else
                {
                    craftCostUI.transform.GetChild(0).GetComponent<Image>().color = Color.grey;
                }
            }
        }
    }
}