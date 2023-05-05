using System.Collections.Generic;
using UnityEngine;

public class TowerBulletScript : MonoBehaviour
{
    // The purpose of this Script is to ensure the collision of a Tower's bullet with it's Targeted enemy
    
    // Credits:  Gabriel Belchie, Casey Vue

    // Data Member
    public float mBulletDamage;

    void OnCollisionEnter(Collision other)       // Make sure to turn on the Collider on the Bullet prefab for this to work
    {
        // tower bullet
        if (other.gameObject.CompareTag("Enemy"))//touches enemy
        {
            other.gameObject.GetComponent<BasicEnemyScript>().EnemyDamaged(mBulletDamage, false); // Uses the enemy's damage function from the Enemy Scripts
            Destroy(gameObject); // Destroys the Bullet in the end
        }
    }

    
}