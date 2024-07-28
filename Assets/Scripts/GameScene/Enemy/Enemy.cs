using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private ActorStats _actorStats;
        [SerializeField] private EnemyCombat _enemyCombat;

        private Rigidbody _rb;
        private Animator _animator;

        public ActorStats ActorStats => _actorStats;
        public EnemyCombat EnemyCombat => _enemyCombat;

        private void OnEnable()
        {
            _rb = Helpers.CheckOnComponent<Rigidbody>(gameObject);
            _rb.useGravity = false;
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;

            _animator = Helpers.CheckOnComponent<Animator>(gameObject);
            _animator.SetTrigger("Reset");

            transform.rotation = Quaternion.identity;

            StartCoroutine(DisableCo());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator DisableCo()
        {

            yield return new WaitForSeconds(15f);
            gameObject.SetActive(false);
        }

    }
}
