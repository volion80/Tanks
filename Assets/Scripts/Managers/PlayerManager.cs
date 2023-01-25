using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status {get; private set;}
    
    public int hurt {get; private set;}

    public static Action LivesUpdated;
    public static Action LevelFailed;
    public static Action PlayerLivesDecreased;
    
    public void Startup() {
        UpdateData(0);
        status = ManagerStatus.Started;
    }
    
    public void UpdateHurt()
    {
        hurt++;
        if (hurt > Managers.Level.playersLivesMax) {
            hurt = Managers.Level.playersLivesMax;
        }
        
        LivesUpdated?.Invoke();
        
        if (hurt == Managers.Level.playersLivesMax)
        {
            LevelFailed?.Invoke();
        }
        else
        {
            PlayerLivesDecreased?.Invoke();
        }
    }

    public void Respawn()
    {
        UpdateData(0);
    }
    
    public void UpdateData(int hurt)
    {
        this.hurt = hurt;
        LivesUpdated?.Invoke();
    }
}
