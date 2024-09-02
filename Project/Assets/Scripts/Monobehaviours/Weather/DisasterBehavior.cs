using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DisasterBehavior : MonoBehaviour
{
    [SerializeField] DisasterType disaster;
    [SerializeField] float duration;

    public abstract void Begin();
    public abstract void Stop();

    public float Duration => duration;
}
