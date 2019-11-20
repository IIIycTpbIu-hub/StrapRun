using UnityEngine;
using System.Collections;

public class TestButton : MonoBehaviour {

	GameObject[] _togles;
	GameManager _gameManager;
	GAME_DIFFICULTY _difficulty;

	public void SetDifficlultyToEasy()
	{	
		_difficulty = GAME_DIFFICULTY.EASY;
	}
	public void SetDifficlultyToNormal()
	{
		_difficulty = GAME_DIFFICULTY.NORMAL;
	}
	public void SetDifficlultyToHard()
	{
		_difficulty = GAME_DIFFICULTY.HARD;
	}

	public void OnStartPressed()
	{
		_gameManager  = GameManager.Instanse;
		_gameManager.SetDifficulty (_difficulty);
		Application.LoadLevel (1);
	}
}
