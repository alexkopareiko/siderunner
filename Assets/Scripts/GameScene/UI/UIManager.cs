using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance => s_Instance;
        private static UIManager s_Instance;

        [Header("Canvases")]
        [SerializeField] private GameObject _playCanvas;
        [SerializeField] private GameObject _dieCanvas;
        [SerializeField] private GameObject _menuCanvas;


        private List<GameObject> _canvases = new List<GameObject>();

        private void OnEnable()
        {
            SetupInstance();

            _canvases.Add(_playCanvas);
            _canvases.Add(_dieCanvas);
            _canvases.Add(_menuCanvas);

            ShowPlayCanvas();
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

        private void ShowCanvas(GameObject canvas)
        {
            foreach (var item in _canvases)
                item.SetActive(item == canvas);
        }

        public void ShowPlayCanvas()
        {
            ShowCanvas(_playCanvas);
        }

        public void ShowDieCanvas()
        {
            ShowCanvas(_dieCanvas);
        }

        public void ShowMenuCanvas()
        {
            ShowCanvas(_menuCanvas);
        }

    }

}