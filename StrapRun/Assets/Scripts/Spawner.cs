using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public PRODUCT_TYPE ObjectType;

	IFactory _factory;
	GameObject _lastObject;
	float _spawnTime;
	Vector3 _lastObjectPosition;
	Vector3 _lastObjectRightPosition;
	Vector3 _lastObjectLeftPosition;
	Vector3 _objectStartPosition;
	float _strapDistance;
	float _lightBurstDistance = 1000f;
	SpawnerController _strapSpawnerController;
	LightBurstSpawnController _lightBurstSpawnController;
	MOVING_DIRECTION _movingDiraction = MOVING_DIRECTION.LEFT;

	public Vector3 spawnerCoordinates { get; private set;}

	public MOVING_DIRECTION MovingDiraction {
		get {return _movingDiraction;}
		set {_movingDiraction = value;}
	} 

	void Awake()
	{

	}
	void Start () {
		//spawnTime = Random.Range (2f, 3f);
		//InvokeRepeating ("SpawnObject", 0, spawnTime);
		//this.SpawnObject (PRODUCT_TYPE.STRAP);
		if (ObjectType == PRODUCT_TYPE.STRAP) {
			_strapSpawnerController = GameObject.Find("SpawnerController").GetComponent<SpawnerController>();
			_strapSpawnerController.AddObserver(this);
			SpawnObject (ObjectType);
		}
		if (ObjectType == PRODUCT_TYPE.LIGHT_BUSRST) {
			_lightBurstSpawnController = GameObject.Find ("LightBurstSpawnController").GetComponent<LightBurstSpawnController> ();
			_lightBurstSpawnController.AddObserver (this);
			_lastObject = GameObject.Find("Player");
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
				_factory = new StrapsFactory();
				//Debug.Log(lastStrap.name);
			} break;

			case PRODUCT_TYPE.LIGHT_BUSRST :
			{
				_factory = new LightBurstFactory();
			} break;
		}
		GameObject prefab = _factory.CreateProduct();
		_objectStartPosition = gameObject.transform.position;
		_objectStartPosition.x += prefab.transform.localScale.x/2;
		_lastObject = GameObject.Instantiate(prefab, _objectStartPosition, Quaternion.Euler(0,0,0)) as GameObject;
	}

	public void SetStrapDistance(float newDistance)
	{
		_strapDistance = newDistance;
		//Debug.Log ("Новая дистанция! " + strapDistance);
	}

	void CheckStrapSpawn()
	{
		_lastObjectLeftPosition = _lastObject.transform.position;
		_lastObjectLeftPosition.x -= _lastObject.transform.localScale.x / 2;
		float distanseToLastStrap = Vector3.Distance (this.transform.position, _lastObjectLeftPosition);
		if (distanseToLastStrap > _lastObject.transform.localScale.x + _strapDistance)
			SpawnObject (ObjectType);
	}

}
	
