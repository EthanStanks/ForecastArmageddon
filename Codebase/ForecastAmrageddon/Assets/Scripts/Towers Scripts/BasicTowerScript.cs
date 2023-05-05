using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTowerScript : MonoBehaviour
{
    // The purpose of this Script is to be the main hub for all Tower Functions

    // Credits:  Gabriel Belchie, Casey Vue, Ethan Stanks (Weather Effects)

    // Tower's data
    public int mHP, mCardSlot, mHPMaster; // mHPMaster is for knowing what the original hp was on the tower
    public float mFireRate, mDamage, mDamageMaster; // mDamageMaster is for knowing what the original damage was in case a weather event changes it and needs to go back after a certain amount of time
    public float mAttackCooldown;
    public float mReloadSpeed;
    public string mName, mDesc;
    public float mAttackRange, mAttackRangeMaster;
    public Renderer mTowerRender;
    public Color mDamagedTowerColor = Color.red;
    public Color mTowerColorHolder;
    public bool damageTower;
    public GameObject mTarget;

    public GameObject mSpotRef;

    public GameObject TriggerVisual;

    [Header("Gun")]

    //works with mFireFate;
    public float mBulletSpeed;

    // Tower's Bullet/Pellet placement
    public int mPelletAmount;
    public GameObject mBullet;
    public GameObject mPellets;
    public GameObject[] mShotgunSpread;
    public List<Quaternion> mReadiedPellets;
    public float mRandomizedPelletAngle;
    public Vector3 mBulletLocation;
    public Vector3 mPelletLocation;

    // Support Tower data
    public string mEffectDesc;
    public float mEffectRange;
    public List<GameObject> mSupportTargets;

    // Various Tower Checks
    public bool isSupportTower;
    public bool isShotgunTower;
    public bool mIsEffectedBySun; // used to see if the sunny weather already buffed this tower
    public bool mIsEffectedByWind;
    public bool isGoingToGetIced;

    //Tower Sound FX
    AudioSource m_MyAudioSource;
    public AudioClip place;
    public AudioClip shoot;
    public AudioClip hit;
    public AudioClip die;

    private void Start()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
        m_MyAudioSource.PlayOneShot(place);
        mTowerRender = GetComponent<Renderer>();
        mTowerColorHolder = mTowerRender.material.color;
        TriggerVisual = gameObject.transform.GetChild(1).gameObject;
        mAttackRange = GetComponentInChildren<SphereCollider>().radius;
        mAttackRangeMaster = mAttackRange;
        TriggerVisual.transform.localScale = new Vector3(mAttackRange * 2, mAttackRange * 2, mAttackRange * 2);

        if(isShotgunTower)
        {
            ReadyShotgun();
        }

        if (isSupportTower)
        {
            SetSupportTowerStats();
        }

        if (isSupportTower)
        {
            InvokeRepeating(nameof(SupportEffect), 0f, 1.5f);
        }

        InvokeRepeating(nameof(UpdateHeading), 0f, .2f); // Updates the heading of a bullet every few milliseconds to minimize tower bullets missing

    }
    private void Update()
    {
        if(!isShotgunTower)
        {
            if (mAttackCooldown <= 0f)
            {
                FireBasicBullet();
                mAttackCooldown = mFireRate / mReloadSpeed;
            }
        }
        
        if(isShotgunTower)
        {
            if (mAttackCooldown <= 0f)
            {
                FireShotgun();
                mAttackCooldown = mFireRate / mReloadSpeed;
            }
        }
        

        mAttackCooldown -= Time.deltaTime;

        if (damageTower)
        {
            TowerDamaged(3);
            damageTower = false;
        }
    }


    public void FireBasicBullet()  // Where the Tower's Bullets are fired
    {
        if (!isSupportTower)
        {
            if (mTarget != null)
            {

                mBullet = Instantiate(GameManager.Instance.basicTowerBulletPrefab, transform.position, transform.rotation);
                mBullet.GetComponent<TowerBulletScript>().mBulletDamage = mDamage;
                mBulletLocation = (mTarget.transform.position - transform.position).normalized; //setting destination to current position and "normalizes" it
                m_MyAudioSource.PlayOneShot(shoot);
                Quaternion look = Quaternion.LookRotation(mBulletLocation);
                Vector3 rotate = Quaternion.Lerp(gameObject.transform.rotation, look, Time.deltaTime * 200f).eulerAngles;
                gameObject.transform.rotation = look;// Quaternion.Euler(0f, rotate.y, 0f);
                
                

                mBullet.GetComponent<Rigidbody>().velocity = mBulletLocation * mBulletSpeed * 2; //velocity setter
            }
            Destroy(mBullet, .5f);
        }

    }
    public void ReadyShotgun()
    {
        mReadiedPellets = new List<Quaternion>(mPelletAmount);
        for (int i = 0; i < mPelletAmount; i++)
        {
            mReadiedPellets.Add(Quaternion.Euler(Vector3.zero));
        }
    }

    public void FireShotgun()
    {
        if (!isSupportTower)
        {
            if (mTarget != null)
            {
                mShotgunSpread = new GameObject[5];
                for (int pelletsCreated = 0; pelletsCreated < mShotgunSpread.Length; pelletsCreated++)
                {
                    mRandomizedPelletAngle = (float)Random.Range(-1f, 1f);
                    mReadiedPellets[pelletsCreated] = Random.rotation;
                    mPellets = Instantiate(GameManager.Instance.pelletShotgunBulletPrefab, transform.position, transform.rotation);
                    mPellets.GetComponent<TowerBulletScript>().mBulletDamage = mDamage;
                    mPellets.transform.rotation = Quaternion.RotateTowards(mPellets.transform.rotation, mReadiedPellets[pelletsCreated], (mTarget.transform.position.x) - mRandomizedPelletAngle);
                    mPelletLocation = (mTarget.transform.position - mPellets.transform.position);
                    mPellets.GetComponent<Rigidbody>().velocity = mPelletLocation * mBulletSpeed * 2;
                    
                    mShotgunSpread[pelletsCreated] = mPellets;
                    Quaternion look = Quaternion.LookRotation(mPelletLocation);
                    Vector3 rotate = Quaternion.Lerp(gameObject.transform.rotation, look, Time.deltaTime * 200f).eulerAngles;
                    gameObject.transform.rotation = look;// Quaternion.Euler(0f, rotate.y, 0f);
                }
            }
        }

        m_MyAudioSource.PlayOneShot(shoot);
        for (int i = 0; i < mShotgunSpread.Length; i++)
        {
            Destroy(mShotgunSpread[i], .20f);
        }
    }

    public void UpdateHeading() // Forces the Bullet's heading to update
    {
        if (mTarget != null)
        {
            if (mBulletLocation != mTarget.transform.position)
            {
                mBulletLocation = (mTarget.transform.position - transform.position).normalized;
                float mDistanceFromTarget = mBulletSpeed * Time.deltaTime;

                if (mBulletLocation.magnitude <= mDistanceFromTarget)
                {
                    Destroy(mBullet, .5f);
                }
            }
        }
    }


    void TowerDeath() // When the tower dies
    {
        m_MyAudioSource.PlayOneShot(die);
        mSpotRef.tag = "TowerSpot";
        if (isSupportTower == true)
        {
            int index = 0;

            foreach (GameObject tower in mSupportTargets)
            {
                mSupportTargets[index].GetComponent<BasicTowerScript>().mReloadSpeed += .1f; // Reverts Support Tower Buffs before death
                index++;
            }
        }
        if (isGoingToGetIced)
            GameManager.Instance.iceStrikeObjs.Remove(gameObject);
        GameManager.Instance.activeTowerInGame.Remove(gameObject); // removes the tower from the list of current towers in the game
        Destroy(gameObject);
    }


    public void TowerDamaged(int dmg) // Whenever the tower gets damaged
    {
        m_MyAudioSource.PlayOneShot(hit);
        mHP -= dmg;

        //for (float t = 0; t < 1.0f; t += Time.deltaTime / 10)
        //{
        //    DamagedTowerEffect();
        //}

        //ChangeRenderBack(mTowerColorHolder);

        var selectionRenderer = GetComponent<Renderer>();
        Color originalColor = selectionRenderer.material.color;

        StartCoroutine(DamageEffectSequence(selectionRenderer, Color.red, 0, .1f));
        StartCoroutine(ReturnTowerColor(selectionRenderer.material.color, originalColor));

        if (mHP <= 0)
            TowerDeath();
    }

    public void DamagedTowerEffect()
    {
        mTowerRender.material.color = mDamagedTowerColor;
        Debug.Log("Color is Red");
        return;
    }

    public void ChangeRenderBack(Color _moriginalColor)
    {
        mTowerRender.material.color = _moriginalColor;

        return;
    }

    public IEnumerator DamageEffectSequence(Renderer _mTowerRender, Color _mdmgColor, float _mduration, float _mdelay)  // Changes the color of the tower to simulate being damaged
    {
        // save origin color
        Color originColor = _mTowerRender.material.color;

        // tint the sprite with damage color
        _mTowerRender.material.color = _mdmgColor;
        // you can delay the animation
        yield return new WaitForSeconds(_mdelay);

        // lerp animation with given duration in seconds
        for (float t = 0; t < 1.0f; t += Time.deltaTime / _mduration)
        {
            _mTowerRender.material.color = /*_mdmgColor;*/Color.Lerp(_mdmgColor, originColor, t);

            yield return null;
        }


        _mTowerRender.material.color = originColor; //makes the restoration happen twice just in case.


        // restore origin color
        _mTowerRender.material.color = originColor;


    }

    public IEnumerator ReturnTowerColor(Color _mChangedRender, Color _moriginalColor/*, float _mduration, float _mdelay*/)  // Reverts the color of the Tower back to it's original color forcefully
    {
        _mChangedRender = _moriginalColor;

        yield return null;
    }

    public void HideTrigger(bool isShowTrigger) // Hide's the tower's trigger range from the player until they want to see it
    {
        if (TriggerVisual != null)
        {
            if (isShowTrigger)
                TriggerVisual.SetActive(true);
            else if (!isShowTrigger)
                TriggerVisual.SetActive(false);
        }
    }

    public void SunEffect() // Begins and reverts Sun effect on towers
    {
        mDamage += (mDamage * .10f);
    }
    public void RevertSunEffect()
    {
        mDamage = mDamageMaster;
    }
    public void RainEffect() // Begins Rain effect on towers
    {
        if (mHP < mHPMaster)
            mHP++;
    }
    public void WindEffect() // Begins and reverts Wind effect on tower
    {
        if (mAttackRange > 1)
        {
            mAttackRange--;
            GetComponentInChildren<SphereCollider>().radius = mAttackRange;
            TriggerVisual.transform.localScale = new Vector3(mAttackRange * 2, mAttackRange * 2, mAttackRange * 2);
        }
    }
    public void RevertWindEffect()
    {
        mAttackRange = mAttackRangeMaster;
        GetComponentInChildren<SphereCollider>().radius = mAttackRangeMaster;
        TriggerVisual.transform.localScale = new Vector3(mAttackRangeMaster * 2, mAttackRangeMaster * 2, mAttackRangeMaster * 2);
    }

    public void IceStormEffect() // Begins Hailstorm effect on tower
    {
        GameObject lightning = Instantiate(GameManager.Instance.towerLightning, transform.parent);
        Destroy(lightning, 2);
        lightning.transform.position = gameObject.transform.position;
        TowerDamaged(2);
    }

    public void SetSupportTowerStats() // Sets damage for Support Towers to 0 and converts their Attack Range to the range of their effects
    {
        mDamageMaster = 0;
        mEffectRange = mAttackRangeMaster;
    }

    public void SupportEffect() // Since there's only 1 Support tower variant currently, here is where their effects are
    {
        for (int i = 0; i < mSupportTargets.Count; i++)
        {
            if (mSupportTargets[i].GetComponent<BasicTowerScript>().mHP < mSupportTargets[i].GetComponent<BasicTowerScript>().mHPMaster) // Checks if the Allied tower is less than Max HP
            { mSupportTargets[i].GetComponent<BasicTowerScript>().mHP++; }

            if (mSupportTargets[i].GetComponent<BasicTowerScript>().mReloadSpeed < 5f)
            { mSupportTargets[i].GetComponent<BasicTowerScript>().mReloadSpeed += .05f; }
        }
    }

    
}
