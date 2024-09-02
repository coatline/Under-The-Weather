using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class Person : MonoBehaviour
{
    [SerializeField] GameObject healthUi;
    [SerializeField] Image healthBar;
    [SerializeField] UnityEvent Died;
    [SerializeField] Transform healthBarPosition;
    [SerializeField] ParticleSystem dieParticles;
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float maxHealth = 100;

    float health;
    bool dead;

    void Start()
    {
        health = maxHealth;

        StartCoroutine(Move());
    }

    void Update()
    {
        if (dead) { return; }

        healthUi.transform.position = healthBarPosition.position;
        healthUi.transform.rotation = healthBarPosition.rotation;

        if (rb.velocity.magnitude > (.2f))
        {
            health -= (rb.velocity.magnitude * 3);
            SoundManager.I.PlaySound("Pain", transform.position);
            UpdateHealthBar();

            if (health <= 0)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                GetComponent<Collider2D>().enabled = false;
                sr.enabled = false;
                Destroy(healthUi);
                Invoke("Die", 4f);

                dead = true;
                dieParticles.Emit(100);

                SoundManager.I.PlaySound("Die", transform.position);
            }
        }

        if (!canMove) { return; }

        if (transform.position.x > targetX)
        {
            transform.Translate(new Vector3(-.0005f, 0, 0));
        }
        else if (transform.position.x < targetX)
        {
            transform.Translate(new Vector3(.0005f, 0, 0));
        }
    }

    float targetX;
    bool canMove;

    void ChooseNewTargetPos()
    {
        targetX = Random.Range(-.75f, .75f);
    }

    IEnumerator Move()
    {
        if (Mathf.Abs(transform.position.x - targetX) < .1f)
        {
            canMove = false;
            yield return new WaitForSeconds(Random.Range(0f, 5f));
            ChooseNewTargetPos();
            canMove = true;
        }

        yield return new WaitForSeconds(.1f);

        StartCoroutine(Move());
    }

    void Die()
    {
        //gameOver = true;
        Died?.Invoke();
        Destroy(this.gameObject);
    }

    void UpdateHealthBar()
    {
        float fillmount = (health / maxHealth);

        healthBar.fillAmount = fillmount;
    }
}
