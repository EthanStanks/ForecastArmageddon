using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHover : MonoBehaviour
{
    public bool isAnimationed; // used to keep track if the pop up animation played
    public GameObject mSavedTower;

    private void Start()
    {
        isAnimationed = false;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // if there is no current animation and they click the left mouse button
            RaycastTower();
    }
    void Hover() // called when the mouse is over a tower
    {
        FillPopUp();
        GameManager.Instance.mHoverAnimation.SetBool("isHover", true);
    }
    void UnHover() // called when the mouse isnt over a tower
    {
        GameManager.Instance.mHoverAnimation.SetBool("isHover", false);
        isAnimationed = false;
    }

    void RaycastTower() // called in the update if check
    {
        // raycast stuff
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f, ~GameManager.Instance.ignoreLayer)) // if the raycast hit something
        {
            GameObject obj = hit.transform.gameObject;
            if (hit.transform.CompareTag("Tower")) // if the hit is a tower
            {
                if (mSavedTower != null && mSavedTower != obj)
                {
                    mSavedTower.GetComponent<BasicTowerScript>().HideTrigger(false); // hides trigger
                    UnHover();
                    mSavedTower = obj;
                    GameManager.Instance.clickedOnTowerRef = hit.transform.GetComponentInParent<BasicTowerScript>(); // sets towerRef to the hit's parent obj tower script
                    obj.GetComponent<BasicTowerScript>().HideTrigger(true); // shows trigger
                    Hover();
                }
                else
                {
                    mSavedTower = obj;
                    GameManager.Instance.clickedOnTowerRef = hit.transform.GetComponentInParent<BasicTowerScript>(); // sets towerRef to the hit's parent obj tower script
                    obj.GetComponent<BasicTowerScript>().HideTrigger(true); // shows
                    Hover();
                }
            }

            if (!hit.transform.CompareTag("Tower"))  // if the hit isnt a tower
            {
                if (mSavedTower != null)
                {
                    GameManager.Instance.clickedOnTowerRef.HideTrigger(false);
                    UnHover();
                }
            }

        }
    }

    void FillPopUp() // called to fill the card with the tower info
    {
        GameManager.Instance.towerPopUpTitle.text = GameManager.Instance.clickedOnTowerRef.mName;
        GameManager.Instance.towerPopUpHPBar.fillAmount = GameManager.Instance.clickedOnTowerRef.mHP * 0.1f;
        GameManager.Instance.towerPopUpDamageBar.fillAmount = GameManager.Instance.clickedOnTowerRef.mDamage * 0.1f;
        GameManager.Instance.towerPopUpAttackRateBar.fillAmount = GameManager.Instance.clickedOnTowerRef.mFireRate * 0.1f;
    }
}
