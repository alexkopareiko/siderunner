using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour
    {
        public static Player Instance => s_Instance;
        private static Player s_Instance;

        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerCombat _playerCombat;
        [SerializeField] private ActorStats _actorStats;

        public PlayerMovement PlayerMovement => _playerMovement;
        public PlayerCombat PlayerCombat => _playerCombat;
        public ActorStats ActorStats => _actorStats;

        private void OnEnable()
        {
            SetupInstance();
        }

        private void SetupInstance()
        {
            if (s_Instance != null && s_Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            s_Instance = this;
        }
    }
}
