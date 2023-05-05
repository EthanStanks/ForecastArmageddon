using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVisualScript : MonoBehaviour
{
    public GameObject mTowerVisual;
    public MeshRenderer mGLRender;

    private void Start()
    {
        mTowerVisual = gameObject;
        gameObject.SetActive(false);
    }

}
