using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Configuration Parameters
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.5f;
    [SerializeField] float yTopPadding = 5f;
    //[SerializeField] float gameOverDelay = 5f;

    [Header("Projectile")]
    [SerializeField] GameObject playerLaserPrefab;
    [SerializeField] float playerLaserSpeed = 10f;
    [SerializeField] float laserRecycleTime = 0.1f;
    [SerializeField] AudioClip playerLaserSound;
    [SerializeField] float playerLaserVol = 0.5f;

    [Header("Damage Effects")]
    [SerializeField] AudioClip playerHitSound;
    [SerializeField] float playerHitVol = 1f;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] float explosionVol;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] float explosionTime = 1f;
    [SerializeField] GameObject hitPrefab;
    [SerializeField] float hitTime = 1f;


    Coroutine firingCoroutine;
    GameStatus gameStatus;

    float xMin;
    float xMax;
    float yMin;
    float yMax;
    Vector3 leftGun;
    Vector3 rightGun;
    int health;


    // Start is called before the first frame update
    void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        MoveBounds();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        Quit();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinously()
    {
        while (true)
        {
            leftGun = new Vector3(transform.position.x - 0.19f, transform.position.y + 1f, transform.position.z);
            rightGun = new Vector3(transform.position.x + 0.19f, transform.position.y + 1f, transform.position.z);

            GameObject blueLaserLeft = Instantiate(playerLaserPrefab, leftGun, Quaternion.identity) as GameObject;
            blueLaserLeft.GetComponent<Rigidbody2D>().velocity = new Vector2(0, playerLaserSpeed);

            GameObject blueLaserRight = Instantiate(playerLaserPrefab, rightGun, Quaternion.identity) as GameObject;
            blueLaserRight.GetComponent<Rigidbody2D>().velocity = new Vector2(0, playerLaserSpeed);

            AudioSource.PlayClipAtPoint(playerLaserSound, Camera.main.transform.position, playerLaserVol);
            yield return new WaitForSeconds(laserRecycleTime);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed; //Time.DeltaTime makes Game FPS independant.
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newPos = new Vector2(Mathf.Clamp(transform.position.x + deltaX, xMin, xMax), Mathf.Clamp(transform.position.y + deltaY, yMin, yMax));
        transform.position = newPos;
    }

    private void Quit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

    }

    private void MoveBounds()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yTopPadding;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            return;
        }

        gameStatus.ProcessHit(damageDealer);
        //Destroy(other.gameObject);
    }


    public void PlayerHitSfx()
    {
        AudioSource.PlayClipAtPoint(playerHitSound, Camera.main.transform.position, playerHitVol);
    }

    public void HitVfx()
    {
        GameObject hit = Instantiate(hitPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(hit, hitTime);
    }

    public void PlayerExplosionSFX()
    {
        AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, explosionVol);
    }

    public void PlayerExplosionVFX()
    {
        Destroy(gameObject); 
        GameObject playerExplosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(playerExplosion, explosionTime);
    }

}
