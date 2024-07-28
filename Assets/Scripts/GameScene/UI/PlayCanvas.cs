using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game
{
    public class PlayCanvas : MonoBehaviour
    {
        public static PlayCanvas Instance => s_Instance;
        private static PlayCanvas s_Instance;

        [SerializeField] private Slider _sliderHealth;
        [SerializeField] private Button _menuButton;
        [SerializeField] private TMP_Text _scoreText;

        private void OnEnable()
        {
            SetupInstance();
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

        private void Start()
        {

        }

        public void SetHealth(float health, float maxHealth)
        {
            _sliderHealth.value = health;
            _sliderHealth.maxValue = maxHealth;
        }

        public void SetScore(int score)
        {
            _scoreText.text = score.ToString();
        }
    }
}
