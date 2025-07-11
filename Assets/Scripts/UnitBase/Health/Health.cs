using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float curentHealth;
    public PlayingPanle playingPanle;
    public void Init(UnitConfig config)
    {
        maxHealth = config.maxHP;
        curentHealth = maxHealth;
    }
    public void CheckCurrentHealth()
    {
        if (curentHealth <= 0)
        {
            if (GetComponent<TargetFilterData>().unitType == UnitType.Player_MainHall)
            {
                GameStateMachine.instance.ChangeState(GameStateMachine.instance.gameOverState);
                playingPanle.ChangeToEndGamePanle();
                return;
            }
            if (GetComponent<TargetFilterData>().unitType == UnitType.Enemy_Boss)
            {
                return;
            }
            Destroy(gameObject);
        }
    }
}
