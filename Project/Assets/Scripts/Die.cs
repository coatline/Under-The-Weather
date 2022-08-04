using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    [SerializeField] float timeToDie;

    void Start()
    {
        Invoke("DIE", timeToDie);       
    }

    void DIE()
    {
        Destroy(gameObject);
    }
}
