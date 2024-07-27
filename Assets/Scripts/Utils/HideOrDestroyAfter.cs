using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOrDestroyAfter : MonoBehaviour
{
    private float _timeToHideOrDestroy = 0.0f;

    public enum ToDoType
    {
        hide,
        destroy
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void Initialize(float time, ToDoType todo = ToDoType.hide)
    {
        _timeToHideOrDestroy = time;

        if (todo == ToDoType.destroy)
        {
            StartCoroutine(DestroyAfter());
        }
        else
        {
            StartCoroutine(HideAfter());
        }
    }

    private IEnumerator HideAfter()
    {
        yield return new WaitForSeconds(_timeToHideOrDestroy);
        //Destroy(this);
        gameObject.SetActive(false);
    }

    private IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(_timeToHideOrDestroy);
        Destroy(gameObject);
    }
}
