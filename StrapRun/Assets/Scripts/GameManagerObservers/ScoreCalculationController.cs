using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreCalculationController : MonoBehaviour, IGameManagerObserver {
	
	float gameTime;
	float score;
	bool isGamePaused;
	bool isPlayerDead;
	Text ScoreDisplay;
	Text scorePauseDisplay;

	// Use this for initialization
	void Start () {
		GameManager.Instanse.AddObserver (this);
		ScoreDisplay = GameObject.Find ("ScoreDisplay").GetComponentInChildren<Text>();
		scorePauseDisplay = GameObject.Find ("PausePanel").GetComponentInChildren<Text> ();
		score = 0;
		DisplayScore ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateFields(float time, GAME_DIFFICULTY difficulty, bool isGamePaused, bool isPlayerDead, MOVING_DIRECTION movingDiraction)
	{
		gameTime = time;
		this.isGamePaused = isGamePaused;
		this.isPlayerDead = isPlayerDead;
		if (!isGamePaused)
			CalculateScore (time);
		if (isPlayerDead) {
			GameManager.Instanse.RemoveObserver(this);
			GameManager.Instanse.Score = (int)score;
		}
		DisplayScore ();

	}

	public void CalculateScore(float gameTime)
	{
		score += gameTime * 1.1f; 
	}

	public void DisplayScore()
	{
		ScoreDisplay.text = Convert.ToInt64(score).ToString();
		scorePauseDisplay.text = "SCORE: " + Convert.ToInt64(score	).ToString();
	}	
}
