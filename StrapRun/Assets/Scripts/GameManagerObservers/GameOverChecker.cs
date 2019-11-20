using UnityEngine;
using System.Collections;

public class GameOverChecker : MonoBehaviour, IGameManagerObserver {

	GameObject _player;

	void Start () {
		_player = GameObject.FindGameObjectWithTag ("Player");
		GameManager.Instanse.AddObserver (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.name == "Player") {
			GameManager.Instanse.SetGameOver(true);
		}
		
	}

	public void UpdateFields(float time, GAME_DIFFICULTY difficulty, bool isGamePaused, bool isPlayerDead, MOVING_DIRECTION movingDiraction)
	{
		if (isPlayerDead) {
			GameManager.Instanse.RemoveObserver(this);
			Application.LoadLevel (2);
		}
	}
}
