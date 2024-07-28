using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance => s_Instance;
    private static PoolManager s_Instance;

    private Dictionary<PoolType, ObjectPool> _pools = new ();

    [Header("Enemy Prefabs")]
    [SerializeField] private List<PoolPrefab> _enemyPrefabs = new();

    [Header("Hit Prefabs")]
    [SerializeField] private List<PoolPrefab> _hitPrefabs = new();

    [Header("Other")]
    [SerializeField] private PoolPrefab _otherPrefabs = new();

    public enum PoolType
    {
        enemy1 = 1,

        hit1 = 2,

    }

    private void OnEnable()
    {
        if (s_Instance != null && s_Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        s_Instance = this;

        //DontDestroyOnLoad(this.gameObject);

        Initialize();
    }

    private void Initialize()
    {
        List<PoolPrefab> all = new ();

        all.AddRange(_enemyPrefabs);
        all.AddRange(_hitPrefabs);
        //all.Add(_otherPrefabs);

        foreach (PoolPrefab prefab in all)
            CreatePool(prefab.poolType, prefab, prefab.poolSize);
    }

    public void CreatePool(PoolType key, PoolPrefab prefab, int size = 5)
    {
        if (!_pools.ContainsKey(key))
        {
            GameObject poolObject = new GameObject(key + "Pool");
            poolObject.transform.parent = transform;

            ObjectPool objectPool = poolObject.AddComponent<ObjectPool>();
            prefab.gameObject.SetActive(false);
            objectPool.Initialize(prefab, size);

            _pools.Add(key, objectPool);
        }
        else
        {
            Debug.LogWarning("Pool with key " + key + " already exists.");
        }
    }

    /// <summary>
    /// Returns deactivated object from the pool
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public GameObject GetObjectFromPool(PoolType key)
    {
        if (_pools.ContainsKey(key))
        {
            return _pools[key].GetObjectFromPool();
        }

        Debug.LogWarning("Pool with key " + key + " not found.");
        return null;
    }

    public void ReturnObjectToPool(PoolType key, GameObject obj)
    {
        if (_pools.ContainsKey(key))
        {
            _pools[key].ReturnObjectToPool(obj);
        }
        else
        {
            Debug.LogWarning("Pool with key " + key + " not found.");
        }
    }

    public void RemoveObjectFromPool(PoolType key, GameObject obj)
    {
        if (_pools.ContainsKey(key))
        {
            _pools[key].RemoveFromPool(obj);
        }
        else
        {
            Debug.LogWarning("Pool with key " + key + " not found.");
        }
    }
}
