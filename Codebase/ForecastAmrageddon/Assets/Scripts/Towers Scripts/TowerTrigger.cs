using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTrigger : MonoBehaviour
{
    //The purpose of this Script is to act as the scope for the tower

    // Credits:  Gabriel Belchie, Ethan Stanks (Additional Targeting)

    // Data Members
    public List<GameObject> mPriorityTargetList = new List<GameObject>(); // What the tower uses to target enemies in range
    public bool isSupportTower;
    public GameObject[] mTowersInRange;
    public Vector3 mTowerLocation;

    private void Start()
    {
        InvokeRepeating(nameof(NewTowerTarget), 0f, .2f);


        if (isSupportTower == true)
            InvokeRepeating(nameof(GetSupportTargets), 0f, .2f);

        InvokeRepeating(nameof(ForceRemove), 8f, 5f);
    }

    void NewTowerTarget()  // Gets a new Target for tower
    {

        if (mPriorityTargetList.Count > 0)
        {
            GetComponentInParent<BasicTowerScript>().mTarget = mPriorityTargetList[0]; // Sets the Tower's "Scope" to the 1st enemy it sees
        }

    }

    void GetSupportTargets()
    {
        mTowersInRange = GameObject.FindGameObjectsWithTag("Tower");  // Finds All towers and stores them

        int index = 0;    // Controls the index of the array while in the loop

        foreach (GameObject tower in mTowersInRange)
        {
            float _mValidDistance = Vector3.Distance(transform.position, tower.transform.position);   // Gets distance between the Support and other tower

            if (_mValidDistance < GetComponentInParent<BasicTowerScript>().mEffectRange && tower.transform.position != null)
            {
                GetComponentInParent<BasicTowerScript>().mSupportTargets.Add(mTowersInRange[index]);  // Adds the valid target to the Support Tower's List
            }
            else
            {
                GetComponentInParent<BasicTowerScript>().mSupportTargets.Remove(tower);
            }
            index++;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")) // if the game object entering the trigger is an enemy
        {
            GameObject enemy = other.gameObject;
            mPriorityTargetList.Add(enemy); // add the enemy to the tower's priority list
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (mPriorityTargetList.Contains(other.gameObject)) // if the game object that is leaving is an enemy
        {
            mPriorityTargetList.Remove(other.gameObject); // remove it from the tower's priority list
        }
        GetComponentInParent<BasicTowerScript>().mTarget = null;
    }

    private void ForceRemove()   // Used to deal with the edge case of the targeter overlapping the Base and making the tower's target become invalid
    {
        mPriorityTargetList.Remove(GetComponentInParent<BasicTowerScript>().mTarget);
    }
}

