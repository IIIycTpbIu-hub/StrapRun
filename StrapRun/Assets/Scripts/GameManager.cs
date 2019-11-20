using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GAME_DIFFICULTY {EASY, NORMAL, HARD}
public class GameManager : MonoBehaviour {

	enum G_MANAGER_STATE {START_MENU, PLAY}
	G_MANAGER_STATE _gameState;
	GAME_DIFFICULTY _difficulty;
	MOVING_DIRECTION _movingDiraction = MOVING_DIRECTION.LEFT;
	bool _isPlayerDead;
	bool _gamePause;
	float _gameTime;
	int _score;
	List<IGameManagerObserver> _observersList = new List<IGameManagerObserver> ();
	private static GameManager _instanse;

	public static GameManager Instanse { get {return _instanse;}}
	public int Score {get {return _score;} set {_score = value;}}
	public bool GamePause {get {return _gamePause;}}

	// Use this for initialization
	void Start () {

	}

	void Awake()
	{

		if (_instanse) {
			DestroyImmediate(gameObject);
			return;
		}
		_instanse = this;
		DontDestroyOnLoad (gameObject);
		if (Application.loadedLevel == 0)
			_gameState = G_MANAGER_STATE.START_MENU;
		else if (Application.loadedLevel == 1){
			_gameState = G_MANAGER_STATE.PLAY;
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SetDifficulty(GAME_DIFFICULTY difficulty)
	{
		this._difficulty = difficulty;
		_gameState = G_MANAGER_STATE.PLAY;
	}

	public GAME_DIFFICULTY GetDifficulty()
	{
		return _difficulty;
	}

	public void SetGameTime(float time)
	{
		_gameTime = time;
		NotifyObservers ();
	}

	public void SetPause(bool pause)
	{
		this._gamePause = pause;
		NotifyObservers ();
	}

	public void AddObserver(IGameManagerObserver observer)
	{
		_observersList.Add (observer);
		//Debug.Log (observer);
	}

	public void RemoveObserver(IGameManagerObserver observer)
	{
		_observersList.Remove (observer);
	}

	public void NotifyObservers()
	{
		var tempObserverssList = new List<IGameManagerObserver> (_observersList);
		foreach (var observer in tempObserverssList) {
			observer.UpdateFields(_gameTime, _difficulty, _gamePause, _isPlayerDead, _movingDiraction);
		}
	}

	public void SetGameOver(bool gameOver)
	{
		_isPlayerDead = gameOver;
	}
}
