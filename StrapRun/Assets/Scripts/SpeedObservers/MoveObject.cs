using UnityEngine;
using System.Collections;

public enum OBJECT_TYPE {STRAP, LIGHTSHOT}
public enum MOVING_DIRECTION {LEFT, RIGHT}

public class MoveObject : MonoBehaviour, ISpeedObserver {

	public OBJECT_TYPE movingObjectType;
	public MOVING_DIRECTION movingDiraction;
	float speed;
	float maxAliveTime;
	float bornObjectTime;
	float currentTime;
	bool isGamePaused = false;
	bool isInitialized = false;
	Vector3 leftDirection;
	Vector3 rightDirection;
	StrapsSpeedCalculationController strapsSpeedController;
	
	// Use this for initialization
	void Start () {
		movingDiraction = MOVING_DIRECTION.LEFT;
		RegisterToSubject ();
		SetDiractionPoints ();
		if (movingObjectType == OBJECT_TYPE.LIGHTSHOT)
			speed = 30f;
	}
	
	// Update is called once per frame


	public void UpdateFields(float newSpeed, float currentTime, bool isGamePaused)
	{
		if (!isInitialized)
			SetMaxAliveTime (currentTime);
		speed = newSpeed;
		this.isGamePaused = isGamePaused;
		this.currentTime = currentTime;
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
		if(!isGamePaused)
			Move ();
	}
	
	void Move()
	{

		if (gameObject != null) {
			switch (movingDiraction) {
			case MOVING_DIRECTION.LEFT: {
				gameObject.transform.position = Vector3.MoveTowards (
					this.transform.position, leftDirection, Time.deltaTime * speed);
			}
				break;
			case MOVING_DIRECTION.RIGHT:
			{
				gameObject.transform.position = Vector3.MoveTowards (
					this.transform.position, rightDirection, Time.deltaTime * speed);
			}
				break;
			}
			
		}
	}

	void RegisterToSubject()
	{
		strapsSpeedController = GameObject.Find ("StrapsSpeedController").GetComponent<StrapsSpeedCalculationController>();
		if(movingObjectType == OBJECT_TYPE.STRAP)
			strapsSpeedController.AddObserver (this);

	}

	void SetDiractionPoints()
	{
		leftDirection = gameObject.transform.position;
		leftDirection.x = -1000;
		rightDirection = gameObject.transform.position;
		rightDirection.x = 1000;
	}

	void CheckObjectDestroy()
	{
		if (currentTime > maxAliveTime) {
			strapsSpeedController.RemoveObserver(this);
			Destroy (this.gameObject);
			//Debug.Log("Устрой дестрой");
		}
	}

	void SetMaxAliveTime(float currentTime)
	{
		maxAliveTime = currentTime + 10f;
		isInitialized = true;
	}
}
