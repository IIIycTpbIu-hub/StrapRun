using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
	GameObject gameOverMessage;
	GameManager gameManager;
	// Use this for initialization
	void Start () {
		gameOverMessage = GameObject.Find ("UI");
		gameManager = GameManager.Instanse;
		Text text = gameOverMessage.GetComponentInChildren<Text>();
		text.text = "GAME OVER \n" + "Difficulty: " + 
			gameManager.GetDifficulty ().ToString () + "\n" + "Score: " + 
				gameManager.Score.ToString ();
		gameManager.Score = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadLevel1()
	{
		gameManager.SetGameOver (false);
		Application.LoadLevel (1);
	}

	public void Quit()
	{
		Application.Quit ();
	}
}
