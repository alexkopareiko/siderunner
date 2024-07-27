using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Platform : MonoBehaviour
    {
        private bool _activated = false;

        public bool Activated => _activated;

        public void Activate ()
        {
           _activated = true;
        }
    }
}
