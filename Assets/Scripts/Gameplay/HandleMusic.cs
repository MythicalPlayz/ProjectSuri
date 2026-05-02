using UnityEngine;

public class HandleMusic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private AudioSource audioSource;
    public enum MusicType
    {
        Music,
        SFX,
    }
    public MusicType type;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        float musicVolume;
        if (type == MusicType.Music)
        {
            musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        }
        else
        {
            musicVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        }
        audioSource.volume = musicVolume;
    }
}
