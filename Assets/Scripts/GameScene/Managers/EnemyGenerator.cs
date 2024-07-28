using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PoolManager;

namespace Game
{
    public class EnemyGenerator : MonoBehaviour
    {
        public static EnemyGenerator Instance => s_Instance;
        private static EnemyGenerator s_Instance;

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

        public GameObject SpawnEnemy()
        {
            List<PoolType> poolTypes = new List<PoolType>()
            {
                PoolType.enemy1,
            };

            PoolType poolType = poolTypes[Random.Range(0, poolTypes.Count)];

            GameObject enemy = PoolManager.Instance.GetObjectFromPool(poolType);
            return enemy;
        }
    }
}
