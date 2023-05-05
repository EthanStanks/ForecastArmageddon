using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportTowerTrigger : MonoBehaviour
{
    public GameObject[] mTowersInRange;
    public Vector3 mTowerLocation;

    private void Start()
    {
        InvokeRepeating(nameof(GetTowerTargets), 0f, .2f);
    }

    void GetTowerTargets()
    {
        mTowersInRange = GameObject.FindGameObjectsWithTag("Tower");  // Finds All towers and stores them

        int index = 0;    // Controls the index of the array while in the loop

        foreach (GameObject tower in mTowersInRange)
        {
            float _mValidDistance = Vector3.Distance(transform.position, tower.transform.position);   // Gets distance between the Support and other tower

            if (_mValidDistance < GetComponentInParent<BasicTowerScript>().mEffectRange)
            {
                GetComponentInParent<BasicTowerScript>().mSupportTargets.Add(mTowersInRange[index]);  // Adds the valid target to the Support Tower's List
            }
            index++;
        }


    }
}