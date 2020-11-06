using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [Header("Player")]
    [SerializeField] float Speed = 15f;
    [SerializeField] float Xpadding = 1f;
    [SerializeField] float Ypadding = 1f;
    [SerializeField] int health = 100;
    [SerializeField] GameObject explosion;
    [SerializeField] AudioClip fireSFX;
    [SerializeField] [Range(0, 1)] float volumeFireSFX = 0.25f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float volumeDeathSFX = 0.7f;

    [Header("Projectile")]
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float projectilePeriod = 0.1f;

    Coroutine firingCoroutine;

    float Xmin, Xmax, Ymin, Ymax;
    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }
    void Update()
    {
        Movement();
        Fire();
        
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(fireSFX, Camera.main.transform.position, volumeFireSFX);
            yield return new WaitForSeconds(projectilePeriod);
        }
    }


    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        Xmin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        Xmax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        Ymin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        Ymax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

    // Update is called once per frame

    private void Movement()
    {
        var deltaX = Speed * Input.GetAxis("Horizontal") * Time.deltaTime;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, Xmin + Xpadding, Xmax - Xpadding);

        var deltaY = Speed * Input.GetAxis("Vertical") * Time.deltaTime;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, Ymin + Ypadding, Ymax - Ypadding);
        transform.position = new Vector2(newXPos,newYPos);
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
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSFX, transform.position, volumeDeathSFX);
        GameObject deathVFX = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(deathVFX, 1f);
    }

    public int GetHealth()
    {
        return health > 0 ? health : 0;
    }
}
