using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status {get; private set;}
    
    public int killed {get; private set;}

    public static Action EnemyHit;
    public static Action LevelComplete;
    
    void Start()
    {
        
    }
    
    public void Startup() {
        Debug.Log("Enemy manager starting...");
        
        UpdateData(0);
        
        status = ManagerStatus.Started;
    }

    public void UpdateKilled() {
        killed ++;
        if (killed > Managers.Level.enemiesToKill) {
            killed = Managers.Level.enemiesToKill;
        }
        
        if (killed == Managers.Level.enemiesToKill)
        {
            LevelComplete?.Invoke();
        }
        
        EnemyHit?.Invoke();
    }
    
    public void Respawn()
    {
        UpdateData(0);
    }

    public void UpdateData(int killed)
    {
        this.killed = killed;
    }
}
