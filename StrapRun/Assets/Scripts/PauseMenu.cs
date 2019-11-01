using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

	GameObject pauseMenu;
	GameObject scoreDisplay;
	bool isActive;
	// Use this for initialization
	void Start () {
		isActive = false;
		pauseMenu = GameObject.Find ("PausePanel");
		scoreDisplay = GameObject.Find ("ScoreDisplay");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.Escape)) {
			if(!isActive)
			{
				isActive = true;
				scoreDisplay.GetComponent<Image>().enabled = !isActive;
				scoreDisplay.GetComponentInChildren<Text>().enabled = !isActive;
				pauseMenu.GetComponent<Image>().enabled = isActive;
				pauseMenu.GetComponentInChildren<Text>().enabled = isActive;
				GameManager.Instanse.SetPause(true);
			}
			else{
				scoreDisplay.GetComponent<Image>().enabled = isActive;
				scoreDisplay.GetComponentInChildren<Text>().enabled = isActive;
				pauseMenu.GetComponent<Image>().enabled = !isActive;
				pauseMenu.GetComponentInChildren<Text>().enabled = !isActive;
				isActive = false;
				GameManager.Instanse.SetPause(false);
			}

		}
	}
}
