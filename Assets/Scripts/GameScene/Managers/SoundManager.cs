using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance => s_Instance;
    private static SoundManager s_Instance;

    [Serializable]
    public class SoundButtonClipPair
    {
        public ButtonUIType m_type;
        public AudioClip m_audioClip;
    }

    public enum ButtonUIType
    {
        regular,
        cancel,
        buy
    }

    [Header("Clips")]
    [SerializeField] private List<SoundButtonClipPair> _buttonClipPairs = new();
    [SerializeField] private AudioClip _gameTheme;
    [SerializeField] private List<AudioClip> _dieClips;

    [Header("Music")]
    [SerializeField] private AudioClip _gameClip;

    [Header("Mixer")]
    [SerializeField] private AudioMixer _audioMixer;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _soundEffectSource;
    [SerializeField] private AudioSource _musicSource1;
    [SerializeField] private AudioSource _musicSource2; 

    [Header("Other")]
    [SerializeField] private float _fadeTime = 1.0f;
    [SerializeField] private float _soundInterval = 0.01f;

    private AudioSource _currentMusicSource;
    private AudioSource _nextMusicSource;
    private bool _isCrossfading;
    private float _soundPlayedTime;

    private void OnEnable()
    {
        if (s_Instance != null && s_Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        s_Instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Start()
    {
        _currentMusicSource = _musicSource1;
        _nextMusicSource = _musicSource2;

        _currentMusicSource.loop = true;
        _nextMusicSource.loop = true;

        // Set initial volume levels
        SetMusicVolume(SaveManager.Instance.MusicVolume);
        SetSoundEffectVolume(SaveManager.Instance.EffectsVolume);

        PlayMusic(_gameTheme);
    }

    #region General


    // Play a sound effect
    public void PlaySoundEffect(AudioClip clip)
    {
        if (clip == null || (_soundPlayedTime + _soundInterval > Time.time))
        {
            return;
        }
        _soundEffectSource.PlayOneShot(clip);
        _soundPlayedTime = Time.time;
    }

    public void PlaySoundEffect(AudioClip clip, float volume)
    {
        if (clip == null || (_soundPlayedTime + _soundInterval > Time.time))
        {
            return;
        }
        _soundEffectSource.PlayOneShot(clip, volume);
        _soundPlayedTime = Time.time;
    }

    // Play background music
    public void PlayMusic(AudioClip clip)
    {
        if (clip == null)
            return;

        Crossfade(clip);
    }

    // Stop playing background music
    public void PauseMusic()
    {
        _musicSource1.Pause();
        _musicSource2.Pause();
    }

    public void UnPauseMusic()
    {
        _musicSource1.UnPause();
        _musicSource2.UnPause();
    }

    private void Crossfade(AudioClip musicClip)
    {
        /*if (_isCrossfading)
            return;*/
        StopAllCoroutines();
        StartCoroutine(CrossfadeCoroutine(musicClip));
    }

    private IEnumerator CrossfadeCoroutine(AudioClip musicClip)
    {
        _isCrossfading = true;

        // Ensure the next music source is playing
        _nextMusicSource.clip = musicClip;
        _nextMusicSource.Play();

        // Fade out the current music source and fade in the next music source simultaneously
        float currentTime = 0.0f;
        float startVolume = 1f;
        while (currentTime < _fadeTime)
        {
            currentTime += Time.unscaledDeltaTime;
            _currentMusicSource.volume = Mathf.Lerp(startVolume, 0, currentTime / _fadeTime);
            _nextMusicSource.volume = Mathf.Lerp(0, startVolume, currentTime / _fadeTime);
            yield return null;
        }

        // Stop the current music source and set the volume back to its original value
        _currentMusicSource.Stop();
        _currentMusicSource.volume = startVolume;

        // Swap the current and next music sources
        AudioSource temp = _currentMusicSource;
        _currentMusicSource = _nextMusicSource;
        _nextMusicSource = temp;

        _isCrossfading = false;
    }


    // Set the volume of sound effects
    public void SetSoundEffectVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0.001f, 1);
        _audioMixer.SetFloat("EffectsVolume", Mathf.Log10(volume) * 20);
        SaveManager.Instance.EffectsVolume = volume;
    }

    // Set the volume of background music
    public void SetMusicVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0.001f, 1);
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        SaveManager.Instance.MusicVolume = volume;
    }

    #endregion


    #region Play Sound Effects

    public void PlayButtonSound(ButtonUIType type)
    {
        AudioClip _buttonClip = _buttonClipPairs.Find(x => x.m_type == type).m_audioClip;
        PlaySoundEffect(_buttonClip);
        Vibrate();
    }

    public void PlayDieSound()
    {
        AudioClip audioClip = _dieClips[UnityEngine.Random.Range(0, _dieClips.Count)];
        PlaySoundEffect(audioClip);
    }

    public void Vibrate(int durationMilis = 10)
    {
        if (SaveManager.Instance.Vibration == 0)
            return;

        long[] vibrationPattern = { 0, durationMilis };

        /*if (SaveManager.Instance.Vibration == 1)*/
        if (Application.platform == RuntimePlatform.Android)
        {

            // Get the current activity
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            // Get the vibrator service from the current activity
            AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");

            // Check if the vibrator service exists
            if (vibrator != null)
            {
                // Vibrate with the specified pattern
                vibrator.Call("vibrate", vibrationPattern, -1);
            }
            else
            {
                Debug.LogWarning("Vibrator service not found.");
            }
        }
        else
        {
            //Debug.LogWarning("Vibration only supported on Android.");
        }
    }

    #endregion


}
