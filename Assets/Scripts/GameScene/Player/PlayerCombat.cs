using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _animator.SetTrigger("Attack");
                _animator.SetInteger("AttackType", Random.Range(0, 2));
            }
        }
    }
}
