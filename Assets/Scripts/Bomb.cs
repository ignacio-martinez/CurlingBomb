using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float stopSpeed = .2f;
    public float timer = 2;
    public ExplosionController explosionController;
    public GameObject bombVisual;
    public GameObject explosionEffect;
    
    private bool _isLaunched = false;
    private bool _isExploding = false;
    private Rigidbody _rigidbody;
    private Animator _anim;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_isLaunched)
        {
            if (!_isExploding && _rigidbody.velocity.magnitude < stopSpeed)
            {
                StartExplosion();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartExplosion();
        }        
    }

    private void StartExplosion()
    {
        _rigidbody.velocity = Vector3.zero;
        _isExploding = true;
        _anim.SetTrigger("countdown");
    }

    public void SetLaunched()
    {
        _isLaunched = true;
        GameManager.Instance.abandonButton.SetActive(true);
    }

    public void Explode()
    {
        bombVisual.SetActive(false);
        Instantiate(explosionEffect, bombVisual.transform.position, Quaternion.identity);
        explosionController.Explode();
        GameManager.Instance.BombHasExploded();
    }
}
