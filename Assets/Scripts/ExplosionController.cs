using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public float powerIncrease = 2;
    public float radius = 5;
    public float speed = 10;
    
    private int _power = 0;
    private float _targetScale;
    private float _currentForce = 1;

    private void Update()
    {
        if (transform.localScale.x < _targetScale)
        {
            transform.localScale += new Vector3(speed, speed, speed) * Time.deltaTime;
            _currentForce = 1 - (transform.localScale.x / _targetScale);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public float GetForce()
    {
        return _currentForce;
    }
    
    public void Explode()
    {
        transform.localScale = Vector3.zero;
        _targetScale = radius + (_power * powerIncrease);
        gameObject.SetActive(true);
    }
}
