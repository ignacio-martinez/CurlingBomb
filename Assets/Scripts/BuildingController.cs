using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    private List<Rigidbody> rigidbodies;
    private bool _isTouched = false;

    public bool IsTouched
    {
        get { return _isTouched; }

        set
        {
            if (!_isTouched && value == true)
            {
                Free();
            }

            _isTouched = value;
        }
    }
    
    void Awake()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>().ToList();
        Lock();
    }


    public void Free()
    {
        foreach (var rb in rigidbodies)
        {
            rb.isKinematic = false;
        }
    }
    
    public void Lock()
    {
        foreach (var rb in rigidbodies)
        {
            rb.isKinematic = true;
        }
    }
}
