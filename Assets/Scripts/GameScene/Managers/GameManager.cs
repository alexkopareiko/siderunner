using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance => s_Instance;
        private static GameManager s_Instance;

        private int _score;

        private void OnEnable()
        {
            SetupInstance();
        }

        private void Start()
        {
            AddScore(0);
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

        public void AddScore(int score)
        {
            _score += score;
            PlayCanvas.Instance.SetScore(_score);
        }

        public void GameOver()
        {
            UIManager.Instance.ShowDieCanvas();
        }

        public void Pause(bool value)
        {
            Time.timeScale = value ? 0 : 1;
        }
    }
}