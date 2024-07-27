using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerSphere : MonoBehaviour
    {
        public float _moveSpeed = 5f;    // Speed of movement along the surface
        public float _stickForce = 10f;  // Force to keep the character attached to the surface

        private Rigidbody _rb;
        private bool _isGrounded = false;
        private Vector3 _groundNormal;
        private Vector3 _groundPosition;

        public Vector3 GroundNormal => _groundNormal;
        public Vector3 GroundPosition => _groundPosition;
        public Rigidbody Rigidbody => _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            // Basic input for left/right movement
            //float moveInput = Input.GetAxis("Horizontal");
            float leftMoveInput = Input.GetKey(KeyCode.A) ? -1 : 0;
            float rightMoveInput = Input.GetKey(KeyCode.D) ? 1 : 0;
            //float moveInput = leftMoveInput + rightMoveInput;
            float moveInput = 1f;

            // Calculate the movement direction along the surface

            _isGrounded = CheckIfGrounded();

            Vector3 perpendicular = Vector3.Cross(_groundNormal, Vector3.forward);

            // If the resulting vector is too small, use another vector for the cross product
            if (perpendicular.magnitude < 0.001f)
            {
                perpendicular = Vector3.Cross(_groundNormal, Vector3.right);
            }

            Vector3 right = perpendicular.normalized;
            Vector3 moveDirection = right * moveInput * _moveSpeed;


            if (_isGrounded)
            {
                // Apply the movement along the surface
                _rb.velocity = moveDirection;
            }


            // Apply force to keep character sticking to the surface
            //_rb.AddForce(-_groundNormal * _stickForce);

        }

        private void OnCollisionStay(Collision collision)
        {
            // Get the normal of the surface at the point of contact
            _groundNormal = collision.contacts[0].normal;
            _groundPosition = collision.contacts[0].point;


        }

        private bool CheckIfGrounded()
        {
            return Vector3.Distance(transform.position, _groundPosition) < 0.3f;
        }

    }

}

