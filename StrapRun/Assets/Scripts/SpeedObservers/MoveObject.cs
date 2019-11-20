using UnityEngine;
using System.Collections;

public enum OBJECT_TYPE {STRAP, LIGHTSHOT}
public enum MOVING_DIRECTION {LEFT, RIGHT}

public class MoveObject : MonoBehaviour, ISpeedObserver {

	public OBJECT_TYPE movingObjectType;
	public MOVING_DIRECTION movingDiraction;
	float _speed;
	float _maxAliveTime;
	float _bornObjectTime;
	float _currentTime;
	bool _isGamePaused = false;
	bool _isInitialized = false;
	Vector3 _leftDirection;
	Vector3 _rightDirection;
	StrapsSpeedCalculationController _strapsSpeedController;
	
	// Use this for initialization
	void Start () {
		movingDiraction = MOVING_DIRECTION.LEFT;
		RegisterToSubject ();
		SetDiractionPoints ();
		if (movingObjectType == OBJECT_TYPE.LIGHTSHOT)
			_speed = 30f;
	}
	
	// Update is called once per frame


	public void UpdateFields(float newSpeed, float currentTime, bool isGamePaused)
	{
		if (!_isInitialized)
			SetMaxAliveTime (currentTime);
		_speed = newSpeed;
		this._isGamePaused = isGamePaused;
		this._currentTime = currentTime;
		CheckObjectDestroy ();
	}

	public void SetDiraction(MOVING_DIRECTION diraction)
	{
		movingDiraction = diraction;
	}

	void Update () {
//		currentAlifeTime += Time.deltaTime;
//		if (currentAlifeTime > maxAlifeTime - 1f && movingObjectType == OBJECT_TYPE.STRAP)
//		{
//			Debug.Log ("Удаляюсь");
//			strapsSpeedController.RemoveObserver (this);
//		}
		if(!_isGamePaused)
			Move ();
	}
	
	void Move()
	{

		if (gameObject != null) {
			switch (movingDiraction) {
			case MOVING_DIRECTION.LEFT: {
				gameObject.transform.position = Vector3.MoveTowards (
					this.transform.position, _leftDirection, Time.deltaTime * _speed);
			}
				break;
			case MOVING_DIRECTION.RIGHT:
			{
				gameObject.transform.position = Vector3.MoveTowards (
					this.transform.position, _rightDirection, Time.deltaTime * _speed);
			}
				break;
			}
			
		}
	}

	void RegisterToSubject()
	{
		_strapsSpeedController = GameObject.Find ("StrapsSpeedController").GetComponent<StrapsSpeedCalculationController>();
		if(movingObjectType == OBJECT_TYPE.STRAP)
			_strapsSpeedController.AddObserver (this);

	}

	void SetDiractionPoints()
	{
		_leftDirection = gameObject.transform.position;
		_leftDirection.x = -1000;
		_rightDirection = gameObject.transform.position;
		_rightDirection.x = 1000;
	}

	void CheckObjectDestroy()
	{
		if (_currentTime > _maxAliveTime) {
			_strapsSpeedController.RemoveObserver(this);
			Destroy (this.gameObject);
			//Debug.Log("Устрой дестрой");
		}
	}

	void SetMaxAliveTime(float currentTime)
	{
		_maxAliveTime = currentTime + 10f;
		_isInitialized = true;
	}
}
