using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] float gameOverDelay;
    ScoreHandler scoreHandler;
    GameStatus gameStatus;

   public void  LoadGameOverScene()
    {
        StartCoroutine(WaitForGameOverScene(gameOverDelay));
    }

    IEnumerator WaitForGameOverScene(float gameOverDelay)
    {
        yield return new WaitForSeconds(gameOverDelay);
        SceneManager.LoadScene(2);
    }

    public void Play()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        gameStatus.SetScoreToZero();
        //scoreHandler = FindObjectOfType<ScoreHandler>();
        //scoreHandler.SetScoreToZero();
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
