using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerStats : ActorStats
    {
        protected override void OnEnable()
        {
            base.OnEnable();

            OnHealthChanged += CallUI;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            OnHealthChanged -= CallUI;
        }

        public override void Die()
        {
            base.Die();
            GameManager.Instance.GameOver();
        }

        private void CallUI(float val)
        {
            PlayCanvas.Instance.SetHealth(val, maxHealth);
        }
    }
}
