using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    bool GameIsGo;
    void TutorialEnemy()
    {
        Instantiate(GameManager.Instance.tutorialEnemyPrefab, transform.position, transform.rotation);

    }
    // Start is called before the first frame update
    void Start()
    {
        GameIsGo = true;
        StartCoroutine("SpawnTutorialEnemy");

    }
    IEnumerator SpawnTutorialEnemy()
    {
        while (GameIsGo)
        {
            yield return new WaitForSeconds(4);
            TutorialEnemy();
        }
    }
}
