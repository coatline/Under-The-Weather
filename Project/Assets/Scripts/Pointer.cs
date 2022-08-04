using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public GameObject currentOverGob;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) { return; }
        currentOverGob = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == currentOverGob)
        {
            currentOverGob = null;
        }
    }

    private void Update()
    {
        transform.localScale = new Vector3(C.size, C.size, C.size);

        var rot = Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime;

        if (rot > 0)
        {
            transform.Rotate(new Vector3(0, 0, -7.5f));
        }
        else if (rot < 0)
        {
            transform.Rotate(new Vector3(0, 0, 7.5f));
        }
    }
}