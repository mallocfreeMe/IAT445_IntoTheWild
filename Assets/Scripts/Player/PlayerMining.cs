using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player
{
    public class PlayerMining : MonoBehaviour
    {
        public Transform pickaxeTransform;
        public GameObject stonePrefab;
        public bool generateStone;

        private PlayerAnimation _playerAnimation;
        private Vector3 _generateStonePos;

        private void Start()
        {
            _playerAnimation = GetComponent<PlayerAnimation>();
        }

        private void Update()
        {
            if (generateStone)
            {
                for (var i = 0; i < 5; i++)
                {
                    var stone = Instantiate(stonePrefab, _generateStonePos, Quaternion.identity);
                    stone.name = "Stone";
                }

                generateStone = false;
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
                    Destroy(other.gameObject);
                }
            }
        }
    }
}