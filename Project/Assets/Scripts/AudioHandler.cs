using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] AudioClip[] placeDownWoodSounds;
    [SerializeField] AudioClip[] placeDownStoneSounds;
    [SerializeField] AudioClip[] placeDownConcreteSounds;
    [SerializeField] AudioClip[] breakAttachmentSounds;
    [SerializeField] AudioClip[] attachSounds;
    [SerializeField] AudioClip[] deleteSounds;
    [SerializeField] AudioClip[] lightningStrikeSounds;
    [SerializeField] AudioClip[] meteorPoopSounds;
    [SerializeField] AudioClip[] dieSounds;
    [SerializeField] AudioClip[] painSounds;

    public AudioSource soundEffectAudioSource;
    [SerializeField] AudioSource earthquakeRumblingAS;
    [SerializeField] AudioSource windSoundAS;
    [SerializeField] AudioSource rainSoundAS;

    public AudioClip PainSound()
    {
        return painSounds[Random.Range(0, painSounds.Length)];
    }

    public AudioClip DieSound()
    {
        return dieSounds[Random.Range(0, dieSounds.Length)];
    }

    public AudioClip DeleteSound()
    {
        return deleteSounds[Random.Range(0, deleteSounds.Length)];
    }

    public AudioClip LightningStrikeSound()
    {
        return lightningStrikeSounds[Random.Range(0, lightningStrikeSounds.Length)];
    }

    public AudioClip MeteorPoopSounds()
    {
        return meteorPoopSounds[Random.Range(0, meteorPoopSounds.Length)];
    }

    public AudioSource EarthQuakeSoundAudioSource()
    {
        return earthquakeRumblingAS;
    }

    public AudioSource RainSoundAudioSource()
    {
        return rainSoundAS;
    }

    public AudioSource WindSoundAudioSource()
    {
        return windSoundAS;
    }

    public AudioClip PlaceDownWoodSound()
    {
        return placeDownWoodSounds[Random.Range(0, placeDownWoodSounds.Length)];
    }

    public AudioClip PlaceDownStoneSound()
    {
        return placeDownStoneSounds[Random.Range(0, placeDownStoneSounds.Length)];
    }

    public AudioClip PlaceDownConcreteSound()
    {
        return placeDownConcreteSounds[Random.Range(0, placeDownConcreteSounds.Length)];
    }

    public AudioClip BreakAttachmentSound()
    {
        return breakAttachmentSounds[Random.Range(0, breakAttachmentSounds.Length)];
    }

    public AudioClip AttachSound()
    {
        return attachSounds[Random.Range(0, attachSounds.Length)];
    }

}
