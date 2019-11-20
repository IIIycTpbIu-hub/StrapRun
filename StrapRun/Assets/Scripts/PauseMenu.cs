using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

	GameObject _pauseMenu;
	GameObject _scoreDisplay;
	bool _isActive;
	// Use this for initialization
	void Start () {
		_isActive = false;
		_pauseMenu = GameObject.Find ("PausePanel");
		_scoreDisplay = GameObject.Find ("ScoreDisplay");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.Escape)) {
			if(!_isActive)
			{
				_isActive = true;
				_scoreDisplay.GetComponent<Image>().enabled = !_isActive;
				_scoreDisplay.GetComponentInChildren<Text>().enabled = !_isActive;
				_pauseMenu.GetComponent<Image>().enabled = _isActive;
				_pauseMenu.GetComponentInChildren<Text>().enabled = _isActive;
				GameManager.Instanse.SetPause(true);
			}
			else{
				_scoreDisplay.GetComponent<Image>().enabled = _isActive;
				_scoreDisplay.GetComponentInChildren<Text>().enabled = _isActive;
				_pauseMenu.GetComponent<Image>().enabled = !_isActive;
				_pauseMenu.GetComponentInChildren<Text>().enabled = !_isActive;
				_isActive = false;
				GameManager.Instanse.SetPause(false);
			}

		}
	}
}
