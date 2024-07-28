using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class EnemyCombat : MonoBehaviour
    {
        [SerializeField] private LayerMask _actorLayer; // Set this in the Inspector to your "actor" layer
        [SerializeField] private float _detectionRadius = 5f; // Set the radius of detection
        [SerializeField] private Animator _animator;

        private Player _playerComponent;
        private ActorStats _actorStats;

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private void Start()
        {
            _animator = Helpers.CheckOnComponent<Animator>(gameObject);
            _actorStats = Helpers.CheckOnComponent<ActorStats>(gameObject);
        }

        private void Update()
        {
            CheckForNearbyActors();
        }

        private void CheckForNearbyActors()
        {
            if (CheckIfAttackAnimationIsPlaying() || _playerComponent != null)
            {
                return;
            }

            if (_actorStats.health <= 0)
            {
                return;
            }

            // Get all colliders within the detection radius and on the specified layer
            Collider[] colliders = Physics.OverlapSphere(transform.position, _detectionRadius, _actorLayer);

            foreach (Collider collider in colliders)
            {
                // Check if the collider's GameObject has a "Player" component
                _playerComponent = collider.GetComponent<Player>();
                if (_playerComponent != null)
                {
                    _animator.SetTrigger("Attack");
                    Attack();
                }
            }
        }

        private void Attack()
        {
            if (_playerComponent == null)
            {
                return;
            }

            if (Vector3.Distance(_playerComponent.transform.position, transform.position) > _detectionRadius * 2f)
            {
                return;
            }
            Debug.Log("enemy attack 1");

            ActorStats actorStats = _playerComponent.ActorStats;
            float damage = Helpers.CheckOnComponent<ActorStats>(gameObject).damage;
            actorStats.TakeDamage(damage);
            _playerComponent = null;
        }

        private void AttackFromAnimation()
        {

        }


        private bool CheckIfAttackAnimationIsPlaying()
        {
            return _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01 0");
        }

        // Optionally, visualize the detection radius in the editor
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _detectionRadius);
        }
    }
}
