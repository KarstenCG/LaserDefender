using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;

    GameStatus gameStatus;
    int health;

    // Start is called before the first frame update
    void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        health = gameStatus.GetHealth();
        healthText.text = "Health: " + health;
    }

    // Update is called once per frame
    void Update()
    {
        health = gameStatus.GetHealth();
        healthText.text = "Health: " + health.ToString();
    }
}
