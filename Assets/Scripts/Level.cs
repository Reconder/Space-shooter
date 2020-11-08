using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float delayGameOver = 1f;
    GameSession gameSession;

    private void Start() => gameSession = FindObjectOfType<GameSession>();


    public void LoadGame()
    {
        SceneManager.LoadScene("Level1");
        gameSession.ResetGame();
    }
    public void LoadGameOver() => StartCoroutine(GameOver());

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(delayGameOver);
        SceneManager.LoadScene("Game Over");
    }

    public void LoadGameScene(int buildIndex) => SceneManager.LoadScene(buildIndex + 1);
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    public void LoadStartMenu() => SceneManager.LoadScene(0);
    public void QuitGame() => Application.Quit();
}
