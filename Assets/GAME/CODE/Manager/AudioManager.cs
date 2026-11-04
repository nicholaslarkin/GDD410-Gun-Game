using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public SoundSingle[] soundSingles;
    public SoundList[] soundLists;
    public AudioSource sfxPlayer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /*public void PlayMusic(string name)
    {
        Sound soundToPlay = Array.Find(sounds, x => x.name == name);

        if (soundToPlay == null)
            Debug.LogError("Sound Not Found!");
        else
        {
            sfxPlayer.clip = soundToPlay.sound;
            sfxPlayer.Play();
        }
    }*/

    public void PlaySFX(string name, float volumeLevel)
    {
        SoundSingle soundToPlay = Array.Find(soundSingles, x => x.name == name);

        if (soundToPlay == null)
            Debug.LogError("Sound Not Found!");
        else
        {
            sfxPlayer.PlayOneShot(soundToPlay.soundSingle, volumeLevel);
        }
    }

    public void PlaySFXMult(string name, float volumeLevel)
    {
        SoundList soundList = Array.Find(soundLists, x => x.name == name);

        int soundToPlay = UnityEngine.Random.Range(0, soundList.soundMult.Length);

        if (soundList == null)
            Debug.LogError("Sound Not Found!");
        else
        {
            sfxPlayer.PlayOneShot(soundList.soundMult[soundToPlay], volumeLevel);
        }
    }
}

[System.Serializable]
public class SoundSingle
{
    public string name;
    public AudioClip soundSingle;
}

[System.Serializable]
public class SoundList
{
    public string name;
    public AudioClip[] soundMult;
}
