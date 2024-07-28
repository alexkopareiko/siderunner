using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSequencer : MonoBehaviour
{
    [SerializeField] private List<GameObject> _gameObjects = new List<GameObject>();

    private void Awake()
    {
        foreach (var item in _gameObjects)
        {
            Instantiate(item);
        }
    }
}
