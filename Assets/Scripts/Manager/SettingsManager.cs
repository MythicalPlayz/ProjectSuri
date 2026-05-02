using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        // check if playerprefabs exist, if not create them with default values
        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 1f);
        }
        if (!PlayerPrefs.HasKey("SFXVolume"))
        {
            PlayerPrefs.SetFloat("SFXVolume", 1f);
        }
        if (!PlayerPrefs.HasKey("GameTime"))
        {
            PlayerPrefs.SetInt("GameTime", 300);
        }
    }

    // Update is called once per frame
    public void UpdateMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    public void UpdateSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }

    public void UpdateGameTime(int time)
    {
        PlayerPrefs.SetInt("GameTime", time);
        PlayerPrefs.Save();
    }
}
