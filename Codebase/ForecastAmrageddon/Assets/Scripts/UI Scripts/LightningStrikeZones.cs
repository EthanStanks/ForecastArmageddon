using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrikeZones : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            GameManager.Instance.lightningStrikedEnemies.Add(other.gameObject);
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            StartCoroutine(RemoveEnemyFromList(0.3f, other.gameObject));
    }

    IEnumerator RemoveEnemyFromList(float wait, GameObject enemy)
    {
        yield return new WaitForSeconds(wait);
        GameManager.Instance.lightningStrikedEnemies.Remove(enemy);
    }
}
