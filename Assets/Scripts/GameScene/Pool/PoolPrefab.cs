using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PoolManager;

public class PoolPrefab : MonoBehaviour
{
    [SerializeField] private PoolType _poolType;
    [SerializeField] private int _poolSize = 10;

    public PoolType poolType => _poolType;
    public int poolSize => _poolSize;
}
