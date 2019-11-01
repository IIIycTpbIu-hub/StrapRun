using UnityEngine;
using System.Collections;

public class TestButton : MonoBehaviour {

	GameObject[] togles;
	GameManager gameManager;
	GAME_DIFFICULTY difficulty;

	public void SetDifficlultyToEasy()
	{	
		difficulty = GAME_DIFFICULTY.EASY;
	}
	public void SetDifficlultyToNormal()
	{
		difficulty = GAME_DIFFICULTY.NORMAL;
	}
	public void SetDifficlultyToHard()
	{
		difficulty = GAME_DIFFICULTY.HARD;
	}

	public void OnStartPressed()
	{
		gameManager  = GameManager.Instanse;
		gameManager.SetDifficulty (difficulty);
		Application.LoadLevel (1);
	}
}
