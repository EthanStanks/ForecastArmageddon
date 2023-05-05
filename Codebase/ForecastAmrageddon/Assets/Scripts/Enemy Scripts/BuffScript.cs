using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))//touches base
        {
            other.transform.GetComponent<BasicEnemyScript>().mHealth += 2;

        }
                
    }
}
