using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject shellPrefab;
    private GameObject _shell;

    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_shell == null)
            {
                _shell = Instantiate(shellPrefab) as GameObject;
                _shell.transform.position = transform.TransformPoint(0,1.5f,3);
                _shell.transform.rotation = transform.rotation;
            }
        }
    }
}
