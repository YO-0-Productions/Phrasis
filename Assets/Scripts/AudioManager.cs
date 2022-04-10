using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private Sound lastPlayed;
    private Sound currentBackgroundMusic;



    public static AudioManager instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
             Debug.LogWarning($"The sound '{name}' not found.");
            return; 
        }
        s.source.Play();
        lastPlayed = s;
    }

    public void SetBackgroundMusic (string name)
    {
        if (currentBackgroundMusic != null)
        {
            Debug.Log("debug from SetBackgroundMusic");
            currentBackgroundMusic.source.Stop();
        }
        Play(name);
        currentBackgroundMusic = lastPlayed;
    }
}
