using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreCalculationController : MonoBehaviour, IGameManagerObserver {
	
	float _gameTime;
	float _score;
	bool _isGamePaused;
	bool _isPlayerDead;
	Text _scoreDisplay;
	Text _scorePauseDisplay;

	// Use this for initialization
	void Start () {
		GameManager.Instanse.AddObserver (this);
		_scoreDisplay = GameObject.Find ("ScoreDisplay").GetComponentInChildren<Text>();
		_scorePauseDisplay = GameObject.Find ("PausePanel").GetComponentInChildren<Text> ();
		_score = 0;
		DisplayScore ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateFields(float time, GAME_DIFFICULTY difficulty, bool isGamePaused, bool isPlayerDead, MOVING_DIRECTION movingDiraction)
	{
		_gameTime = time;
		_isGamePaused = isGamePaused;
		_isPlayerDead = isPlayerDead;
		if (!_isGamePaused)
			CalculateScore (time);
		if (_isPlayerDead) {
			GameManager.Instanse.RemoveObserver(this);
			GameManager.Instanse.Score = (int)_score;
		}
		DisplayScore ();

	}

	public void CalculateScore(float gameTime)
	{
		_score += gameTime * 1.1f; 
	}

	public void DisplayScore()
	{
		_scoreDisplay.text = Convert.ToInt64(_score).ToString();
		_scorePauseDisplay.text = "SCORE: " + Convert.ToInt64(_score	).ToString();
	}	
}
