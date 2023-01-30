using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject shellPrefab;
    
    [SerializeField] private AudioClip shootSound;
    
    private GameObject _shell;
    
    private AudioSource _soundSource;

    void Start()
    {
        _soundSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_shell == null)
            {
                _soundSource.PlayOneShot(shootSound);
                _shell = Instantiate(shellPrefab) as GameObject;
                _shell.transform.position = transform.TransformPoint(0,1.5f,3);
                _shell.transform.rotation = transform.rotation;
            }
        }
    }
}
