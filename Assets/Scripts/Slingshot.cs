using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    public Transform arrowBody;
    public Transform arrowTip;
    public GameObject fire;

    public float minLength = 1f;
    public float maxLength = 10f;

    public float minSpeed = 1f;
    public float maxSpeed = 10f;

    private Bomb _bomb;
    private Camera _cam;
    private Rigidbody _rb;
    private bool _isAiming = false;
    private bool _hasShot = false;
    private Vector3 _tapPos;
    private Vector3 _currentTapPos;
    private Vector3 _direction;
    private float _currentLength;
    private float _lerpFactor;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _cam = Camera.main;
        _bomb = GetComponent<Bomb>();

        arrowBody.gameObject.SetActive(false);
        arrowTip.gameObject.SetActive(false);
        fire.SetActive(false);
    }

    private void Update()
    {
        if (GameManager.Instance.GetState() != GameManager.State.Game) return;
        if (_hasShot) return;

        arrowBody.gameObject.SetActive(_currentLength > minLength);
        arrowTip.gameObject.SetActive(_currentLength > minLength);

        if (Input.GetMouseButtonDown(0))
        {
            _tapPos = _cam.ScreenToWorldPoint(Input.mousePosition);
            _isAiming = true;
        }

        if (Input.GetMouseButton(0) && _isAiming)
        {
            Vector3 _currentTapPos = _cam.ScreenToWorldPoint(Input.mousePosition);
            _direction = _currentTapPos - _tapPos;

            _currentLength = _direction.magnitude;
            if (_currentLength > minLength)
            {
                _lerpFactor = (_currentLength - minLength) / (maxLength - minLength);
                _lerpFactor = Mathf.Min(_lerpFactor, 1f);

                Vector3 projectedDirection = Vector3.ProjectOnPlane(-_direction, Vector3.up);

                arrowBody.localScale = new Vector3(arrowBody.localScale.x, Mathf.Lerp(minSpeed, maxSpeed, _lerpFactor) * 0.1f, arrowBody.localScale.z);
                arrowBody.up = projectedDirection.normalized;
                arrowTip.up = projectedDirection.normalized;
                arrowTip.position = transform.position + projectedDirection.normalized * Mathf.Lerp(minSpeed, maxSpeed, _lerpFactor) * 0.1f;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_currentLength < minLength)
            {
                _isAiming = false;
            }
            else
            {
                _rb.velocity = -_direction.normalized * Mathf.Lerp(minSpeed, maxSpeed, _lerpFactor);
                _hasShot = true;
                arrowBody.gameObject.SetActive(false);
                arrowTip.gameObject.SetActive(false);
                fire.SetActive(true);
                _bomb.SetLaunched();
            }

        }
    }

    private void OnDrawGizmos()
    {
        if (!_isAiming || _hasShot) return;

        if (_currentLength > maxLength)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + _direction);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + _direction.normalized * maxLength);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + _direction.normalized * minLength);
        }
        else if (_currentLength <= maxLength && _currentLength > minLength)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + _direction);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + _direction.normalized * minLength);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + _direction);
        }
    }


}
