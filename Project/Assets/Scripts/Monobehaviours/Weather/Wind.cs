using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : DisasterBehavior
{
    [SerializeField] float changeDirectionInterval;
    [SerializeField] float windVisualInterval;
    [SerializeField] GameObject windVisualPrefab;
    [SerializeField] AudioSource audioSource;
    [SerializeField] float windIntensity;

    int windDirection;

    public override void Begin()
    {
        audioSource.Play();
        StartCoroutine(ChangeDirection());
    }

    public override void Stop()
    {
        audioSource.Stop();
        StopAllCoroutines();
    }

    IEnumerator ChangeDirection()
    {
        while (true)
        {
            if (Random.Range(0, 2) == 0)
                windDirection = 1;
            else
                windDirection = -1;

            yield return new WaitForSeconds(changeDirectionInterval);
        }
    }

    IEnumerator ShowWind()
    {
        while (true)
        {
            Instantiate(windVisualPrefab, new Vector3(Random.Range(-10f, 10f), Random.Range(-4f, 4.5f)), Quaternion.identity);
            yield return new WaitForSeconds(windVisualInterval);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (rb)
            rb.velocity += new Vector2(Time.deltaTime * windIntensity * windDirection, 0);
    }
}
