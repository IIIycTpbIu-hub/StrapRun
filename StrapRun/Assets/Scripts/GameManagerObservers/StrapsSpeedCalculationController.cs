using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StrapsSpeedCalculationController : MonoBehaviour, IGameManagerObserver {

	const int MAX_EASY_SPEED_LEVELS_INDEX = 2;
	const int MAX_NORMAL_SPEED_LEVELS_INDEX = 3;
	const int MAX_HARD_SPEED_LEVELS_INDEX = 4;

	GAME_DIFFICULTY _difiiculty;
	bool _isGamePaused;
	bool _isPlayerDead;
	float _currentTime;
	float _changedSpeedTime = 5f;
	float _currentStrapSpeed = 5f;
	float _targetStrapSpeed;
	float[] _speedLevels = {5f, 10f, 14f, 18f, 22f};
	int _currentSpeedFlag = 0;
	List<ISpeedObserver> _strapsList = new List<ISpeedObserver>(); 

	// Use this for initialization
	void Start () {
		GameManager.Instanse.AddObserver (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateFields(float time, GAME_DIFFICULTY difficulty, bool isGamePaused, bool isPlayerDead, MOVING_DIRECTION movingDiraction)
	{
		_currentTime = time;
		_isGamePaused = isGamePaused;
		_isPlayerDead = isPlayerDead;
		_difiiculty = difficulty;
		if (_isPlayerDead)
			GameManager.Instanse.RemoveObserver (this);
		ChangeSpeedByDifficulty ();
	}

	void ChangeSpeedByDifficulty()
	{
		switch (_difiiculty) {
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
		_strapsList.Add (observer);
		observer.UpdateFields (_currentStrapSpeed, _currentTime, _isGamePaused);
	}

	public void RemoveObserver(ISpeedObserver observer)
	{
		_strapsList.Remove (observer);
	}

	public void NotifyObservers()
	{
		var tempStrapsList = new List<ISpeedObserver> (_strapsList);
		foreach (var strap in tempStrapsList) {
			strap.UpdateFields(_currentStrapSpeed, _currentTime ,_isGamePaused);
		}
	}

	void ChangeSpeed (int speedLevelsIndex)
	{
		if (_currentTime >= _changedSpeedTime) {
			_changedSpeedTime += 20f;
			if (_currentSpeedFlag < speedLevelsIndex) {
				_currentSpeedFlag++;
				//Debug.Log("Установили флаг скорости на " + currentSpeedFlag);
				_targetStrapSpeed = _speedLevels [_currentSpeedFlag];
			}
		}
		SmoothSpeedIncreasing (_targetStrapSpeed);

	}

	void SmoothSpeedIncreasing(float speedLevel)
	{
		if (_currentStrapSpeed < speedLevel) {
			_currentStrapSpeed += 0.25f;
			//Debug.Log(currentStrapSpeed);
		}
		NotifyObservers();
	}
}
