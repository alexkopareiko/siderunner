using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private LayerMask _actorLayer;
        [SerializeField] private AudioClip _hitEnd;


        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log(_actorLayer);
            if ((_actorLayer.value & (1 << collision.gameObject.layer)) != 0)
            {
                Enemy enemy = Helpers.CheckOnComponent<Enemy>(collision.gameObject);
                if (enemy != null)
                {
                    float damage = Player.Instance.ActorStats.damage;
                    enemy.ActorStats.TakeDamage(damage);

                    Rigidbody rigidbody = Helpers.CheckOnComponent<Rigidbody>(collision.gameObject);
                    if (rigidbody != null)
                    {
                        Vector3 directionToEnemy = (collision.transform.position - transform.position).normalized;
                        rigidbody.AddForce(directionToEnemy * 30f, ForceMode.Impulse);
                        rigidbody.useGravity = true;
                    }

                    GameObject hit = PoolManager.Instance.GetObjectFromPool(PoolManager.PoolType.hit1);
                    hit.transform.position = collision.contacts[0].point;
                    hit.SetActive(true);

                    SoundManager.Instance.PlaySoundEffect(_hitEnd);
                }
            }
        }
    }
}
