using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SludgeEffect : MonoBehaviour
{
    [SerializeField] int destoryTime = 3; // how long it last
    //[SerializeField] int damage = 1; // commented out because there is a warning for it not being used
    [SerializeField] float SlowTimer = 3f; // how long it takes between damage;


    public void Start()
    {
        StartCoroutine(SludgeAttack());
        Invoke("DestroyObj", destoryTime);
    }
    public IEnumerator SludgeAttack()
    {
        while (true)
        {
            transform.GetComponent<BasicTowerScript>().mFireRate = 3;
            yield return new WaitForSeconds(SlowTimer);
            transform.GetComponent<BasicTowerScript>().mFireRate = 6;
        }
        
    }
    public void DestroyObj()
    {
        Destroy(this.gameObject);
        
    }
}
