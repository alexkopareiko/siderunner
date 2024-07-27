using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    private void StartEvent(string methodName) {
        transform.root.SendMessage(methodName);
    }
}
