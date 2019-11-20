using UnityEngine;
using System.Collections;

public class LightBurstSpawnerMover : MonoBehaviour {

	GameObject _player;
	// Use this for initialization
	void Start () {
		_player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = Vector3.MoveTowards (
			this.transform.position, new Vector3 (this.transform.position.x, _player.transform.position.y,
		                                     this.transform.position.z), Time.deltaTime * 10f);
	}
}
