using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    public class EnemyStats : ActorStats
    {
        public override void SetStats()
        {
            _health = _maxHealth;
        }

        public override void Die()
        {
            base.Die();
            StartCoroutine(DisableMe());
            GameManager.Instance.AddScore(10);
        }

        private IEnumerator DisableMe()
        {
            yield return new WaitForSeconds(3f);
            gameObject.SetActive(false);
        }

        public override void TakeDamage(float damage, DamageType damageType = DamageType.regular)
        {
            base.TakeDamage(damage, damageType);
            
        }

    }
}
