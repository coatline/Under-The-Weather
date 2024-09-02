using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorShower : DisasterBehavior
{
    [SerializeField] float timeBetweenMeteors;
    [SerializeField] Rigidbody2D meteorPrefab;

    public override void Begin()
    {
        StartCoroutine(RainMeteors());
    }

    public override void Stop()
    {
        StopAllCoroutines();
    }

    IEnumerator RainMeteors()
    {
        while (true)
        {
            Vector2 meteorPos = new Vector3(Random.Range(-8f, 8f), 8f);
            Rigidbody2D meteor = Instantiate(meteorPrefab, meteorPos, Quaternion.Euler(0, 0, Random.Range(0, 361)), transform);
            meteor.AddForce(new Vector2(Random.Range(-5f, 5f), 0), ForceMode2D.Impulse);
            meteor.angularVelocity = Random.Range(-500f, 500f);

            SoundManager.I.PlaySound("Meteor Pop", meteorPos);

            yield return new WaitForSeconds(timeBetweenMeteors + Random.Range(-.25f, .25f));
        }
    }
}
