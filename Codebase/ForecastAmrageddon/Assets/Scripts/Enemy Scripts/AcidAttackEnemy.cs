using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AcidAttackEnemy : MonoBehaviour
{
    [SerializeField] GameObject Projectile;
    public NavMeshAgent agent;
    public int mDamage = 1;
    public int attackDistance = 1;
    public float mAttackDelay;
    public int mHealth;
    public float mRemainingDistance;
    public GameObject mTarget;
    public Vector3 mBulletLocation;
    public float mBulletDeathDelay;
    public float mBulletSpeed;
    public int mEnemyValue; // used for giving the player a score
    public bool mCanFire = true;
    public bool mIsHit;


    // Start is called before the first frame update
    void Start()
    {
        //makes the man walk
        agent.destination = GameManager.Instance.baseObj.transform.position;
    }

    // Update is called once per frame
    //checks if the enemy should be dead
    void Update()
    {
        if (!mCanFire)
        {
            StartCoroutine(Waiter(mAttackDelay));
            mCanFire = true;
        }


    }

    public void EnemyDamaged(int dmg, bool diedFromBase)
    {
        if (!mIsHit)
        {
            mIsHit = true;
            mHealth -= dmg;
            var selectionRenderer = GetComponent<Renderer>();

            if (selectionRenderer.GetComponent<Color>() != Color.red)
                StartCoroutine(DamageEffectSequence(selectionRenderer, Color.red, 0, (float)0.1));

            if (mHealth <= 0)
                Died(diedFromBase);
            mIsHit = false;
        }
    }
    public void Died(bool diedFromBase) // called when it dies
    {
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
        Destroy(gameObject);
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
        GameObject basic_enemy_bullet = Instantiate(Projectile, transform.position, transform.rotation); // cloning bullet object setting to position & rotation of the current enemy
        mBulletLocation = (mTarget.transform.position - transform.position); //setting destination to current position to target position -1 and "normalizes"
        basic_enemy_bullet.GetComponent<Rigidbody>().velocity = mBulletLocation * mBulletSpeed; //velocity setter
        Destroy(basic_enemy_bullet, 2); //destorys after 2 secs
    }

}
