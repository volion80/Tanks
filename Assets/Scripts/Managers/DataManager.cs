using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status {get; private set;}
    
    public void Startup() {
        status = ManagerStatus.Started;
    }

    public void SaveGameState()
    {
        PlayerPrefs.SetInt("hurt", Managers.Player.hurt);
        PlayerPrefs.SetInt("curLevel", Managers.Level.curLevel);
        PlayerPrefs.SetInt("saved", 1);
    }

    public void LoadGameState()
    {
        int saved = PlayerPrefs.GetInt("saved", 0);
        if (saved == 0)
        {
            Debug.Log("No saved game found");
            return;
        }

        Managers.Player.UpdateData(PlayerPrefs.GetInt("hurt"));
        Managers.Level.UpdateData(PlayerPrefs.GetInt("curLevel"), Managers.Level.maxLevel);
        Managers.Enemy.UpdateData(0);
        
        Managers.Level.RestartCurrent();
    }
}
