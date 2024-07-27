using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance => s_Instance;
        private static CameraManager s_Instance;

        [SerializeField] private Camera _mainCamera;

        private Vector3 _playerShift;

        public Camera MainCamera => _mainCamera;

        private void OnEnable()
        {
            SetupInstance();
        }

        private void Start()
        {
            if (Player.Instance != null)
            {
                _playerShift = transform.position - Player.Instance.transform.position;
            }
        }

        private void SetupInstance()
        {
            if (s_Instance != null && s_Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            s_Instance = this;
        }

        private void Update()
        {
            if (Player.Instance != null)
            {
                transform.position = Vector3.Lerp(transform.position, Player.Instance.transform.position + _playerShift, Time.deltaTime * 1f);
            }
        }
    }
}
