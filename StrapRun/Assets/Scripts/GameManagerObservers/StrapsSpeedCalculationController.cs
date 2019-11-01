using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StrapsSpeedCalculationController : MonoBehaviour, IGameManagerObserver {

	const int MAX_EASY_SPEED_LEVELS_INDEX = 2;
	const int MAX_NORMAL_SPEED_LEVELS_INDEX = 3;
	const int MAX_HARD_SPEED_LEVELS_INDEX = 4;

	GAME_DIFFICULTY difiiculty;
	bool isGamePaused;
	bool isPlayerDead;
	float currentTime;
	float changedSpeedTime = 5f;
	float currentStrapSpeed = 5f;
	float targetStrapSpeed;
	float[] speedLevels = {5f, 10f, 14f, 18f, 22f};
	int currentSpeedFlag = 0;
	List<ISpeedObserver> strapsList = new List<ISpeedObserver>(); 

	// Use this for initialization
	void Start () {
		GameManager.Instanse.AddObserver (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateFields(float time, GAME_DIFFICULTY difficulty, bool isGamePaused, bool isPlayerDead, MOVING_DIRECTION movingDiraction)
	{
		this.currentTime = time;
		this.isGamePaused = isGamePaused;
		this.isPlayerDead = isPlayerDead;
		this.difiiculty = difficulty;
		if (isPlayerDead)
			GameManager.Instanse.RemoveObserver (this);
		ChangeSpeedByDifficulty ();
	}

	void ChangeSpeedByDifficulty()
	{
		switch (difiiculty) {
		case GAME_DIFFICULTY.EASY:
			ChangeSpeed (MAX_EASY_SPEED_LEVELS_INDEX);
			break;
		case GAME_DIFFICULTY.NORMAL:
			ChangeSpeed (MAX_NORMAL_SPEED_LEVELS_INDEX);
			break;
		case GAME_DIFFICULTY.HARD:
			ChangeSpeed (MAX_HARD_SPEED_LEVELS_INDEX);
			break;
		}
	}

	public void AddObserver(ISpeedObserver observer)
	{
		strapsList.Add (observer);
		observer.UpdateFields (currentStrapSpeed, currentTime, isGamePaused);
	}

	public void RemoveObserver(ISpeedObserver observer)
	{
		strapsList.Remove (observer);
	}

	public void NotifyObservers()
	{
		var tempStrapsList = new List<ISpeedObserver> (strapsList);
		foreach (var strap in tempStrapsList) {
			strap.UpdateFields(currentStrapSpeed, currentTime ,isGamePaused);
		}
	}

	void ChangeSpeed (int speedLevelsIndex)
	{
		if (currentTime >= changedSpeedTime) {
			changedSpeedTime += 20f;
			if (currentSpeedFlag < speedLevelsIndex) {
				currentSpeedFlag++;
				//Debug.Log("Установили флаг скорости на " + currentSpeedFlag);
				targetStrapSpeed = speedLevels [currentSpeedFlag];
			}
		}
		SmoothSpeedIncreasing (targetStrapSpeed);

	}

	void SmoothSpeedIncreasing(float speedLevel)
	{
		if (currentStrapSpeed < speedLevel) {
			currentStrapSpeed += 0.25f;
			//Debug.Log(currentStrapSpeed);
		}
		NotifyObservers();
	}
}
