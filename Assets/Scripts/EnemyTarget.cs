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
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        transform.Rotate(-10, -50, 10);
        
        yield return new WaitForSeconds(1.0f);
        
        Destroy(this.gameObject);
        
        Managers.Enemy.UpdateKilled();
    }
        
}
