using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbushSpawn : MonoBehaviour
{
    #region Enemies
    void SpawnBasic()
    {
        GameObject enemy = Instantiate(GameManager.Instance.basicEnemyPrefab, transform.position, transform.rotation);
        enemy.name = "Ambush Basic Enemy";
        GameManager.Instance.enemyCount++;
        GameManager.Instance.activeEnemiesInGame.Add(enemy);
    }
    IEnumerator SpawnBasicEnemy(float _wait)
    {
        yield return new WaitForSeconds(_wait);
        SpawnBasic();

    }
    void SpawnTank()
    {
        GameObject enemy = Instantiate(GameManager.Instance.tankEnemyPrefab, transform.position, transform.rotation);
        enemy.name = "Ambush Tank Enemy";
        GameManager.Instance.enemyCount++;
        GameManager.Instance.activeEnemiesInGame.Add(enemy);
    }
    IEnumerator SpawnTankEnemy(float _wait)
    {
        yield return new WaitForSeconds(_wait);
        SpawnTank();

    }
    void SpawnFast()
    {
        GameObject enemy = Instantiate(GameManager.Instance.fastEnemyPrefab, transform.position, transform.rotation);
        enemy.name = "Ambush Fast Enemy";
        GameManager.Instance.enemyCount++;
        GameManager.Instance.activeEnemiesInGame.Add(enemy);
    }
    IEnumerator SpawnFastEnemy(float _wait)
    {
        yield return new WaitForSeconds(_wait);
        SpawnFast();

    }

    #endregion
  
    #region Spawn
    public void Spawn()//Chooses a number between 1 nad 3 and spawns the corresponding wave
    {
        int random = UnityEngine.Random.Range(1, 4);
        switch (random)
        {
            case 1:
                SpawnRound1();
                break;
            case 2:
                SpawnRound2();
                break;
            case 3:
                SpawnRound3();
                break;
        }
    }
    #endregion

    #region Rounds

    void SpawnRound1()
    {
        StartCoroutine(SpawnBasicEnemy(1));
        StartCoroutine(SpawnBasicEnemy(1.5f));
        StartCoroutine(SpawnBasicEnemy(2));
        StartCoroutine(SpawnBasicEnemy(2.5f));

    }
    void SpawnRound2()
    {
        SpawnRound1();
        StartCoroutine(SpawnTankEnemy(3f));
    }
    void SpawnRound3()
    {
        SpawnRound2();
        StartCoroutine(SpawnTankEnemy(3.5f));
    }

    #endregion
}
