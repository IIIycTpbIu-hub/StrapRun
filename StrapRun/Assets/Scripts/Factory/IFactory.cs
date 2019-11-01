using UnityEngine;
using System.Collections;

public enum PRODUCT_TYPE {STRAP, LIGHT_BUSRST};

public interface IFactory
{
	GameObject CreateProduct();
}
