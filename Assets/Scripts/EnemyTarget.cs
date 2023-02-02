using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
    
    public void ReactToHit() {
        EnemyAI behavior = GetComponent<EnemyAI>();
        if (behavior != null)
        {
            behavior.SetAlive(false);
        }
        Managers.Enemy.UpdateKilled();
        
        Destroy(this.gameObject);
    }
}
