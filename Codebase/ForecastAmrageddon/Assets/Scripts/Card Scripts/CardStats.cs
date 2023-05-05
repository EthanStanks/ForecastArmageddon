using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardStats : MonoBehaviour
{
    public int mHP, mDamage, mFireRate, mCardSlot; // used to trasnfer this card's data to a new tower 
    public string mName, mDesc; // used to know what card it is exactly
    public Sprite mCardSprite; // used to update the card's image
    public bool mIsTower, mIsTrap, mIsBuff; // used to see what type of card it is
    public int mBuffCardIndex; // -1 is for nonbuff cards, 
    public int mTrapCardIndex; // -1 is for nontrap cards
    public bool isEnhanced;

    // public int mEnhancementRoundTracker; // Keeps track of the current round for enhancing cards

    private void Start()
    {
        SetSlotCard();
        isEnhanced = false;

    }
    private void Update()
    {
        MoveCard();
    }
    public void UpdateSprite()
    {
        GetComponent<Image>().sprite = mCardSprite; // sets the card's sprite to the member field sprite
    }
    public void SetSlotCard()
    {
        switch (mCardSlot)
        {
            case 1:
                GameManager.Instance.slotOneCard = transform.parent.gameObject;
                break;
            case 2:
                GameManager.Instance.slotTwoCard = transform.parent.gameObject;
                break;
            case 3:
                GameManager.Instance.slotThreeCard = transform.parent.gameObject;
                break;
            case 4:
                GameManager.Instance.slotFourCard = transform.parent.gameObject;
                break;
            case 5:
                GameManager.Instance.slotFiveCard = transform.parent.gameObject;
                break;
            case 6:
                GameManager.Instance.slotNewCard = transform.parent.gameObject;
                break;
        }

    }
    public void CardClicked()
    {
        if (mTrapCardIndex != 1 && GameManager.Instance.trashCount != GameManager.Instance.maxTrash) // if the current count now equal the max trash count after trashing that card
        {
            GameManager.Instance.trashButton.SetActive(true);
            GameManager.Instance.trashLockedButton.SetActive(false);
        }
        else if (mTrapCardIndex != 1 && GameManager.Instance.trashCount == GameManager.Instance.maxTrash) // if the current count now equal the max trash count after trashing that card
        {
            GameManager.Instance.trashButton.SetActive(false);
            GameManager.Instance.trashLockedButton.SetActive(true);
        }
        else if (mTrapCardIndex == 1)
        {
            GameManager.Instance.trashButton.SetActive(false); // disable the trash card button
            GameManager.Instance.trashLockedButton.SetActive(false);
        }

        GameManager.Instance.clickedCard = mCardSlot; // tells system what card they clicked on
        GameManager.Instance.cardPopupCard.GetComponent<Image>().sprite = mCardSprite;
        GameManager.Instance.mPopUpDesc.text = mDesc;

        GameManager.Instance.cardPopup.SetActive(true);
    }

    IEnumerator AmbushCheck(float wait)
    {
        yield return new WaitForSeconds(wait);
        GameManager.Instance.AmbushCheck();
    }
    /////Dont touch//////////
    #region Card Moving Conveyor Belt
    void MoveCard()
    {
        switch (mCardSlot)
        {
            case 1:
                break;
            case 2:
                IfCardSlot2();
                break;
            case 3:
                IfCardSlot3();
                break;
            case 4:
                IfCardSlot4();
                break;
            case 5:
                IfCardSlot5();
                break;
            case 6:
                IfCardSlot6();
                break;

        }

    }
    void IfCardSlot6()
    {
        if (!GameManager.Instance.isSlot5Filled)
        {
            if (!GameManager.Instance.isSlot4Filled)
            {
                if (!GameManager.Instance.isSlot3Filled)
                {
                    if (!GameManager.Instance.isSlot2Filled)
                    {
                        if (!GameManager.Instance.isSlot1Filled)
                        {
                            MoveConveyorFive();
                            MoveConveyorFour();
                            MoveConveyorThree();
                            MoveConveyorTwo();
                            MoveConveyorOne();
                            GetComponentInParent<Animator>().Play("SixToOne");
                            StartCoroutine(StopConveyorOne(15));
                            mCardSlot = 1;
                            SetSlotCard();
                            GameManager.Instance.cardOneScript = this;
                            GameManager.Instance.isSlot1Filled = true;
                            GameManager.Instance.isSlot6Filled = false;
                        }
                        else
                        {
                            MoveConveyorFive();
                            MoveConveyorFour();
                            MoveConveyorThree();
                            MoveConveyorTwo();
                            GetComponentInParent<Animator>().Play("SixToTwo");
                            StartCoroutine(StopConveyorTwo(12));
                            mCardSlot = 2;
                            SetSlotCard();
                            GameManager.Instance.cardTwoScript = this;
                            GameManager.Instance.isSlot2Filled = true;
                            GameManager.Instance.isSlot6Filled = false;
                        }
                    }
                    else
                    {
                        MoveConveyorFive();
                        MoveConveyorFour();
                        MoveConveyorThree();
                        GetComponentInParent<Animator>().Play("SixToThree");
                        StartCoroutine(StopConveyorThree(9));
                        mCardSlot = 3;
                        SetSlotCard();
                        GameManager.Instance.cardThreeScript = this;
                        GameManager.Instance.isSlot3Filled = true;
                        GameManager.Instance.isSlot6Filled = false;
                    }
                }
                else
                {
                    MoveConveyorFive();
                    MoveConveyorFour();
                    GetComponentInParent<Animator>().Play("SixToFour");
                    StartCoroutine(StopConveyorFour(6));
                    mCardSlot = 4;
                    SetSlotCard();
                    GameManager.Instance.cardFourScript = this;
                    GameManager.Instance.isSlot4Filled = true;
                    GameManager.Instance.isSlot6Filled = false;
                }
            }
            else
            {
                MoveConveyorFive();
                GetComponentInParent<Animator>().Play("SixToFive");
                StartCoroutine(StopConveyorFive(3));
                mCardSlot = 5;
                SetSlotCard();
                GameManager.Instance.cardFiveScript = this;
                GameManager.Instance.isSlot5Filled = true;
                GameManager.Instance.isSlot6Filled = false;
                StartCoroutine(AmbushCheck(3));
            }
        }
    }
    void IfCardSlot5()
    {
        if (!GameManager.Instance.isSlot4Filled)
        {
            if (!GameManager.Instance.isSlot3Filled)
            {
                if (!GameManager.Instance.isSlot2Filled)
                {
                    if (!GameManager.Instance.isSlot1Filled)
                    {
                        MoveConveyorFive();
                        MoveConveyorFour();
                        MoveConveyorThree();
                        MoveConveyorTwo();
                        MoveConveyorOne();
                        GetComponentInParent<Animator>().Play("FiveToOne");
                        StartCoroutine(StopConveyorOne(12));
                        mCardSlot = 1;
                        SetSlotCard();
                        GameManager.Instance.cardOneScript = this;
                        GameManager.Instance.isSlot1Filled = true;
                        GameManager.Instance.isSlot5Filled = false;
                    }
                    else
                    {
                        MoveConveyorFive();
                        MoveConveyorFour();
                        MoveConveyorThree();
                        MoveConveyorTwo();
                        GetComponentInParent<Animator>().Play("FiveToTwo");
                        StartCoroutine(StopConveyorTwo(9));
                        mCardSlot = 2;
                        SetSlotCard();
                        GameManager.Instance.cardTwoScript = this;
                        GameManager.Instance.isSlot2Filled = true;
                        GameManager.Instance.isSlot5Filled = false;
                    }
                }
                else
                {
                    MoveConveyorFive();
                    MoveConveyorFour();
                    MoveConveyorThree();
                    GetComponentInParent<Animator>().Play("FiveToThree");
                    StartCoroutine(StopConveyorThree(6));
                    mCardSlot = 3;
                    SetSlotCard();
                    GameManager.Instance.cardThreeScript = this;
                    GameManager.Instance.isSlot3Filled = true;
                    GameManager.Instance.isSlot5Filled = false;
                }
            }
            else
            {
                MoveConveyorFive();
                MoveConveyorFour();
                GetComponentInParent<Animator>().Play("FiveToFour");
                StartCoroutine(StopConveyorFour(3));
                mCardSlot = 4;
                SetSlotCard();
                GameManager.Instance.cardFourScript = this;
                GameManager.Instance.isSlot4Filled = true;
                GameManager.Instance.isSlot5Filled = false;
            }
        }
    }
    void IfCardSlot4()
    {
        if (!GameManager.Instance.isSlot3Filled)
        {
            if (!GameManager.Instance.isSlot2Filled)
            {
                if (!GameManager.Instance.isSlot1Filled)
                {
                    MoveConveyorFour();
                    MoveConveyorThree();
                    MoveConveyorTwo();
                    MoveConveyorOne();
                    GetComponentInParent<Animator>().Play("FourToOne");
                    StartCoroutine(StopConveyorOne(9));
                    mCardSlot = 1;
                    SetSlotCard();
                    GameManager.Instance.cardOneScript = this;
                    GameManager.Instance.isSlot1Filled = true;
                    GameManager.Instance.isSlot4Filled = false;
                }
                else
                {
                    MoveConveyorFour();
                    MoveConveyorThree();
                    MoveConveyorTwo();
                    GetComponentInParent<Animator>().Play("FourToTwo");
                    StartCoroutine(StopConveyorOne(6));
                    mCardSlot = 2;
                    SetSlotCard();
                    GameManager.Instance.cardTwoScript = this;
                    GameManager.Instance.isSlot2Filled = true;
                    GameManager.Instance.isSlot4Filled = false;
                }
            }
            else
            {
                MoveConveyorFour();
                MoveConveyorThree();
                GetComponentInParent<Animator>().Play("FourToThree");
                StartCoroutine(StopConveyorThree(3));
                mCardSlot = 3;
                SetSlotCard();
                GameManager.Instance.cardThreeScript = this;
                GameManager.Instance.isSlot3Filled = true;
                GameManager.Instance.isSlot4Filled = false;
            }
        }
    }
    void IfCardSlot3()
    {
        if (!GameManager.Instance.isSlot2Filled)
        {
            if (!GameManager.Instance.isSlot1Filled)
            {
                MoveConveyorThree();
                MoveConveyorTwo();
                MoveConveyorOne();
                GetComponentInParent<Animator>().Play("ThreeToOne");
                StartCoroutine(StopConveyorOne(6));
                mCardSlot = 1;
                SetSlotCard();
                GameManager.Instance.cardOneScript = this;
                GameManager.Instance.isSlot1Filled = true;
                GameManager.Instance.isSlot3Filled = false;
            }
            else
            {
                MoveConveyorThree();
                MoveConveyorTwo();
                GetComponentInParent<Animator>().Play("ThreeToTwo");
                StartCoroutine(StopConveyorTwo(3));
                mCardSlot = 2;
                SetSlotCard();
                GameManager.Instance.cardTwoScript = this;
                GameManager.Instance.isSlot2Filled = true;
                GameManager.Instance.isSlot3Filled = false;
            }
        }
    }
    void IfCardSlot2()
    {
        if (!GameManager.Instance.isSlot1Filled)
        {
            MoveConveyorTwo();
            MoveConveyorOne();
            GetComponentInParent<Animator>().Play("TwoToOne");
            StartCoroutine(StopConveyorOne(3));
            mCardSlot = 1;
            SetSlotCard();
            GameManager.Instance.cardOneScript = this;
            GameManager.Instance.isSlot1Filled = true;
            GameManager.Instance.isSlot2Filled = false;
        }
    }

    IEnumerator StopConveyorOne(float wait)
    {
        yield return new WaitForSeconds(wait);
        GameManager.Instance.mConveyor1Moving.enabled = false;
    }
    IEnumerator StopConveyorTwo(float wait)
    {
        yield return new WaitForSeconds(wait);
        GameManager.Instance.mConveyor2Moving.enabled = false;
    }
    IEnumerator StopConveyorThree(float wait)
    {
        yield return new WaitForSeconds(wait);
        GameManager.Instance.mConveyor3Moving.enabled = false;
    }
    IEnumerator StopConveyorFour(float wait)
    {
        yield return new WaitForSeconds(wait);
        GameManager.Instance.mConveyor4Moving.enabled = false;
    }
    IEnumerator StopConveyorFive(float wait)
    {
        yield return new WaitForSeconds(wait);
        GameManager.Instance.mConveyor5Moving.enabled = false;
    }

    void MoveConveyorOne()
    {
        if (GameManager.Instance.mConveyor1Moving.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !GameManager.Instance.mConveyor1Moving.IsInTransition(0))
        {
            GameManager.Instance.mConveyor1Moving.enabled = true;
            GameManager.Instance.mConveyor1Moving.Play("ConveyorOneMove");
        }
    }
    void MoveConveyorTwo()
    {
        if (GameManager.Instance.mConveyor2Moving.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !GameManager.Instance.mConveyor2Moving.IsInTransition(0))
        {
            GameManager.Instance.mConveyor2Moving.enabled = true;
            GameManager.Instance.mConveyor2Moving.Play("ConveyorTwoMove");
        }
    }
    void MoveConveyorThree()
    {
        if (GameManager.Instance.mConveyor3Moving.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !GameManager.Instance.mConveyor3Moving.IsInTransition(0))
        {
            GameManager.Instance.mConveyor3Moving.enabled = true;
            GameManager.Instance.mConveyor3Moving.Play("ConveyorThreeMove");
        }
    }
    void MoveConveyorFour()
    {
        if (GameManager.Instance.mConveyor4Moving.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !GameManager.Instance.mConveyor4Moving.IsInTransition(0))
        {
            GameManager.Instance.mConveyor4Moving.enabled = true;
            GameManager.Instance.mConveyor4Moving.Play("ConveyorFourMove");
        }
    }
    void MoveConveyorFive()
    {
        if (GameManager.Instance.mConveyor5Moving.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !GameManager.Instance.mConveyor5Moving.IsInTransition(0))
        {
            GameManager.Instance.mConveyor5Moving.enabled = true;
            GameManager.Instance.mConveyor5Moving.Play("ConveyorFiveMove");
        }
    }
    #endregion
    /////////////////////////

    #region Buff Card
    public void GetBuffCardFunction() // returns the buff card's function
    {
        if (mIsBuff)
        {
            switch (mBuffCardIndex)
            {
                case 0:
                    BuffCard0TowerFix();
                    break;

                case 1:
                    BuffCard1TowerEnhance();
                    break;

                case 2:
                    BuffCard2NewHand();
                    break;

                case 3:
                    BuffCard3BaseFixer();
                    break;

                case 4:
                    BuffCard4ConveyorSpeedUp();
                    break;

                case 5:
                    BuffCard5SlowEnemies();
                    break;
                case 6:
                    SpawnSun();
                    break;
                case 7:
                    SpawnRain();
                    break;
                case 8:
                    SpawnLightning();
                    break;
                case 9:
                    SpawnSnow();
                    break;

            }
        }
    }

    void BuffCard0TowerFix()  // Index 1: Heals all towers
    {
        foreach (var towers in GameManager.Instance.activeTowerInGame)
        {
            BasicTowerScript towerScript = towers.GetComponent<BasicTowerScript>();
            if (towerScript.mHP < towerScript.mHPMaster)
                towerScript.mHP++;
        }
    }

    void BuffCard1TowerEnhance()
    {
        if (!isEnhanced)
        {
            foreach (var towers in GameManager.Instance.activeTowerInGame)
            {
                if (!towers.GetComponent<BasicTowerScript>().isSupportTower)
                {
                    BasicTowerScript towerScript = towers.GetComponent<BasicTowerScript>();
                    towerScript.mFireRate -= .2f;
                    towerScript.mDamage += 4;
                }
            }
            isEnhanced = true;
        }

    }

    void BuffCard2NewHand()
    {
        for (int i = 1; i < 6; i++)
        {
            GameManager.Instance.SpotEmpty(i);
        }
        GameManager.Instance.FourRandomCards();
    }
    void BuffCard3BaseFixer()
    {
        GameManager.Instance.baseHealthCurrent += 10;
        if (GameManager.Instance.baseHealthCurrent > 100)
            GameManager.Instance.baseHealthCurrent = 100;
        GameManager.Instance.UpdateHudVisuals();
    }

    void BuffCard4ConveyorSpeedUp()
    {
        GameManager.Instance.SetConveyorSpeed(GameManager.Instance.conveyorSpeedMaster + .5f, GameManager.Instance.cardSpeedMaster + .5f);
    }

    void BuffCard5SlowEnemies()
    {
        foreach (var enemy in GameManager.Instance.activeEnemiesInGame)
        {
            enemy.GetComponent<BasicEnemyScript>().Slow();
        }
    }
    public void SpawnSun()
    {
        GameManager.Instance.currentWeatherScript.OverrideWeather(0, GameManager.Instance.sunnySprite, "SunnyWeather");
    }
    public void SpawnSnow()
    {
        GameManager.Instance.currentWeatherScript.OverrideWeather(5, GameManager.Instance.snowSprite, "SnowWeather");
    }
    public void SpawnRain()
    {
        GameManager.Instance.currentWeatherScript.OverrideWeather(1, GameManager.Instance.rainSprite, "RainWeather");
    }
    public void SpawnLightning()
    {
        GameManager.Instance.currentWeatherScript.OverrideWeather(3, GameManager.Instance.lightningSprite, "LightningWeather");
    }
    #endregion

    #region Trap Card

    public void GetTrapCardFunction() // returns the buff card's function
    {
        if (mIsTrap)
        {
            switch (mTrapCardIndex)
            {
                case 0:
                    // put the function of the first trap card here
                    // make sure the first trap card has the index of 0
                    HasteTrapCard();
                    break;
                case 1:
                    AmbushTrapCard();
                    break;
                case 2:
                    SlowedConveyorTrapCard();
                    break;
                case 3:
                    DamageTowerTrapCard();
                    break;
                case 4:
                    SpawnIceStorm();
                    break;
                case 5:
                    SpawnWindStorm();
                    break;

            }
        }
    }
    public void HasteTrapCard()
    {
        foreach (var enemy in GameManager.Instance.activeEnemiesInGame)
        {
            enemy.GetComponent<BasicEnemyScript>().Haste();
        }
    }
    public void AmbushTrapCard()
    {
        GameManager.Instance.ambushSpawner.GetComponent<AmbushSpawn>().Spawn();
    }
    public void SlowedConveyorTrapCard()
    {
        GameManager.Instance.SetConveyorSpeed(GameManager.Instance.conveyorSpeedMaster - .5f, GameManager.Instance.cardSpeedMaster - .5f);
    }
    public void DamageTowerTrapCard()
    {
        foreach (var tower in GameManager.Instance.activeTowerInGame)
        {
            if (tower != null && tower.GetComponent<BasicTowerScript>() != null)
                tower.GetComponent<BasicTowerScript>().damageTower = true;
        }

    }
    public void SpawnIceStorm()
    {
        GameManager.Instance.currentWeatherScript.OverrideWeather(4, GameManager.Instance.iceStormSprite, "IceStormWeather");
    }
    public void SpawnWindStorm()
    {
        GameManager.Instance.currentWeatherScript.OverrideWeather(2, GameManager.Instance.windSprite, "WindStormWeather");
    }
    #endregion


}
