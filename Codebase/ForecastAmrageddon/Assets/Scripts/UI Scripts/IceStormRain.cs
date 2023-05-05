using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceStormRain : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            GameManager.Instance.iceStrikeObjs.Add(other.gameObject);
        if (other.gameObject.CompareTag("Tower"))
            GameManager.Instance.iceStrikeObjs.Add(other.gameObject);
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            StartCoroutine(RemoveEnemyFromList(0.3f, other.gameObject));
    }

    IEnumerator RemoveEnemyFromList(float wait, GameObject enemy)
    {
        yield return new WaitForSeconds(wait);
        GameManager.Instance.iceStrikeObjs.Remove(enemy);
    }
}
