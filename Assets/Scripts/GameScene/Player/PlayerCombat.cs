using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Collider _axeCollider;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _animator.SetTrigger("Attack");
                _animator.SetInteger("AttackType", Random.Range(0, 2));
            }

            _axeCollider.enabled = IsAttacking();
        }

        public bool IsAttacking()
        {
            return _animator.GetCurrentAnimatorStateInfo(1).IsName("Attack01") || _animator.GetCurrentAnimatorStateInfo(1).IsName("Attack02");
        }
    }
}

