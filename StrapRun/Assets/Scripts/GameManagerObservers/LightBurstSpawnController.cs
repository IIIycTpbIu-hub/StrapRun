using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightBurstSpawnController : MonoBehaviour, IGameManagerObserver {

	
	List<Spawner> lightBurstSpawnersList = new List<Spawner>();
	float lightBurstSpawnTime;
	bool spawnTimeInitialized = false;
	MOVING_DIRECTION movingDiraction;
	GAME_DIFFICULTY difficulty;
	// Use this for initialization
	void Start () {
		GameManager.Instanse.AddObserver (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateFields(float time, GAME_DIFFICULTY difficulty, bool isGamePaused, bool isPlayerDead, MOVING_DIRECTION movingDiraction)
	{
		this.movingDiraction = movingDiraction;
		this.difficulty = difficulty;
		if (!spawnTimeInitialized) {
			switch (difficulty) {
			case GAME_DIFFICULTY.NORMAL:
			{
				lightBurstSpawnTime = time + 15f;
			}
				break;
			case GAME_DIFFICULTY.HARD:
			{
				lightBurstSpawnTime = time + 8f;
			}
				break;
			}
			spawnTimeInitialized = true;
		}
		if (isPlayerDead) {
			GameManager.Instanse.RemoveObserver (this);
		}
		if (time > lightBurstSpawnTime) {
			NotifyObservers();
			spawnTimeInitialized = false;
		}
		

	}

	public void AddObserver(Spawner lightBurstSpawner)
	{
		lightBurstSpawnersList.Add (lightBurstSpawner);
	}

	public void RemoveObserver(Spawner lightBurstSpawner)
	{
		lightBurstSpawnersList.Remove (lightBurstSpawner);
	}

	public void NotifyObservers()
	{

		foreach (var lightBurstSpawner in lightBurstSpawnersList) {
			if(this.movingDiraction == lightBurstSpawner.MovingDiraction && difficulty != GAME_DIFFICULTY.EASY)
				lightBurstSpawner.SpawnObject(PRODUCT_TYPE.LIGHT_BUSRST);
		}
	}
}
