using UnityEngine;
using System.Collections;

public interface ISpeedObserver{

	void UpdateFields(float currentSpeed, float currentTime, bool isGamePaused);
}
