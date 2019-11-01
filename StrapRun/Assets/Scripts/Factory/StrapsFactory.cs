using UnityEngine;
using System.Collections;

public class StrapsFactory :  IFactory {

	enum STRAP_TYPE { SMALL, DEFAULT, LARGE}
	STRAP_TYPE strapType;
	int randomIndex;
	GameObject strap;

	public GameObject CreateProduct()
	{
		randomIndex = UnityEngine.Random.Range (0, 2);
		switch (randomIndex) {
		case 0: strap = Resources.Load("Prefabs/Straps/SmallStrap") as GameObject;
			break;
		case 1 : strap = Resources.Load("Prefabs/Straps/MediumStrap") as GameObject;
			break;
		case 2: strap = Resources.Load("Prefabs/Straps/LargeStrap") as GameObject;
			break;
		}
		return strap;
	}

}
