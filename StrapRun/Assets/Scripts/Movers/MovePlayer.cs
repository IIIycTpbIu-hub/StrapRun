using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

	Vector3 destinationPoint;
	Vector3 upSide;
	Vector3 downSide;
	float speed;
	bool inAir;
	public Animator animationController;
	// Use this for initialization
	void Start () {
		upSide = gameObject.transform.position;
		upSide.y = 100;
		downSide = gameObject.transform.position;
		downSide.y = -100; 
		//upSide = transform.GetChild (0).transform.position;
		//downSide = transform.GetChild (1).transform.position;
		destinationPoint = upSide;
		speed = 15f;
		inAir = true;
	}
	
	// Update is called once per frame
	void Update () {

		Move ();
	}

	void Move()
	{
		if (Input.GetKey(KeyCode.Space) && !inAir && !GameManager.Instanse.GamePause) {
			if(destinationPoint == upSide) 
			{
				destinationPoint = downSide;
				animationController.SetBool("upSide", false);
			}
			else 
			{
				destinationPoint = upSide;
				animationController.SetBool("upSide", true);
			}
			inAir = true;
		}
		if (inAir && !GameManager.Instanse.GamePause) {
			gameObject.transform.position = Vector3.MoveTowards (
				this.transform.position, destinationPoint, Time.deltaTime * speed);
			gameObject.transform.rotation = Quaternion.Euler (0, 0, 0);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		inAir = false;
	}
	void OnTriggerExit(Collider other)
	{
		inAir = true;
	}
}
