using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] float health = 100f;
    [SerializeField] int pointValue  = 20;

    [Header("Projectile")]
    [SerializeField] float shotCounter;
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
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        gameStatus = FindObjectOfType<GameStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    public void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        var gun = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
        GameObject orangeLaser = Instantiate(enemyLaserPrefab, gun, Quaternion.identity) as GameObject;
        orangeLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyLaserSpeed);
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
