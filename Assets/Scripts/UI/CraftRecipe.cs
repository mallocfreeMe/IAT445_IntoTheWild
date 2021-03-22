using System;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI
{
    public class CraftRecipe : MonoBehaviour
    {
        public GameObject craftRecipeUI;
        public GameObject craftCostUI;
        public GameObject cursor;
        public AudioClip interfacePopUpAudioClip;
        public AudioClip craftItemCompletedAudioClip;

        [Header("Prefab")] public GameObject axe;
        public GameObject campFire;
        public GameObject tent;

        // item the player selected 
        private Image _item;
        private GameObject _player;
        private Vector3 _playerPosition;
        private int _craftDistance;
        private AudioSource _audioSource;

        private void Start()
        {
            _item = null;
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
                    InteractWithCraftingItem(child.GetSiblingIndex());
                });
            }
            
            // add click event to the build button inside the 
            craftCostUI.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate
            {
                BuildCraftingItem(craftCostUI.GetComponent<Image>().sprite.name);
            });
        }

        private void Update()
        {
            ToggleCraftRecipeUI();
        }

        // this method get called after update
        // set the pos in front of the player
        // based on https://stackoverflow.com/questions/22696782/placing-an-object-in-front-of-the-camera
        // get relative height to the terrain space based on player position, then craft the item in that position 
        // based on https://docs.unity3d.com/ScriptReference/Terrain.SampleHeight.html
        private void LateUpdate()
        {
            if (_item != null)
            {
                _playerPosition = _player.transform.position;
                var pos = _playerPosition + _player.transform.forward * _craftDistance;
                pos.y = Terrain.activeTerrain.SampleHeight(pos);
            }
        }

        // if the player presses down b key, toggle the craft recipe UI
        private void ToggleCraftRecipeUI()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (craftRecipeUI.activeSelf)
                {
                    craftRecipeUI.SetActive(false);
                    craftCostUI.SetActive(false);
                    _item = null;
                    StaticMethods.HideCursor();
                    cursor.SetActive(true);
                }
                else
                {
                    _audioSource.PlayOneShot(interfacePopUpAudioClip);
                    craftRecipeUI.SetActive(true);
                    StaticMethods.ShowCursor();
                    cursor.SetActive(false);
                }
            }
        }

        private void InteractWithCraftingItem(int index)
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
            craftCostUI.SetActive(true);
        }

        private void BuildCraftingItem(string name)
        {
            switch (name)
            {
                case "Axe Cost":
                    break;
                case "Camp Fire Cost":
                    break;
                case "Tent Cost":
                    break;
            }
            
            // play the audio clip
            _audioSource.PlayOneShot(craftItemCompletedAudioClip);
        }
    }
}