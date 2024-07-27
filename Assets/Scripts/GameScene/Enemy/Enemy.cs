using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ActorStats _actorStats;
    [SerializeField] private EnemyCombat _enemyCombat;

    public ActorStats ActorStats => _actorStats;
    public EnemyCombat EnemyCombat => _enemyCombat;


}
