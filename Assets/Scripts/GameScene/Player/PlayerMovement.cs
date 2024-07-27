using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody _rb;

        [SerializeField] private PlayerSphere _playerSphere;

        public PlayerSphere PlayerSphere => _playerSphere;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            //transform.position = Vector3.Lerp(transform.position, _playerSphere.transform.position, Time.deltaTime * 10f);
            transform.position = _playerSphere.transform.position;
            transform.up = Vector3.Lerp(transform.up, _playerSphere.GroundNormal, Time.deltaTime * 10f);
            //transform.up = Vector3.up;
        }

    }

}
