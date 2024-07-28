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
        }

        private IEnumerator DisableMe()
        {
            yield return new WaitForSeconds(3f);
            gameObject.SetActive(false);
        }
    }
}
