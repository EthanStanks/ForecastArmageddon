using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeShake : MonoBehaviour
{
    public Shaker Shaker;
    public float duration = 1f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shaker.Shake(duration);
        }
    }
}
