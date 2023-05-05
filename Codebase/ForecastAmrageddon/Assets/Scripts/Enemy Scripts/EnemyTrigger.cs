using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Base"))//touches base
        {
            GameManager.Instance.baseHealthCurrent -= GetComponentInParent<BasicEnemyScript>().mDamage;
            GameManager.Instance.UpdateHudVisuals(); // needs to call this so it can update the base health bar
            if (GameManager.Instance.baseHealthCurrent <= 0)
            {
                GameManager.Instance.Loser();
            }
            GameManager.Instance.activeEnemiesInGame.Remove(other.gameObject);
            GameManager.Instance.enemyCount--;
            GameManager.Instance.LastRound(); // called to see if the player won or lost if this dies in the 15th round
            GetComponent<BasicEnemyScript>().Died(true);
       //     Destroy(gameObject);
        }
        if (GetComponentInParent<BasicEnemyScript>())
        {
            if (GetComponentInParent<BasicEnemyScript>().mTarget == null && other.gameObject.CompareTag("Tower"))//touches tower
            {
                GetComponentInParent<BasicEnemyScript>().mTarget = other.gameObject;
                if (GetComponentInParent<BasicEnemyScript>().mCanFire)
                {
                    GetComponentInParent<BasicEnemyScript>().FireBullet();
                    GetComponentInParent<BasicEnemyScript>().mCanFire = false;
                }
                //adding stuff for paticles
                if (GetComponentInParent<ShootParticleScript>())
                {
                    GetComponentInParent<ShootParticleScript>().DoParticle();
                }
            }

        }
      

    }
    private void OnTriggerExit(Collider other)
    {
        if (GetComponentInParent<BasicEnemyScript>())
        {
            GetComponentInParent<BasicEnemyScript>().mTarget = null;
        }
    }
    
}
