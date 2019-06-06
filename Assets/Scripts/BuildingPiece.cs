using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPiece : MonoBehaviour
{
    public BuildingPieceProfile profile;
    private BuildingController _buildingController;

    public bool IsDestroyed { get; private set; } = false;

    public bool IsTargetPiece
    {
        get { return profile.isTargetPiece; }
    }

    private void Awake()
    {
        _buildingController = GetComponentInParent<BuildingController>();
        
        if (profile.isTargetPiece)
        {
            var meshRen = GetComponentInChildren<SkinnedMeshRenderer>();
            meshRen.material.SetColor("_BaseColor", profile.aliveColor);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var explosionController = other.GetComponent<ExplosionController>();
        if (explosionController)
        {
            _buildingController.IsTouched = true;
            Vector3 force = transform.position - other.transform.position;
            force = force.normalized * explosionController.GetForce() * profile.forceFactor;
            GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (!IsTargetPiece)
            return;
        
        if (other.relativeVelocity.magnitude > profile.destroySpeed)
        {
            Die();
        }
    }

    private void Die()
    {
        IsDestroyed = true;
        var meshRen = GetComponentInChildren<SkinnedMeshRenderer>();
        meshRen.material.SetColor("_BaseColor", profile.deadColor);
        //gameObject.SetActive(false);
    }
}
