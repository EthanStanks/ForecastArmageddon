using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyScript : MonoBehaviour
{
    [SerializeField] GameObject Projectile;
    [SerializeField] float delay;
    public NavMeshAgent agent;
    public int mDamage;
    public int attackDistance;
    public float mAttackDelay;
    public float mHealth;
    public float mRemainingDistance;
    public GameObject mTarget;
    public Vector3 mBulletLocation;
    public float mBulletDeathDelay;
    public float mBulletSpeed;
    public int mEnemyValue; // used for giving the player a score
    public bool mCanFire = true;
    public bool mIsHit;
    private float timer;
    public bool isTransportBoss;
    [SerializeField] int EnemiesToSpawn;
    [SerializeField] bool isFlammer;
    [SerializeField] bool isSummoner;
    [SerializeField] bool isBuffer;
    [SerializeField] bool isBasicEnemy;
    [SerializeField] bool isSuicideBomber;
    AudioSource m_MyAudioSource;
    public AudioClip shoot;
    public AudioClip hit;
    public AudioClip dies;
    public AudioClip spawns;
    public AudioClip touchBase;
    public float mEnemySpeed;
    public float mEnemySpeedMaster;
    public bool mIsEffectedBySnow;
    public bool mIsDamaged;
    public List<GameObject> enemies;
    public Vector3 mPosition;
    private bool Spawned;
    [SerializeField] float timeBetweenSpawn;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        //makes the man walk
        agent.destination = GameManager.Instance.baseObj.transform.position;
        m_MyAudioSource = GetComponent<AudioSource>();
        mEnemySpeed = agent.speed;
        mEnemySpeedMaster = mEnemySpeed;
    }

    // Update is called once per frame
    //checks if the enemy should be dead 
    void Update()
    {
        mPosition.Set(transform.position.x, transform.position.y, transform.position.z);
        if (isBasicEnemy == true)
        {
            if (!mCanFire)
            {
                StartCoroutine(Waiter(mAttackDelay));
                mCanFire = true;
            }
        }
        else if (isSummoner == true)
        {
            timer += Time.deltaTime;
            if (timer >= 3)
            {
                SpawnEnemies();
                timer = 0;

            }
        }
    }
    public void SpawnEnemies()
    {
        GameManager.Instance.enemyCount++;
        Instantiate(GameManager.Instance.basicEnemyPrefab, transform.position, transform.rotation);
    }
    public void EnemyDamaged(float dmg, bool diedFromBase)
    {
        if (this != null)
        {
            mIsDamaged = true;
            mHealth -= dmg;
            var selectionRenderer = GetComponent<Renderer>();

            StartCoroutine(DamageEffectSequence(selectionRenderer, Color.red, 0, (float)0.1));

            if (mHealth <= 0)
            {

                if (!diedFromBase)
                    m_MyAudioSource.PlayOneShot(dies);

                Died(diedFromBase);
            }
            mIsDamaged = false;
        }
    }
    IEnumerator Spawner(float wait)
    {
        yield return new WaitForSeconds(wait);
        GameManager.Instance.enemyCount++;
        GameObject enemy = Instantiate(GameManager.Instance.basicEnemyPrefab, transform.position, transform.rotation);
        GameManager.Instance.activeEnemiesInGame.Add(enemy);
    }
    public void Died(bool diedFromBase) // called when it dies
    {
        if (this != null)
        {
            m_MyAudioSource.PlayOneShot(touchBase);
            GameObject deatho = null;
            if (Application.platform != RuntimePlatform.WebGLPlayer)
                deatho = Instantiate(GameManager.Instance.DeathExplosion, mPosition, transform.rotation);

            foreach (var tower in GameManager.Instance.activeTowerInGame)
            {
                if (tower.GetComponentInChildren<TowerTrigger>().mPriorityTargetList.Contains(gameObject) && tower != null)
                    tower.GetComponentInChildren<TowerTrigger>().mPriorityTargetList.Remove(gameObject);
            }
            if (isTransportBoss == true && !diedFromBase)
            {
                float temp = 0;
                for (int i = 0; i < EnemiesToSpawn; i++)
                {
                    Spawned = false;
                    if (!Spawned)
                    {
                        float Displacer = UnityEngine.Random.Range(-4f, 4f);
                        if (temp == Displacer)
                        {
                            Displacer--;
                        }
                        temp = Displacer;
                        mPosition.x -= Displacer;
                        StartCoroutine(Waiter(mAttackDelay));
                        GameManager.Instance.enemyCount++;
                        GameObject enemy = Instantiate(GameManager.Instance.basicEnemyPrefab, mPosition, transform.rotation);
                        GameManager.Instance.activeEnemiesInGame.Add(enemy);
                        Spawned = true;
                    }
                }
            }
            GameManager.Instance.activeEnemiesInGame.Remove(gameObject);
            GameManager.Instance.enemyCount--;

            if (!GameManager.Instance.isSpawnAgain && GameManager.Instance.enemyCount == 0 && !GameManager.Instance.isGameOver)
            {
                GameManager.Instance.isSpawnAgain = true;
                GameManager.Instance.UpdateHudVisuals();
            }
            GameManager.Instance.LastRound(); // called to see if the player won or lost if this dies in the 15th round

            if (!diedFromBase)
                GameManager.Instance.playerScore += mEnemyValue;

            if (Application.platform != RuntimePlatform.WebGLPlayer)
                Destroy(deatho, 3);
            Destroy(gameObject);
        }
    }

    public IEnumerator DamageEffectSequence(Renderer sr, Color dmgColor, float duration, float delay)
    {
        // save origin color
        Color originColor = sr.material.color;

        // tint the sprite with damage color
        sr.material.color = dmgColor;
        // you can delay the animation
        yield return new WaitForSeconds(delay);

        // lerp animation with given duration in seconds
        for (float t = 0; t < 1.0f; t += Time.deltaTime / duration)
        {
            sr.material.color = Color.Lerp(dmgColor, originColor, t);

            yield return null;
        }

        // restore origin color
        sr.material.color = originColor;
    }
    //thing that should make the stuff wait but doesn't
    IEnumerator Waiter(float delay)
    {
        yield return new WaitForSeconds(delay);
    }

    //bullet stuff <3
    public void FireBullet()
    {
        m_MyAudioSource.PlayOneShot(shoot);
        GameObject basic_enemy_bullet = Instantiate(Projectile, transform.position, transform.rotation); // cloning bullet object setting to position & rotation of the current enemy
        mBulletLocation = (mTarget.transform.position - transform.position); //setting destination to current position to target position -1 and "normalizes"
        basic_enemy_bullet.GetComponent<Rigidbody>().velocity = mBulletLocation * mBulletSpeed; //velocity setter
        if (isSuicideBomber)
        {
            Destroy(gameObject, 0);
        }
        Destroy(basic_enemy_bullet, 2); //destorys after 2 secs
    }
    public void Buff()
    {
        if (isBuffer)
        {

        }
        else
        {
            mHealth += 3;
        }
    }
    //if (other.gameObject.CompareTag("Enemy"))
    //{
    //    GetComponent<BasicEnemyScript>().mHealth += 3;

    //}

    //if (isFlammer)
    //{
    //    if (other.CompareTag("Tower"))
    //    {
    //        timer = 0;
    //        timer += Time.deltaTime;
    //        if (timer >= mAttackDelay)
    //        {
    //            GetComponent<BasicTowerScript>().TowerDamaged(mDamage);
    //        }
    //    }

    public void SnowEffect()
    {
        mEnemySpeed = mEnemySpeed - (mEnemySpeed * 0.25f);
        agent.speed = mEnemySpeed;
        mIsEffectedBySnow = true;
    }
    public void RevertSnowEffect()
    {
        mEnemySpeed = mEnemySpeedMaster;
        agent.speed = mEnemySpeedMaster;
        mIsEffectedBySnow = false;
    }

    public void LightningEffect()
    {
        EnemyDamaged(5, false);
    }

    public void IceStormEffect()
    {
        EnemyDamaged(2, false);


    }
    public void Haste()
    {
        mEnemySpeed++;
        agent.speed = mEnemySpeed;
    }
    public void Slow()
    {
        if (mEnemySpeed > 1)
        {
            mEnemySpeed--;
            agent.speed = mEnemySpeed;
        }
    }
}
