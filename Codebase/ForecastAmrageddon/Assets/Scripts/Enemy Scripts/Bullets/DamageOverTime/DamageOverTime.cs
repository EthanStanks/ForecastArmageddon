using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTime : MonoBehaviour
{
    [SerializeField] int destoryTime = 3; // how long it last
    [SerializeField] int damage = 1;
    [SerializeField] float corrosiveTimer = 1.5f; // how long it takes between damage;


    public void Start()
    {
        StartCoroutine(AcidAttack());
        Invoke("DestroyObj", destoryTime);
    }
    public IEnumerator AcidAttack()
    {
        while (true)
        {
            transform.GetComponent<BasicTowerScript>().TowerDamaged(damage);
            yield return new WaitForSeconds(corrosiveTimer);
        }
        
    }
    public void DestroyObj()
    {
        Destroy(this.gameObject);
        
    }
}
