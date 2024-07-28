using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ActorStats : MonoBehaviour
    {
        public enum ActorType
        {
            player,
            enemy,
            //boss
        }

        public enum DamageType
        {
            regular,
            critical,
            fire,
            frost
        }

        [SerializeField] protected float _health = 100.0f;
        [SerializeField] protected float _maxHealth = 100.0f;
        [SerializeField] protected float _damage = 10.0f;
        [SerializeField] protected ActorType _actorType;

        private Animator _animator;

        public ActorType actorType => _actorType;
        public float health => _health;
        public float maxHealth => _maxHealth;
        public float damage => _damage;

        public event Action<float> OnHealthChanged;

        protected virtual void OnEnable()
        {
            gameObject.layer = LayerMask.NameToLayer("Actor");
            _animator = Helpers.CheckOnComponent<Animator>(gameObject);

            SetStats();
        }

        protected void OnDisable()
        {
            StopAllCoroutines();
        }

        protected virtual void Start()
        {

        }

        public virtual void SetStats() { }

        public virtual void TakeDamage(float damage, DamageType damageType = DamageType.regular)
        {
            if (gameObject.activeSelf == false || _health <= 0)
                return;

            Health -= damage;

            Debug.Log(gameObject.name + "ouch");

            if (_health <= 0)
            {
                Die();
            }
            else
                _animator.SetTrigger("GetHit");

        }

        public virtual void Die()
        {
            _health = 0;
            _animator.SetTrigger("Die");
            Debug.Log(gameObject.name + " die");
        }

        protected virtual float Health
        {
            get
            {
                return _health;
            }
            set
            {
                _health = value;
                OnHealthChanged?.Invoke(_health);
            }
        }
    }

}

