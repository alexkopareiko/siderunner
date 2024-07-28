using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ActorStats _actorStats;
    [SerializeField] private EnemyCombat _enemyCombat;

    private Rigidbody _rb;

    public ActorStats ActorStats => _actorStats;
    public EnemyCombat EnemyCombat => _enemyCombat;

    private void OnEnable()
    {
        _rb = Helpers.CheckOnComponent<Rigidbody>(gameObject);
        _rb.useGravity = false;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }




}
