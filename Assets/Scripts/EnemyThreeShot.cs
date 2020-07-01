using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThreeShot : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] float health = 100f;
    [SerializeField] int pointValue = 20;

    [Header("Projectile")]
    [SerializeField] float leftShotCounter;
    [SerializeField] float centerShotCounter;
    [SerializeField] float rightShotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float enemyLaserSpeed = 8f;
    [SerializeField] GameObject enemyLaserPrefab;
    [SerializeField] AudioClip enemyLaserSound;
    [SerializeField] float enemyLaserVol = 0.5f;

    [Header("Damage Effects")]
    [SerializeField] GameObject enemyExplosionPrefab;
    [SerializeField] float explosionTime = 1f;
    [SerializeField] GameObject enemyHitPrefab;
    [SerializeField] float hitTime = 1f;
    [SerializeField] AudioClip enemyExplosionSound;
    [SerializeField] float enemyExplosionVol = 1f;

    GameStatus gameStatus;

    // Start is called before the first frame update
    void Start()
    {
        leftShotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        centerShotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        rightShotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);

        gameStatus = FindObjectOfType<GameStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShootLeft();
        CountDownAndShootCenter();
        CountDownAndShootRight();
    }

    public void CountDownAndShootLeft()
    {
        leftShotCounter -= Time.deltaTime;
        if (leftShotCounter <= 0f)
        {
            FireLeft();
            leftShotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    public void CountDownAndShootCenter()
    {
        centerShotCounter -= Time.deltaTime;
        if (centerShotCounter <= 0f)
        {
            FireCenter();
            centerShotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }
    public void CountDownAndShootRight()
    {
        rightShotCounter -= Time.deltaTime;
        if (rightShotCounter <= 0f)
        {
            FireRight();
            rightShotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }


    private void FireLeft()
    {
        var leftGun = new Vector3(transform.position.x - 1.2f, transform.position.y - 2f, transform.position.z);
        GameObject leftShot = Instantiate(enemyLaserPrefab, leftGun, Quaternion.identity) as GameObject;
        leftShot.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyLaserSpeed);
        AudioSource.PlayClipAtPoint(enemyLaserSound, Camera.main.transform.position, enemyLaserVol);
    }

    private void FireCenter()
    {
        var centerGun = new Vector3(transform.position.x, transform.position.y - 1.9f, transform.position.z);
        GameObject centerShot = Instantiate(enemyLaserPrefab, centerGun, Quaternion.identity) as GameObject;
        centerShot.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyLaserSpeed);
        AudioSource.PlayClipAtPoint(enemyLaserSound, Camera.main.transform.position, enemyLaserVol);
    }

    private void FireRight()
    {
        var rightGun = new Vector3(transform.position.x + 1.2f, transform.position.y - 2f, transform.position.z);
        GameObject rightShot = Instantiate(enemyLaserPrefab, rightGun, Quaternion.identity) as GameObject;
        rightShot.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyLaserSpeed);
        AudioSource.PlayClipAtPoint(enemyLaserSound, Camera.main.transform.position, enemyLaserVol);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            return;
        }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        EnemyHit(damageDealer);
        damageDealer.Hit();
        if (health <= 0f)
        {
            EnemyDeath();
        }
    }

    private void EnemyDeath()
    {
        Destroy(gameObject);
        EnemyExplosionVFX();
        EnemyExplosionSFX();
        gameStatus.AddToScore(pointValue);
    }

    private void EnemyHit(DamageDealer damageDealer)
    {
        GameObject enemyHit = Instantiate(enemyHitPrefab, damageDealer.transform.position, Quaternion.identity) as GameObject;
        Destroy(enemyHit, hitTime);
    }
    private void EnemyExplosionSFX()
    {
        AudioSource.PlayClipAtPoint(enemyExplosionSound, Camera.main.transform.position, enemyExplosionVol);
    }

    private void EnemyExplosionVFX()
    {
        GameObject enemyExplosion = Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(enemyExplosion, explosionTime);
    }
}
