using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance => s_Instance;
    private static SaveManager s_Instance;

    const string k_SoundVolume = "SoundVolume";
    const string k_MusicVolume = "MusicVolume";
    const string k_Vibration = "Vibration";


    private void OnEnable()
    {
        SetupInstance();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Awake()
    {

    }

    private void SetupInstance()
    {
        if (Instance == null)
        {
            s_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

  
    #region Reset Prefs

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
    }

    #endregion

    #region Sound Music Volume / Vibration / Post Processing

    public float EffectsVolume
    {
        get => PlayerPrefs.GetFloat(k_SoundVolume, 1f);
        set => PlayerPrefs.SetFloat(k_SoundVolume, value);
    }

    public float MusicVolume
    {
        get => PlayerPrefs.GetFloat(k_MusicVolume, 1f);
        set => PlayerPrefs.SetFloat(k_MusicVolume, value);
    }
    
    public int Vibration
    {
        get => PlayerPrefs.GetInt(k_Vibration, 1);
        set => PlayerPrefs.SetInt(k_Vibration, value);
    }

    #endregion


}
