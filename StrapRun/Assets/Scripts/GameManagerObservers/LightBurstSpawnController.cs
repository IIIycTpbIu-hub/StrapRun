using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightBurstSpawnController : MonoBehaviour, IGameManagerObserver {

	
	List<Spawner> _lightBurstSpawnersList = new List<Spawner>();
	float _lightBurstSpawnTime;
	bool _spawnTimeInitialized = false;
	MOVING_DIRECTION _movingDiraction;
	GAME_DIFFICULTY _difficulty;
	// Use this for initialization
	void Start () {
		GameManager.Instanse.AddObserver (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateFields(float time, GAME_DIFFICULTY difficulty, bool isGamePaused, bool isPlayerDead, MOVING_DIRECTION movingDiraction)
	{
		_movingDiraction = movingDiraction;
		_difficulty = difficulty;
		if (!_spawnTimeInitialized) {
			switch (difficulty) {
			case GAME_DIFFICULTY.NORMAL:
			{
				_lightBurstSpawnTime = time + 15f;
			}
				break;
			case GAME_DIFFICULTY.HARD:
			{
				_lightBurstSpawnTime = time + 8f;
			}
				break;
			}
			_spawnTimeInitialized = true;
		}
		if (isPlayerDead) {
			GameManager.Instanse.RemoveObserver (this);
		}
		if (time > _lightBurstSpawnTime) {
			NotifyObservers();
			_spawnTimeInitialized = false;
		}
		

	}

	public void AddObserver(Spawner lightBurstSpawner)
	{
		_lightBurstSpawnersList.Add (lightBurstSpawner);
	}

	public void RemoveObserver(Spawner lightBurstSpawner)
	{
		_lightBurstSpawnersList.Remove (lightBurstSpawner);
	}

	public void NotifyObservers()
	{

		foreach (var lightBurstSpawner in _lightBurstSpawnersList) {
			if(this._movingDiraction == lightBurstSpawner.MovingDiraction && _difficulty != GAME_DIFFICULTY.EASY)
				lightBurstSpawner.SpawnObject(PRODUCT_TYPE.LIGHT_BUSRST);
		}
	}
}
