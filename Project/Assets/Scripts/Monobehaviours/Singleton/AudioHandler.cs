using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : Singleton<AudioHandler>
{
    [SerializeField] AudioSource generalSfxAudioSource;
    [SerializeField] AudioSource weatherSfxAudioSource;
    [SerializeField] AudioSource ambienceAudioSource;

    public void PlayGeneralSfx(Sound sound) => weatherSfxAudioSource.PlayOneShot(sound.RandomSound());
    public void PlayGeneralSfx(string soundName) => PlayGeneralSfx(DataLibrary.I.Sounds[soundName]);

    public void PlayWeatherSfx(Sound sound) => weatherSfxAudioSource.PlayOneShot(sound.RandomSound());
    public void PlayWeatherSfx(string soundName) => PlayGeneralSfx(DataLibrary.I.Sounds[soundName]);

    public void StopAmbient() => ambienceAudioSource.Stop();
    public void PlayAmbient(Sound sound) => ambienceAudioSource.PlayOneShot(sound.RandomSound());
}
