using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager: Singleton<SoundManager>
{
    private AudioSource aSource;
    public AudioSource ASource
    {
        get { return aSource == null ? aSource = GetComponent<AudioSource>() : aSource; }
        set { aSource = value; }
    }
    public float pitchValue = 0;
    public enum Sound
    {
        Slice,
        Classic
    }

    public void PlaySound(Sound sound)
    {
        if (sound == Sound.Classic)
        {
            pitchValue += .05f;
        }
        else
        {
            pitchValue = 1;
        }
        ASource.clip = GetAudioClip(sound);
        ASource.pitch = pitchValue;
        ASource.Play();
    }

    private AudioClip GetAudioClip(Sound sound)
    {
        foreach (GameManager.SoundAudioClip item in GameManager.Instance.soundAudioClipArray)
        {
            if (sound == item.sound)
            {
                return item.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }
}
