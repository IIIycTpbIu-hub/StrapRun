using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	IFactory factory;
	GameObject lastObject;
	float spawnTime;
	Vector3 lastObjectPosition;
	Vector3 lastObjectRightPosition;
	Vector3 lastObjectLeftPosition;
	Vector3 objectStartPosition;
	float strapDistance;
	float lightBurstDistance = 1000f;
	SpawnerController strapSpawnerController;
	LightBurstSpawnController lightBurstSpawnController;
	MOVING_DIRECTION movingDiraction = MOVING_DIRECTION.LEFT;
	public PRODUCT_TYPE ObjectType;
	public Vector3 spawnerCoordinates { get; private set;}

	public MOVING_DIRECTION MovingDiraction {
		get {return movingDiraction;}
		set {movingDiraction = value;}
	} 

	void Awake()
	{

	}
	void Start () {
		//spawnTime = Random.Range (2f, 3f);
		//InvokeRepeating ("SpawnObject", 0, spawnTime);
		//this.SpawnObject (PRODUCT_TYPE.STRAP);
		if (ObjectType == PRODUCT_TYPE.STRAP) {
			strapSpawnerController = GameObject.Find("SpawnerController").GetComponent<SpawnerController>();
			strapSpawnerController.AddObserver(this);
			SpawnObject (ObjectType);
		}
		if (ObjectType == PRODUCT_TYPE.LIGHT_BUSRST) {
			lightBurstSpawnController = GameObject.Find ("LightBurstSpawnController").GetComponent<LightBurstSpawnController> ();
			lightBurstSpawnController.AddObserver (this);
			lastObject = GameObject.Find("Player");
		}

	}
	
	// Update is called once per frame
	void Update () {
		if(ObjectType == PRODUCT_TYPE.STRAP)
			CheckStrapSpawn ();
	}
	
	public void SpawnObject(PRODUCT_TYPE objectType)
	{
		ObjectType = objectType;
		switch (ObjectType) {

			case PRODUCT_TYPE.STRAP :
			{
				factory = new StrapsFactory();
				//Debug.Log(lastStrap.name);
			} break;

			case PRODUCT_TYPE.LIGHT_BUSRST :
			{
				factory = new LightBurstFactory();
			} break;
		}
		GameObject prefab = factory.CreateProduct();
		objectStartPosition = gameObject.transform.position;
		objectStartPosition.x += prefab.transform.localScale.x/2;
		lastObject = GameObject.Instantiate(prefab, objectStartPosition, Quaternion.Euler(0,0,0)) as GameObject;
	}

	public void SetStrapDistance(float newDistance)
	{
		strapDistance = newDistance;
		//Debug.Log ("Новая дистанция! " + strapDistance);
	}

	void CheckStrapSpawn()
	{
		lastObjectLeftPosition = lastObject.transform.position;
		lastObjectLeftPosition.x -= lastObject.transform.localScale.x / 2;
		float distanseToLastStrap = Vector3.Distance (this.transform.position, lastObjectLeftPosition);
		if (distanseToLastStrap > lastObject.transform.localScale.x + strapDistance)
			SpawnObject (ObjectType);
	}

}
	
