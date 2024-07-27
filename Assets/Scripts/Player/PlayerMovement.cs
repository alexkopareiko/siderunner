using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerMovement : MonoBehaviour
    {
        public float moveSpeed = 5f;    // Speed of movement along the surface
        public float stickForce = 10f;  // Force to keep the character attached to the surface

        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            // Basic input for left/right movement
            float moveInput = Input.GetAxis("Horizontal");

            // Calculate the movement direction along the surface
            Vector3 moveDirection = transform.right * moveInput * moveSpeed;

            // Apply the movement along the surface
            rb.velocity = moveDirection + (transform.up * rb.velocity.y); // Retain vertical velocity component

            // Apply force to keep character sticking to the surface
            rb.AddForce(-transform.up * stickForce);
        }

        void OnCollisionStay(Collision collision)
        {
            // Get the normal of the surface at the point of contact
            Vector3 surfaceNormal = collision.contacts[0].normal;

            // Align the character's up direction with the surface normal
            transform.up = surfaceNormal;
        }
    }

}
