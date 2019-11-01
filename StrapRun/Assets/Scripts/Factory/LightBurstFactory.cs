using UnityEngine;
using System.Collections;

public class LightBurstFactory : IFactory {

	GameObject lightBurst;

	public GameObject CreateProduct()
	{
		lightBurst = Resources.Load("Prefabs/LightBurst") as GameObject;
		return lightBurst;
	}

}
