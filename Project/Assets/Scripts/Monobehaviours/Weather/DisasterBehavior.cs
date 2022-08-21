using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DisasterBehavior : MonoBehaviour
{
    [SerializeField] Disaster disaster;

    public abstract void Begin();
    public abstract void Stop();
}
