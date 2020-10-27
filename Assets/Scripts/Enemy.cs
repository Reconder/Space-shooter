
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 1f;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject explosion;
    [SerializeField] AudioClip fireSFX;
    [SerializeField] [Range(0, 1)] float volumeFireSFX = 0.25f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0,1)] float volumeDeathSFX = 0.7f;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] int score = 150;


    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0) { Shoot(); shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots); }
    }

    private void Shoot()
    {
        GameObject laser = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(fireSFX, Camera.main.transform.position, volumeFireSFX);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
            
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        FindObjectOfType<GameSession>().AddScore(score);
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSFX, transform.position, volumeDeathSFX);
        GameObject deathVFX = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(deathVFX, 1f);
    }
}
