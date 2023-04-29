using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private IEnumerator _GradualIncreaseVolume;
    private Sound audio;
    private float increaseAmount;

    private void Awake()
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


    private void Start()
    {
        _GradualIncreaseVolume = GradualIncreaseVolume();
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

    
    public void GradualVolumePlay(string name)
    {
        Sound audio = Array.Find(sounds, sound => sound.name == name);
        if (audio == null)
        {
            Debug.LogWarning($"Sound: {name} is not found");
            return;
        }
        audio.source.Play();


        StartIncreaseVolume(audio, 0.0005f);
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


    private void StartIncreaseVolume(Sound audio, float increaseAmount)
    {
        this.audio = audio;
        this.increaseAmount = increaseAmount;
        _GradualIncreaseVolume = GradualIncreaseVolume();
        StartCoroutine(_GradualIncreaseVolume);
    }

    
    private IEnumerator GradualIncreaseVolume()
    {
        audio.source.volume = 0f;
        while (true)
        {
            audio.source.volume += increaseAmount;
            yield return null;
            if (audio.volume <= audio.source.volume)
            {
                break;
            }
        }
    }
}
