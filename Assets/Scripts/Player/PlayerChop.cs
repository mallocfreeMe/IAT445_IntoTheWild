using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UI;
using UnityEngine;

namespace Player
{
    public class PlayerChop : MonoBehaviour
    {
        public Transform axeTransform;
        public GameObject branchPrefab;
        public bool generateBranch;

        private PlayerAnimation _playerAnimation;
        private Vector3 _generateBranchPos;

        private void Start()
        {
            _playerAnimation = GetComponent<PlayerAnimation>();
        }

        private void Update()
        {
            if (generateBranch)
            {
                for (var i = 0; i < 5; i++)
                {
                    var branch = Instantiate(branchPrefab,
                        new Vector3(_generateBranchPos.x, _generateBranchPos.y + 10, _generateBranchPos.z),
                        Quaternion.identity);
                    branch.name = "Branch";
                }

                generateBranch = false;
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