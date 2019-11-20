using UnityEngine;
using System.Collections;

public class LightBurstFactory : IFactory {

	GameObject _lightBurst;

	public GameObject CreateProduct()
	{
		_lightBurst = Resources.Load("Prefabs/LightBurst") as GameObject;
		return _lightBurst;
	}

}
