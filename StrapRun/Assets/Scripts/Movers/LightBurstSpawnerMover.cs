using UnityEngine;
using System.Collections;

public class LightBurstSpawnerMover : MonoBehaviour {

	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = Vector3.MoveTowards (
			this.transform.position, new Vector3 (this.transform.position.x, player.transform.position.y,
		                                     this.transform.position.z), Time.deltaTime * 10f);
	}
}
