using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SludgeProjectile : MonoBehaviour
{
    [SerializeField] int destoryTime = 60;
    void Start()
    {
        Destroy(gameObject, destoryTime);
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Tower"))
        {
            other.gameObject.AddComponent<SludgeEffect>();
        }
    }
  

}