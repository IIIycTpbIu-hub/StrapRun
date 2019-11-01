using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerController : MonoBehaviour, ISpeedObserver{

	List<Spawner> spawnerList = new List<Spawner>();
	float currentSpeed;
	float currentTime;
	float strapDistance = 3f;
	bool isGamePaused;
	StrapsSpeedCalculationController speedController;

	void Start()
	{
		speedController = GameObject.Find ("StrapsSpeedController").GetComponent<StrapsSpeedCalculationController> ();
		speedController.AddObserver (this);
		//Debug.Log ("SpawnerController is registered");
	}

	public SpawnerController()
	{

	}

	public void UpdateFields(float currentSpeed, float currentTime, bool isGamePaused)
	{
		CheckSpeed (currentSpeed);
		//this.currentSpeed = currentSpeed;
		this.currentTime = currentTime;
		this.isGamePaused = isGamePaused;

	}

	public void AddObserver(Spawner spawner)
	{
		spawnerList.Add (spawner);
		SetStrapDistance (spawner);
	}

	public void RemoveObserver(Spawner spawner)
	{
		spawnerList.Remove (spawner);
	}

	public void NotifyOnChangeDistance(float newStrapDistance)
	{
		//Debug.Log (strapDistance);
		foreach (var spawner in spawnerList) {
			spawner.SetStrapDistance(newStrapDistance);	
		}
	}

	public void SetStrapDistance(Spawner spawner)
	{	
		spawner.SetStrapDistance (strapDistance);
	}

	void CalculateDistanse(float currentSpeed)
	{
		if (currentSpeed >= 22f)
			strapDistance = 6f;
		else if (currentSpeed >= 18f)
			strapDistance = 5f;
		else if (currentSpeed >= 14f)
			strapDistance = 4f;
		else if (currentSpeed >= 10f)
			strapDistance = 3.5f;
		else if (currentSpeed >= 5f)
			strapDistance = 3f;
		else strapDistance = 3f;
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

		if (newStrapDistance != strapDistance) {
			strapDistance = newStrapDistance;
			NotifyOnChangeDistance (strapDistance);
		}
	}
}
