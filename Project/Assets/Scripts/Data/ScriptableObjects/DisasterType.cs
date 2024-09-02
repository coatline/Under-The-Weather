using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Disaster", fileName = "Disaster")]

public class DisasterType : ScriptableObject
{
    [SerializeField] AudioClip ambience;
    [SerializeField] Sprite icon;
}
