using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScript : MonoBehaviour
{
    public Shaker Shaker;
    public float duration = 1f;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))//touches base
        {
            if (GameManager.Instance.playingMapNum != 4)
                Shaker.Shake(duration);

            GameObject Enemy = other.gameObject;
            GameManager.Instance.baseHealthCurrent -= Enemy.GetComponent<BasicEnemyScript>().mDamage;
            GameManager.Instance.UpdateHudVisuals(); // needs to call this so it can update the base health bar
            if (GameManager.Instance.baseHealthCurrent <= 0)
            {
                GameManager.Instance.Loser();
            }
            Enemy.GetComponent<BasicEnemyScript>().EnemyDamaged(999999, true);
            //GameManager.Instance.LastRound(); // called to see if the player won or lost if this dies in the 15th round
            //Destroy(Enemy);
        }
    }
}
