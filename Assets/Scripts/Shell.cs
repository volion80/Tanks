using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public float speed = 10.0f;
    
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
        if (enemy != null) {
            enemy.ReactToHit();
        }
        Destroy(this.gameObject);
    }
}
