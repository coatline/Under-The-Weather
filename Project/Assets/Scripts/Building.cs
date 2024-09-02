using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] Sound soundOnPlace;
    [SerializeField] Sound soundOnBreak;
    [SerializeField] float maxHp;
    [SerializeField] int cost;

    float hp;

    void Start()
    {
        hp = maxHp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hp--;
    }
}
