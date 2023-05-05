using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    public float Intensity = 1f;

    Transform _target;
    Vector3 _initialPos;
    float _pendingShakeDuration = 0f;
    bool _isShaking = false;

    // Start is called before the first frame update
    void Start()
    {
        _target = GetComponent<Transform>();
        _initialPos = _target.localPosition;
    }

    public void Shake(float duration)
    {
        if (duration > 0)
        {
            _pendingShakeDuration += duration;
        }
    }

    private void Update()
    {
        if (_pendingShakeDuration > 0 && !_isShaking)
        {
            StartCoroutine(DoShake());
        }
    }

    IEnumerator DoShake()
    {
        _isShaking = true;
        var startTime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < startTime + _pendingShakeDuration)
        {
            var randomPoint = new Vector3(
                UnityEngine.Random.Range(_target.position.x - Intensity,_target.position.x + Intensity),
                UnityEngine.Random.Range(_target.position.y - Intensity, _target.position.y + Intensity),
                _initialPos.z);
            _target.localPosition = randomPoint;
            yield return null;
        }
        _pendingShakeDuration = 0f;
        _target.localPosition = _initialPos;
        _isShaking = false;
    }
}
