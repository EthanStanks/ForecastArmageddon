using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyBulletScript : MonoBehaviour
{
    [SerializeField] int destoryTime = 60;
    [SerializeField] int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destoryTime);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Tower"))
        {
            other.transform.GetComponent<BasicTowerScript>().TowerDamaged(damage);
            Destroy(gameObject);
        }
    }



}
