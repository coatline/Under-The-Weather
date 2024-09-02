using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisasterController : MonoBehaviour
{
    [SerializeField] DisasterBehavior[] disasters;

    public DisasterBehavior CurrentDisaster { get; private set; }

    public void StartRandomDisaster()
    {
        CurrentDisaster = disasters[Random.Range(0, disasters.Length)];
        CurrentDisaster.Begin();
    }

    public void StopDisaster()
    {
        if (CurrentDisaster != null)
            CurrentDisaster.Stop();
    }
}
