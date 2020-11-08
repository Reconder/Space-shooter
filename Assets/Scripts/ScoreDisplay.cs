using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        FindObjectOfType<GameSession>().OnScoreChange += UpdateText;
    }

    private void UpdateText(ScoreChangedEventArgs args) => scoreText.text = args.Totalscore.ToString();

    // Update is called once per frame

}
