using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UI;
using UnityEngine;

namespace Player
{
    public class PlayerChopping : MonoBehaviour
    {
        public Transform axeTransform;
        public GameObject branchPrefab;
        public GameObject logPrefab;
        public bool generateBranch;
        public Inventory inventory;
        [Header("Axe Durability")] public int axeDurability = 3;

        [Header("One Tree Generate a fix amount of logs and branches")]
        public int generateBranchNumber = 5;
        public int generateLogNumber = 2;

        private PlayerAnimation _playerAnimation;
        private Vector3 _generateBranchPos;
        private int _axeCounter;

        private void Start()
        {
            _playerAnimation = GetComponent<PlayerAnimation>();
        }

        private void Update()
        {
            if (generateBranch)
            {
                _axeCounter++;
                
                for (var i = 0; i < generateBranchNumber; i++)
                {
                    var branch = Instantiate(branchPrefab,
                        new Vector3(_generateBranchPos.x, _generateBranchPos.y + 10, _generateBranchPos.z),
                        Quaternion.identity);
                    branch.name = "Branch";
                }
                
                for (var i = 0; i < generateLogNumber; i++)
                {
                    var log = Instantiate(logPrefab,
                        new Vector3(_generateBranchPos.x, _generateBranchPos.y + 10, _generateBranchPos.z),
                        Quaternion.identity);
                    log.name = "Log";
                }
                
                generateBranch = false;
            }

            if (_axeCounter == axeDurability)
            {
                var values = inventory.bag["Iron Axe"];
                values--;
                inventory.bag["Iron Axe"] = values;
                _axeCounter = 0;
            }
        }

        // learn how to update the terrain after removing a tree instance
        // from https://www.reddit.com/r/Unity3D/comments/371enj/removing_single_treeinstance_from_terraindata/
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.name == "Main Island" && _playerAnimation.playerIsChopping)
            {
                var activeTerrain = other.gameObject.GetComponent<Terrain>();
                var treeInstances = activeTerrain.terrainData.treeInstances.ToList();

                foreach (var tree in treeInstances)
                {
                    if (Vector3.Distance(Vector3.Scale(tree.position, activeTerrain.terrainData.size),
                        axeTransform.position) < 5)
                    {
                        treeInstances.Remove(tree);
                        generateBranch = true;
                        _generateBranchPos =
                            Vector3.Scale(tree.position, activeTerrain.terrainData.size);
                        break;
                    }
                }

                activeTerrain.terrainData.treeInstances = treeInstances.ToArray();

                float[,] heights = activeTerrain.terrainData.GetHeights(0, 0, 0, 0);
                activeTerrain.terrainData.SetHeights(0, 0, heights);
            }
        }
    }
}