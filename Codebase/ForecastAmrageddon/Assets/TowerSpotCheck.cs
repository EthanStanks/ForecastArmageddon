using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpotCheck : MonoBehaviour
{
    public bool isUsed; // tell us if the spot has the tag UsedTowerSpot
    public bool isOrphan; // tells us if the spot has a tower on it or not

    void Start()
    {
        isUsed = false;
        isOrphan = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (isUsed)
        {
            isOrphan = false;
            for (int i = 0; i < GameManager.Instance.activeTowerInGame.Count; ++i)
            {
                if (GameManager.Instance.activeTowerInGame[i] != null) // if this tower functioning 
                {
                    if (GameManager.Instance.activeTowerInGame[i].GetComponent<BasicTowerScript>().mSpotRef == gameObject) // if the tower spot reference is the same has this spot
                    {
                        isOrphan = false;
                        break;
                    }
                    else isOrphan = true;
                }
                else isOrphan = true;
            }
            if (isOrphan)
            {
                isUsed = false;
                gameObject.tag = "TowerSpot";
            }
        }
    }
}
