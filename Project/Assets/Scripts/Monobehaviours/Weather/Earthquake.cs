using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthquake : DisasterBehavior
{
    [SerializeField] AudioSource audioSource;
    Animator groundAnimator;

    private void Start()
    {
        groundAnimator = Extensions.FindObjectOfNameFromArray("Ground", FindObjectsOfType<Animator>()) as Animator;
    }

    public override void Begin()
    {
        //mainCamAni.Play("CameraShake");
        audioSource.Play();
        groundAnimator.enabled = true;
    }

    public override void Stop()
    {
        audioSource.Stop();
        groundAnimator.enabled = false;
    }
}
