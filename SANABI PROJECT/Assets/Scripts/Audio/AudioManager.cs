using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound element in sounds)
        {
            element.source = gameObject.AddComponent<AudioSource>();
            element.source.clip = element.clip;
            element.source.volume = element.volume;
            element.source.pitch = element.pitch;
            element.source.loop = element.loop;
        }
    }

    public void Play(string name)
    {
        Sound audio = Array.Find(sounds, sound => sound.name == name);
        if (audio == null)
        {
            Debug.LogWarning($"Sound: {name} is not found");
            return;
        }
        audio.source.Play();
    }

    public void Stop(string name)
    {
        Sound audio = Array.Find(sounds, sound => sound.name == name);
        if (audio == null)
        {
            Debug.LogWarning($"Sound: {name} is not found");
            return;
        }
        audio.source.Stop();
    }

}
