using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    private GameObject _shellExplosion;
    
    public float speed = 10.0f;

    public static Action<Vector3, bool> shellExplosion;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        EnemyTarget enemy = other.GetComponent<EnemyTarget>();
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (enemy != null) {
            Vector3 position = enemy.transform.TransformPoint(0,0.5f,0);
            shellExplosion?.Invoke(position, true);
            
            enemy.ReactToHit();
        }
        else if (player != null)
        {
            Vector3 position = player.transform.TransformPoint(0,0.5f,0);
            shellExplosion?.Invoke(position, true);
            
            player.Hurt();
        }
        else
        {
            Vector3 position = transform.TransformPoint(0,0,-1.0f);
            shellExplosion?.Invoke(position, false);
        }
        Destroy(this.gameObject);
    }
}
