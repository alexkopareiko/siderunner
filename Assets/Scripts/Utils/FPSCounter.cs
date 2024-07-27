using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class FPSCounter : MonoBehaviour 
{
    public TMP_Text Text;
    public Button _button;

    private Dictionary<int, string> CachedNumberStrings = new();

    private int[] _frameRateSamples;
    private int _cacheNumbersAmount = 300;
    private int _averageFromAmount = 30;
    private int _averageCounter;
    private int _currentAveraged;

    void Awake()
    {
        // Cache strings and create array
        {
            for (int i = 0; i < _cacheNumbersAmount; i++)
            {
                CachedNumberStrings[i] = i.ToString();
            }

            _frameRateSamples = new int[_averageFromAmount];
        }

        
    }

    private void Start()
    {
        //_button.onClick.AddListener(NavigationManager.ReloadScene);
        var currentScene = SceneManager.GetActiveScene();
        _button.onClick.AddListener(() => SceneManager.LoadScene(currentScene.name));
    }

    void Update()
    {
        // Sample
        {
            var currentFrame = (int)Math.Round(1f / Time.smoothDeltaTime); // Use unscaledDeltaTime for more accurate, or if your game modifies Time.timeScale.
            _frameRateSamples[_averageCounter] = currentFrame;
        }

        // Average
        {
            var average = 0f;

            foreach (var frameRate in _frameRateSamples)
            {
                average += frameRate;
            }

            _currentAveraged = (int)Math.Round(average / _averageFromAmount);
            _averageCounter = (_averageCounter + 1) % _averageFromAmount;
        }

        // Assign to UI
        {
            Text.text = _currentAveraged switch
            {
                var x when x >= 0 && x < _cacheNumbersAmount => CachedNumberStrings[x],
                var x when x >= _cacheNumbersAmount => $"> {_cacheNumbersAmount}",
                var x when x < 0 => "< 0",
                _ => "?"
            };
        }
    }
}