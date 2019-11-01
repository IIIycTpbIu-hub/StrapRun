using UnityEngine;
using System.Collections;

public interface IGameManagerObserver
{
	void UpdateFields(float time, GAME_DIFFICULTY difficulty, bool isGamePaused, bool isPlayerDead, MOVING_DIRECTION movingDiraction);
}
