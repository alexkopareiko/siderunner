using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Game
{
    public class MenuCanvas : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _reloadButton;
        [SerializeField] private Button _soundButton;
        [SerializeField] private Button _noSoundButton;
        [SerializeField] private TMP_Text _scoreText;

        private void OnEnable()
        {
            GameManager.Instance.Pause(true);
            SoundManager.Instance.PauseMusic();

            SetSoundButton();

            _scoreText.text = SaveManager.Instance.Score.ToString();
        }

        private void OnDisable()
        {
            GameManager.Instance.Pause(false);
            SoundManager.Instance.UnPauseMusic();
        }

        private void Awake()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _reloadButton.onClick.AddListener(OnReloadButtonClicked);
            _soundButton.onClick.AddListener(OnSoundButtonClicked);
            _noSoundButton.onClick.AddListener(OnSoundButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            UIManager.Instance.ShowPlayCanvas();
        }

        private void OnReloadButtonClicked()
        {
            var currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }

        private void OnSoundButtonClicked()
        {
            float currentMusicVolume = SaveManager.Instance.MusicVolume;
            if (currentMusicVolume == 0)
            {
                SoundManager.Instance.SetMusicVolume(1);
                SoundManager.Instance.SetSoundEffectVolume(1);
                return;
            }
            else if (currentMusicVolume == 1)
            {
                SoundManager.Instance.SetMusicVolume(0);
                SoundManager.Instance.SetSoundEffectVolume(0);
                return;
            }

            SetSoundButton();
        }

        private void SetSoundButton()
        {
            if (SaveManager.Instance.MusicVolume == 0)
            {
                _soundButton.gameObject.SetActive(false);
                _noSoundButton.gameObject.SetActive(true);
            }
            else
            {
                _soundButton.gameObject.SetActive(true);
                _noSoundButton.gameObject.SetActive(false);
            }
        }
    }
}
