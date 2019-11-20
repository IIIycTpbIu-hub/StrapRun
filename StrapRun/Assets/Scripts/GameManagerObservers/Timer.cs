using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour, IGameManagerObserver {

	float _gameTime;
	float _second;
	GameManager _gameManager;
	bool _isGamePaused;
	bool _firstUpdate = false;
	bool _isPlayerDead;
	// Use this for initialization
	void Start () {
		_gameTime = 0;
		_second = _gameTime;
		_gameManager = GameManager.Instanse;
		_isGamePaused = false;
		GameManager.Instanse.AddObserver (this);
	}
	
	// Update is called once per frame
	void Update () {
		if (!_isGamePaused && !_isPlayerDead) {
			_gameTime += Time.deltaTime;
			if (_gameTime - _second > 1) 
			{
				//Debug.Log(gameTime);
				_second = _gameTime;
				_gameManager.SetGameTime(_second);
			}
		}
	}

	public void UpdateFields(float time, GAME_DIFFICULTY difficulty, bool isGamePaused, bool isPlayerDead, MOVING_DIRECTION movingDiraction)
	{
		_isGamePaused = isGamePaused;
		_isPlayerDead = isPlayerDead;
		_firstUpdate = true;;
		if (_isPlayerDead && !_firstUpdate) {
			_gameTime = 0;
			_gameManager.RemoveObserver(this);
		}
	}
}
