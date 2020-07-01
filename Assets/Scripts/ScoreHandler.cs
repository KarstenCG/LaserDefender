using TMPro;
using UnityEngine;


public class ScoreHandler : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreText;
    GameStatus gameStatus;

    //private void Awake()
    //{
    //    SetUpSingeleton();
    //}
    //private void SetUpSingeleton()
    //{
    //    if (FindObjectsOfType(GetType()).Length > 1)
    //    {
    //        gameObject.SetActive(false);
    //        Destroy(gameObject);
            
    //    }
    //    else
    //    {
    //        DontDestroyOnLoad(gameObject);
    //    }
    //}
    
    // Start is called before the first frame update
    void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        scoreText.text = "Score: " + gameStatus.GetScore().ToString();

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + gameStatus.GetScore().ToString();
    }

    public void SetScoreToZero()
    {
        Destroy(gameObject);
    }

}
