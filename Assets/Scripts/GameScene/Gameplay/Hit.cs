using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    public class Hit : MonoBehaviour
    {
        private void OnEnable()
        {
            StartCoroutine(Disable());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator Disable()
        {
            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false);
        }
    }
}

