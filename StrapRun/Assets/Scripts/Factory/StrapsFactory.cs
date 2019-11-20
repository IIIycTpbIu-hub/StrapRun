using UnityEngine;
using System.Collections;

public class StrapsFactory :  IFactory {

	enum STRAP_TYPE { SMALL, DEFAULT, LARGE}
	int _randomIndex;
	GameObject _strap;

	public GameObject CreateProduct()
	{
		_randomIndex = UnityEngine.Random.Range (0, 2);
		switch (_randomIndex) {
		case 0: _strap = Resources.Load("Prefabs/Straps/SmallStrap") as GameObject;
			break;
		case 1 : _strap = Resources.Load("Prefabs/Straps/MediumStrap") as GameObject;
			break;
		case 2: _strap = Resources.Load("Prefabs/Straps/LargeStrap") as GameObject;
			break;
		}
		return _strap;
	}

}
