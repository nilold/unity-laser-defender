using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    Text scoreText;
    public int score = 0;

	// Use this for initialization
	void Start () {
        scoreText = GetComponent<Text>();
        UpdateScoreText();
    }

    public int AddScore(int score)
    {
        this.score += score;
        UpdateScoreText();
        return this.score;
    }

    private void Reset()
    {
        score = 0;
        UpdateScoreText();
    }

    private void UpdateScoreText(){
        scoreText.text = score.ToString();
    }
}
