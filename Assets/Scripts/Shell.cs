using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
