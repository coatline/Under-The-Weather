using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weather : MonoBehaviour
{
    public enum State
    {
        lightningStorm,
        rainingMeteors,
        earthquake,
        windy
    }

    [SerializeField] GameObject explosionPrefab;
    [SerializeField] GameObject meteorPrefab;
    [SerializeField] GameObject windPrefab;
    [SerializeField] GameObject ground;

    [SerializeField] float timeBetweenLighting;
    [SerializeField] float timeBetweenMeteors;
    [SerializeField] float timeBetweenWinds;
    [SerializeField] float windIntensity;
    public float disasterDuration;

    [SerializeField] ParticleSystem rain;

    public State currentState;
    public State prevState;

    IEnumerator Lightning()
    {
        if (!rain.isPlaying)
        {
            rain.Play();
            //audioHandler.RainSoundAudioSource().Play();
        }

        //mainCamAni.Play("Flash");

        Invoke("Explode", .15f);

        yield return new WaitForSeconds(timeBetweenLighting);

        StartCoroutine(Lightning());
    }

    void Explode()
    {
        Vector2 position = new Vector2(Random.Range(-8f, 8f), 7);
        Vector2 direction = Vector2.down;
        float distance = 30;

        //RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, ~(1 << windLayer));

        //Instantiate(explosionPrefab, hit.point, Quaternion.identity);

        //audioHandler.soundEffectAudioSource.PlayOneShot(audioHandler.LightningStrikeSound());
    }

    void Earthquake()
    {
        //mainCamAni.Play("CameraShake");
        ground.GetComponent<Animator>().enabled = true;
        //audioHandler.EarthQuakeSoundAudioSource().Play();
    }

    IEnumerator Wind()
    {
        Instantiate(windPrefab, new Vector3(Random.Range(-10f, 10f), Random.Range(-4f, 4.5f)), Quaternion.identity);

        windDir = 0;

        while (windDir == 0)
        {
            windDir = Random.Range(-1, 2);
        }

        yield return new WaitForSeconds(timeBetweenWinds + Random.Range(-1f, 1f));

        StartCoroutine(Wind());
    }

    IEnumerator RainMeteor()
    {
        //audioHandler.soundEffectAudioSource.PlayOneShot(audioHandler.MeteorPoopSounds());

        var meteor = Instantiate(meteorPrefab, new Vector3(Random.Range(-8f, 8f), 8f), Quaternion.Euler(0, 0, Random.Range(0, 361)), transform);

        var mRb = meteor.GetComponent<Rigidbody2D>();
        mRb.AddForce(new Vector2(Random.Range(-5f, 5f), 0), ForceMode2D.Impulse);
        mRb.angularVelocity = Random.Range(-500f, 500f);

        yield return new WaitForSeconds(timeBetweenMeteors + Random.Range(-.25f, .25f));

        StartCoroutine(RainMeteor());
    }

    void DoStates()
    {
        switch (currentState)
        {
            case State.earthquake: Earthquake(); break;
            case State.lightningStorm: StartCoroutine(Lightning()); break;
            case State.rainingMeteors: StartCoroutine(RainMeteor()); break;
            case State.windy: AudioHandler.I.PlayWeatherSfx("Wind"); StartCoroutine(Wind()); while (windDir == 0) { windDir = Random.Range(-1, 2); } break;
        }
    }

    public void ResetStates(bool startAgain)
    {
        StopAllCoroutines();

        ground.GetComponent<Animator>().enabled = false;
        //mainCamAni.Play("Idle");
        windDir = 0;

        if (rain.isPlaying)
        {
            rain.Stop();
        }

        //audioHandler.EarthQuakeSoundAudioSource().Stop();
        //audioHandler.RainSoundAudioSource().Stop();
        //audioHandler.WindSoundAudioSource().Stop();

        if (startAgain)
        {
            StartCoroutine(ChooseNewState());
        }
    }

    public IEnumerator ChooseNewState()
    {
        while (currentState == prevState)
        {
            var rand = Random.Range(1, 5);

            switch (rand)
            {
                case 1: currentState = State.earthquake; break;
                case 2: currentState = State.lightningStorm; break;
                case 3: currentState = State.rainingMeteors; break;
                case 4: currentState = State.windy; break;
            }
        }

        DoStates();

        yield return new WaitForSeconds(disasterDuration);

        prevState = currentState;

        ResetStates(false);
    }

    int windDir;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (currentState != State.windy) { return; }

        var rb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (rb)
            rb.velocity += new Vector2(Time.deltaTime * windIntensity * windDir, 0);
    }
}
