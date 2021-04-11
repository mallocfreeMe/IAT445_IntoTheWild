using System;
using Player;
using UnityEngine;

namespace Utility
{
    public class CampFire : MonoBehaviour
    {
        private GameObject _player;
        private DayCounter _dayCounter;
        private float _campFireTimer;

        private void Start()
        {
            _player = GameObject.Find("Player");
            _dayCounter = GameObject.Find("World Light").GetComponent<DayCounter>();
            _campFireTimer = _dayCounter.time;
        }

        private void Update()
        {
            if ((int) Vector3.Distance(_player.transform.position, transform.position) < 10)
            {
                _player.GetComponent<PlayerStatus>().closeToCampFire = true;
            }
            else
            {
                _player.GetComponent<PlayerStatus>().closeToCampFire = false;
            }

            if (_dayCounter.time - _campFireTimer > 180)
            {
                Destroy(gameObject);
            }
        }
    }
}