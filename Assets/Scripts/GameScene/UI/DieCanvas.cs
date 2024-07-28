using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class DieCanvas : MonoBehaviour
    {
        public static DieCanvas Instance => s_Instance;
        private static DieCanvas s_Instance;

        [SerializeField] private Button _reloadButton;

        private void OnEnable()
        {
            SetupInstance();

            StartCoroutine(PauseCor());
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

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private void Start()
        {
            _reloadButton.onClick.AddListener(ReloadScene);
        }

        private IEnumerator PauseCor()
        {
            yield return new WaitForSeconds(1f);
            GameManager.Instance.Pause(true);
        }

        private void ReloadScene()
        {
            var currentScene = SceneManager.GetActiveScene();
            GameManager.Instance.Pause(false);
            SceneManager.LoadScene(currentScene.name);
        }


    }
}
