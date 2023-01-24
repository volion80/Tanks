using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Transform _target;

    public float rotSpeed = 1.5f;
    private float _rotY;
    private Vector3 _offset;
    
    void Start()
    {
        _rotY = transform.eulerAngles.y;
        _offset = target.position - transform.position;
    }
    
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                _target = player.transform;
        }
        else
            _target = target;
        
        if (_target == null)
            return;

        _rotY += Input.GetAxis("Horizontal") * rotSpeed;

        Quaternion rotation = Quaternion.Euler(0, _rotY, 0);
        transform.position = _target.position - (rotation * _offset);
        transform.LookAt(_target);
    }
}
