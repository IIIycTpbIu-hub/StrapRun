using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

	public Animator animationController;

	Vector3 _destinationPoint;
	Vector3 _upSide;
	Vector3 _downSide;
	float _speed;
	bool _inAir;

	// Use this for initialization
	void Start () {
		_upSide = gameObject.transform.position;
		_upSide.y = 100;
		_downSide = gameObject.transform.position;
		_downSide.y = -100; 
		//upSide = transform.GetChild (0).transform.position;
		//downSide = transform.GetChild (1).transform.position;
		_destinationPoint = _upSide;
		_speed = 15f;
		_inAir = true;
	}
	
	// Update is called once per frame
	void Update () {

		Move ();
	}

	void Move()
	{
		if (Input.GetKey(KeyCode.Space) && !_inAir && !GameManager.Instanse.GamePause) {
			if(_destinationPoint == _upSide) 
			{
				_destinationPoint = _downSide;
				animationController.SetBool("upSide", false);
			}
			else 
			{
				_destinationPoint = _upSide;
				animationController.SetBool("upSide", true);
			}
			_inAir = true;
		}
		if (_inAir && !GameManager.Instanse.GamePause) {
			gameObject.transform.position = Vector3.MoveTowards (
				this.transform.position, _destinationPoint, Time.deltaTime * _speed);
			gameObject.transform.rotation = Quaternion.Euler (0, 0, 0);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		_inAir = false;
	}
	void OnTriggerExit(Collider other)
	{
		_inAir = true;
	}
}
