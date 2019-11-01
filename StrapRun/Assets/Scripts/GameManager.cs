using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GAME_DIFFICULTY {EASY, NORMAL, HARD}
public class GameManager : MonoBehaviour {
	enum G_MANAGER_STATE {START_MENU, PLAY}
	G_MANAGER_STATE gameState;
	GAME_DIFFICULTY difficulty;
	bool isPlayerDead;
	bool gamePause;
	float gameTime;
	int score;
	List<IGameManagerObserver> observersList = new List<IGameManagerObserver> ();
	private static GameManager instanse;
	public static GameManager Instanse { get {return instanse;}}
	public int Score {get {return score;} set {score = value;}}
	public bool GamePause {get {return gamePause;}}
	MOVING_DIRECTION movingDiraction = MOVING_DIRECTION.LEFT;
	// Use this for initialization
	void Start () {

	}

	void Awake()
	{

		if (instanse) {
			DestroyImmediate(gameObject);
			return;
		}
		instanse = this;
		DontDestroyOnLoad (gameObject);
		if (Application.loadedLevel == 0)
			gameState = G_MANAGER_STATE.START_MENU;
		else if (Application.loadedLevel == 1){
			gameState = G_MANAGER_STATE.PLAY;
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SetDifficulty(GAME_DIFFICULTY difficulty)
	{
		this.difficulty = difficulty;
		gameState = G_MANAGER_STATE.PLAY;
	}

	public GAME_DIFFICULTY GetDifficulty()
	{
		return difficulty;
	}

	public void SetGameTime(float time)
	{
		gameTime = time;
		NotifyObservers ();
	}

	public void SetPause(bool pause)
	{
		this.gamePause = pause;
		NotifyObservers ();
	}

	public void AddObserver(IGameManagerObserver observer)
	{
		observersList.Add (observer);
		//Debug.Log (observer);
	}

	public void RemoveObserver(IGameManagerObserver observer)
	{
		observersList.Remove (observer);
	}

	public void NotifyObservers()
	{
		var tempObserverssList = new List<IGameManagerObserver> (observersList);
		foreach (var observer in tempObserverssList) {
			observer.UpdateFields(gameTime, difficulty, gamePause, isPlayerDead, movingDiraction);
		}
	}

	public void SetGameOver(bool gameOver)
	{
		isPlayerDead = gameOver;
	}
}
