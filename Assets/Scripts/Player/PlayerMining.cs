using System;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    public class PlayerMining : MonoBehaviour
    {
        public Transform pickaxeTransform;
        public GameObject stonePrefab;
        public bool generateStone;
        public Inventory inventory;
        [Header("Pick Axe Durability")] public int pickaxeDurability = 3;

        [Header("A random amount of stones ranges")]
        public int[] generateStoneRange;

        private PlayerAnimation _playerAnimation;
        private Vector3 _generateStonePos;
        private int _pickAxeCounter;

        private void Start()
        {
            _playerAnimation = GetComponent<PlayerAnimation>();
        }

        private void Update()
        {
            if (generateStone)
            {
                _pickAxeCounter++;

                var min = generateStoneRange[0];
                var max = generateStoneRange[1];
                var result = Random.Range(min, max);

                for (var i = 0; i < result; i++)
                {
                    var stone = Instantiate(stonePrefab, _generateStonePos, Quaternion.identity);
                    stone.name = "Stone";
                }

                generateStone = false;
            }

            if (_pickAxeCounter == pickaxeDurability)
            {
                var values = inventory.bag["Pick Axe"];
                values--;
                inventory.bag["Pick Axe"] = values;
                _pickAxeCounter = 0;
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Mine") && _playerAnimation.playerIsMining)
            {
                if (Vector3.Distance(other.transform.position, pickaxeTransform.position) < 5)
                {
                    generateStone = true;
                    _generateStonePos = other.transform.position;
                    Destroy(other.gameObject, 0.3f);
                }
            }
        }
    }
}