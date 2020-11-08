
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class EnemyDeathEventArgs : EventArgs
{
    public int Points { get; set; }
}
public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] int health = 100;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 1f;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] int score = 150;

    [Header("Effects")]
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject explosion;
    [SerializeField] AudioClip fireSFX;
    [SerializeField] [Range(0, 1)] float volumeFireSFX = 0.25f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0,1)] float volumeDeathSFX = 0.7f;



    public delegate void EnemyDeathEventHandler(EnemyDeathEventArgs args);
    public EnemyDeathEventHandler OnEnemyDeath;
    void Start() => shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);

    void Update() => CountDownAndShoot();

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0) { Shoot(); shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots); }
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
        health -= damageDealer.Damage;
        damageDealer.Hit();
        if (health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        OnEnemyDeath?.Invoke(new EnemyDeathEventArgs(){ Points = score });
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSFX, transform.position, volumeDeathSFX);
        GameObject deathVFX = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(deathVFX, 1f);
    }
}
