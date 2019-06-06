using System;
using UnityEngine;
using UnityEngine.UI;

public class TargetPiece : MonoBehaviour
{
    private float destroySpeed;
        
    public bool IsDestroyed { get; private set; } = false;

    public void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.layer & 1 << 8) == 1)
        {
            IsDestroyed = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.relativeVelocity.magnitude > destroySpeed)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
