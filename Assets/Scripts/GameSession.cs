using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreChangedEventArgs : EventArgs
{
    public int Totalscore { get; set; }
}

public class GameSession : MonoBehaviour
{
    int score = 0;

    public delegate void ScoreChangedEventHandler(ScoreChangedEventArgs args);
    public ScoreChangedEventHandler OnScoreChange;
    private void Awake() => SetUpSingleton();

    private void SetUpSingleton()
    {
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberGameSessions>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int Score => score;

    public void AddScore(EnemyDeathEventArgs args)
    {
        score += args.Points;
        OnScoreChange?.Invoke(new ScoreChangedEventArgs() { Totalscore = score });
    }

    public void ResetGame() => Destroy(gameObject);
    // Update is called once per frame

}
