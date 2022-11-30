using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSound : MonoBehaviour
{
    [SerializeField] private AudioSO _audio;
    [SerializeField] private string[] _soundsToPlay;

    private void Start()
    {
        _audio.StopAllExcept(_soundsToPlay);

        foreach (var soundToPlay in _soundsToPlay)
        {
            if (!_audio.IsPlaying(soundToPlay)) { _audio.Play(soundToPlay); }
        }
    }
}
