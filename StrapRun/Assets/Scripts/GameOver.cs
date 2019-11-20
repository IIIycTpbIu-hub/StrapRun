using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
	GameObject _gameOverMessage;
	GameManager _gameManager;
	// Use this for initialization
	void Start () {
		_gameOverMessage = GameObject.Find ("UI");
		_gameManager = GameManager.Instanse;
		Text text = _gameOverMessage.GetComponentInChildren<Text>();
		text.text = "GAME OVER \n" + "Difficulty: " + 
			_gameManager.GetDifficulty ().ToString () + "\n" + "Score: " + 
				_gameManager.Score.ToString ();
		_gameManager.Score = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadLevel1()
	{
		_gameManager.SetGameOver (false);
		Application.LoadLevel (1);
	}

	public void Quit()
	{
		Application.Quit ();
	}
}
