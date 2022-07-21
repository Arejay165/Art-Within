using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

[CreateAssetMenu(menuName = "Sound Event")]
public class SoundEvent : Sounds
{
    public AudioClip[] clips;
    public AudioMixer mixer;
    public override void PlayAudio(AudioSource source, int audioIndex)
    { 
        source.clip = clips[audioIndex];
        source.PlayOneShot(clips[audioIndex]);
    }
}
