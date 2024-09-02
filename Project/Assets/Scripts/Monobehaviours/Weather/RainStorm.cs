using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainStorm : DisasterBehavior
{
    [SerializeField] ParticleSystem rainParticles;
    [SerializeField] float timeBetweenLighting;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] AudioSource audioSource;
    [SerializeField] LayerMask windLayer;

    public override void Begin()
    {
        rainParticles.Play();
        audioSource.Play();

        StartCoroutine(DoLightningInterval());
    }

    public override void Stop()
    {
        rainParticles.Stop();
        audioSource.Stop();
        StopAllCoroutines();
    }

    IEnumerator DoLightningInterval()
    {
        while (true)
        {
            SpawnLightning();
            yield return new WaitForSeconds(timeBetweenLighting);
        }
    }

    void SpawnLightning()
    {
        //mainCamAni.Play("Flash");

        Vector2 position = new Vector2(Random.Range(-8f, 8f), 7);
        Vector2 direction = Vector2.down;
        float distance = 30;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, ~(1 << windLayer));

        Instantiate(explosionPrefab, hit.point, Quaternion.identity);

        SoundManager.I.PlaySound("Lightning Strike", hit.point);
    }
}
