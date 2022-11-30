using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Others/AudioSO")]
public class AudioSO : ScriptableObject
{
    public List<Sound> sounds;
    public List<string> currentSounds = new List<string>();

    public void Play(string soundName)
    {
        var sound = sounds.Find(sound => sound.name == soundName);
        if (sound != null)
        {
            currentSounds.Add(sound.name);
            sound.source.Play();
        }
#if UNITY_EDITOR
        else
        {
            Debug.LogWarning("Son : " + soundName + " not found!");
        }
#endif
    }

    public void PlaySFX(string soundName)
    {
        var sound = sounds.Find(sound => sound.name == soundName);
        if (sound != null)
        {
            sound.source.Play();
        }
#if UNITY_EDITOR
        else
        {
            Debug.LogWarning("SFX : " + soundName + " not found!");
        }
#endif
    }

    public void Stop(string soundName)
    {
        var sound = sounds.Find(sound => sound.name == soundName);
        if (sound != null && currentSounds.Contains(sound.name))
        {
            currentSounds.Remove(sound.name);
            sound.source.Stop();
        }
#if UNITY_EDITOR
        else if (sound == null)
        {
            Debug.LogWarning("Son : " + soundName + " not found!");
        }
        else
        {
            Debug.LogWarning("Son " + soundName + " is not playing!");
        }
#endif
    }

    public void StopAll()
    {
        if (currentSounds.Count == 0) { return; }

        foreach (var sound in currentSounds.Select(currentSound => sounds.Find(sound => sound.name == currentSound)))
        {
            sound.source.Stop();
        }
        currentSounds.Clear();
    }

    public void StopAllExcept(string[] songsToPlay)
    {
        if (currentSounds.Count == 0) { return; }

        // Take apart all musics to stop
        var songsToStop = currentSounds.Where(currentSound => !songsToPlay.Contains(currentSound)).ToList();

        // Stop all musics
        foreach (var songToStop in songsToStop)
        {
            var sound = sounds.Find(sound => sound.name == songToStop);
            sound.source.Stop();
            currentSounds.Remove(songToStop);
        }
    }

    public bool IsPlaying(string soundName)
    {
        return currentSounds.Count != 0 && currentSounds.Contains(soundName);
    }
}

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 2f)]
    public float pitch;
    public bool loop;
    [HideInInspector]
    public AudioSource source;
}