using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerController : MonoBehaviour, ISpeedObserver{

	List<Spawner> _spawnerList = new List<Spawner>();
	float _currentSpeed;
	float _currentTime;
	float _strapDistance = 3f;
	bool _isGamePaused;
	StrapsSpeedCalculationController _speedController;

	void Start()
	{
		_speedController = GameObject.Find ("StrapsSpeedController").GetComponent<StrapsSpeedCalculationController> ();
		_speedController.AddObserver (this);
		//Debug.Log ("SpawnerController is registered");
	}

	public SpawnerController()
	{

	}

	public void UpdateFields(float currentSpeed, float currentTime, bool isGamePaused)
	{
		CheckSpeed (currentSpeed);
		//this.currentSpeed = currentSpeed;
		_currentTime = currentTime;
		_isGamePaused = isGamePaused;

	}

	public void AddObserver(Spawner spawner)
	{
		_spawnerList.Add (spawner);
		SetStrapDistance (spawner);
	}

	public void RemoveObserver(Spawner spawner)
	{
		_spawnerList.Remove (spawner);
	}

	public void NotifyOnChangeDistance(float newStrapDistance)
	{
		//Debug.Log (strapDistance);
		foreach (var spawner in _spawnerList) {
			spawner.SetStrapDistance(newStrapDistance);	
		}
	}

	public void SetStrapDistance(Spawner spawner)
	{	
		spawner.SetStrapDistance (_strapDistance);
	}

	void CalculateDistanse(float currentSpeed)
	{
		if (currentSpeed >= 22f)
			_strapDistance = 6f;
		else if (currentSpeed >= 18f)
			_strapDistance = 5f;
		else if (currentSpeed >= 14f)
			_strapDistance = 4f;
		else if (currentSpeed >= 10f)
			_strapDistance = 3.5f;
		else if (currentSpeed >= 5f)
			_strapDistance = 3f;
		else _strapDistance = 3f;
	}


	void CheckSpeed(float newSpeed)
	{
		float newStrapDistance = 3f;
		if (newSpeed >= 22f)
			newStrapDistance = 6f;
		else if (newSpeed >= 18f)
			newStrapDistance = 5f;
		else if (newSpeed >= 14f)
			newStrapDistance = 4f;
		else if (newSpeed >= 10f)
			newStrapDistance = 3.5f;
		else if (newSpeed >= 5f)
			newStrapDistance = 3f;

		if (newStrapDistance != _strapDistance) {
			_strapDistance = newStrapDistance;
			NotifyOnChangeDistance (_strapDistance);
		}
	}
}
