using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CraftRecipe : MonoBehaviour
    {
        public GameObject craftRecipeUI;
        public AudioClip interfacePopUpAudioClip;
        public AudioClip craftItemCompletedAudioClip;
        public Image slot1, slot2, slot3;

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
        }

        private void Update()
        {
            ToggleCraftRecipeUI();
            ChooseItem();
        }

        // this method get called after update
        private void LateUpdate()
        {
            if (_item != null)
            {
                // always update player position
                _playerPosition = _player.transform.position;
                
                // set the pos in front of the player
                // based on https://stackoverflow.com/questions/22696782/placing-an-object-in-front-of-the-camera
                var pos = _playerPosition + _player.transform.forward * _craftDistance;
                
                // get relative height to the terrain space based on player position, then craft the item in that position 
                // based on https://docs.unity3d.com/ScriptReference/Terrain.SampleHeight.html
                pos.y = Terrain.activeTerrain.SampleHeight(pos);

                CreateItem(_item, pos);
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

                    // disable the highlighted slot once the UI is hidden
                    ChooseItemHelper(null);
                    _item = null;
                }
                else
                {
                    _audioSource.PlayOneShot(interfacePopUpAudioClip);
                    craftRecipeUI.SetActive(true);
                }
            }
        }

        // press number keys (1,2 and 3) to choose which item you want to craft
        private void ChooseItem()
        {
            // only listens to keyboard events when the craft recipe UI is active
            if (craftRecipeUI.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
                {
                    ChooseItemHelper(slot1);
                    _item = slot1;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
                {
                    ChooseItemHelper(slot2);
                    _item = slot2;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
                {
                    ChooseItemHelper(slot3);
                    _item = slot3;
                }
            }
        }

        // the item that being selected should be highlighted, and unselected slots should be normal
        private void ChooseItemHelper(Image slot)
        {
            Color highlightedColor = new Color32(0XF3, 0XEC, 0XA4, 0XFF);
            Image[] slots = {slot1, slot2, slot3};
            for (var i = 0; i < slots.Length; i++)
            {
                if (slots[i] == slot)
                {
                    slots[i].color = highlightedColor;
                }
                else
                {
                    slots[i].color = Color.white;
                }
            }
        }

        // once the player choose the item he want to build
        // left mouse click to build the item
        private void CreateItem(Image item, Vector3 pos)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // 1. axe, axe will fall down to the ground -> pos.y + 3
                // 2. camp fire
                // 3. tent, rotate -90 degree on x axis 
                if (item == slot1)
                {
                    Instantiate(axe, new Vector3(pos.x, pos.y + 3, pos.z), Quaternion.identity);
                }
                else if (item == slot2)
                {
                    Instantiate(campFire, pos, Quaternion.identity);
                }
                else if (item == slot3)
                {
                    Instantiate(tent, pos, Quaternion.Euler(-90, 0, 0));
                }
                
                // play the audio clip
                _audioSource.PlayOneShot(craftItemCompletedAudioClip);
            }
        }
    }
}