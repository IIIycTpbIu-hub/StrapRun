using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour, IGameManagerObserver {

	float gameTime;
	float second;
	GameManager gameManager;
	bool isGamePaused;
	bool firstUpdate = false;
	bool isPlayerDead;
	// Use this for initialization
	void Start () {
		gameTime = 0;
		second = gameTime;
		gameManager = GameManager.Instanse;
		isGamePaused = false;
		GameManager.Instanse.AddObserver (this);
	}
	
	// Update is called once per frame
	void Update () {
		if (!isGamePaused && !isPlayerDead) {
			gameTime += Time.deltaTime;
			if (gameTime - second > 1) 
			{
				//Debug.Log(gameTime);
				second = gameTime;
				gameManager.SetGameTime(second);
			}
		}
	}

	public void UpdateFields(float time, GAME_DIFFICULTY difficulty, bool isGamePaused, bool isPlayerDead, MOVING_DIRECTION movingDiraction)
	{
		this.isGamePaused = isGamePaused;
		this.isPlayerDead = isPlayerDead;
		firstUpdate = true;;
		if (isPlayerDead && !firstUpdate) {
			gameTime = 0;
			gameManager.RemoveObserver(this);
		}
	}
}
