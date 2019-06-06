using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shooter : MonoBehaviour
{
    public float deathZone;

    [Range(0, 500)]
    public int power;
    [Range(0, 5)]
    public float maxPower = 3;
    [Range(0, 3)]
    public float minPower = 1;
    public ExplosionController explosion;
    [HideInInspector]
    public GameObject catchObject;

    void Update()
    {
        TouchControl();

        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public float force;
    void TouchControl()
    {
        Vector3 slingShotPos = transform.position;
        float zFromCamera = (Camera.main.transform.position - transform.position).magnitude;

        // put
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = GetMousePosition(zFromCamera);
            pos.y = transform.position.y;
            float distance = (pos - transform.position).magnitude;
            
            if(distance < deathZone)
            {
                catchObject = gameObject;
                //GetComponent<TraceCatchObject>().enabled = true;
            }
        }

        // move
        if (catchObject != null && Input.GetMouseButton(0))
        {
            Vector3 pos = GetMousePosition(zFromCamera);
            Vector3 diff = pos - slingShotPos;
            //catchObject.transform.position = Vector3.ClampMagnitude(diff, maxPower) + slingShotPos;
            force = diff.magnitude;
            
            Debug.DrawLine(transform.position, transform.position + diff);
        }

        // release
        if (catchObject != null && Input.GetMouseButtonUp(0))
        {
            Vector3 pos = GetMousePosition(zFromCamera);
            
            Vector3 diff = slingShotPos - pos;
            //diff.y = 0;

            if (diff.magnitude > minPower)
            {
                catchObject.GetComponent<Rigidbody>().isKinematic = false;
                catchObject.GetComponent<Rigidbody>().AddForce(diff * power);
            }

            //GetComponent<TraceCatchObject>().enabled = false;
            catchObject = null;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            explosion.gameObject.SetActive(true);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, maxPower);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minPower);
        Gizmos.color = Color.white;
    }

    Vector3 GetMousePosition(float z)
    {
        var mousePos = Input.mousePosition;
        return Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, z));
    }
}

