using UnityEngine;

public class GameStatus : MonoBehaviour
{

    //Configuration
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int startHealth = 2000;
    //[SerializeField] TextMeshProUGUI healthText;
    int currentScore = 0;

    // State
    Player player;
    SceneLoader sceneLoader;
    int health;

    private void Awake()
    {
        SetUpSingeleton();
    }
    private void SetUpSingeleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        player = FindObjectOfType<Player>();
        health = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }


    public void SetScoreToZero()
    {
        Destroy(gameObject);
    }
    
    
    public int GetScore()
    {
        return currentScore;
    }

    public int GetHealth()
    {
        return health;
    }

    public void AddToScore(int pointValue)
    {
        currentScore += pointValue;
    }

    public void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        health = Mathf.Clamp(health, 0, startHealth);
        damageDealer.Hit();
        player.PlayerHitSfx();
        player.HitVfx();
        if (health <= 0f)
        {
            player.PlayerExplosionSFX();
            player.PlayerExplosionVFX();
            sceneLoader.LoadGameOverScene();
        }
    }
}
