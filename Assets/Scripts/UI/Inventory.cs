using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace UI
{
    public class Inventory : MonoBehaviour
    {
        public GameObject inventoryUI;
        public Dictionary<string, int> bag;
        
        private void Start()
        {
            bag = new Dictionary<string, int>();
        }

        private void Update()
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
                        child.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(arr[index]);
                        child.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = bag[arr[index]].ToString();
                        index++;
                        size--;
                    }
                }
            }

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
    }
}
