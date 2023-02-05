using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status {get; private set;}
    public int curLevel {get; private set;}
    public int maxLevel {get; private set;}
    
    
    public int startEnemyCount {get; private set;}
    public int enemiesToKill {get; private set;}
    public int playersLivesMax {get; private set;}
    
    
    public static Action GameComplete;

    public void Startup()
    {
        UpdateData(0, 3);
        status = ManagerStatus.Started;
    }

    public void GoToNext() {
        if (curLevel < maxLevel) {
            curLevel++;
            string name = "Level" + curLevel;
            Debug.Log("Loading " + name);
            LoadLevel(name);
        } else {
            Debug.Log("Last level");
            GameComplete?.Invoke();
        }
    }
    
    public void RestartCurrent() {
        string name = "Level" + curLevel;
        Debug.Log("Loading " + name);
        LoadLevel(name);
    }
    
    public void UpdateData(int curLevel, int maxLevel) {
        this.curLevel = curLevel;
        this.maxLevel = maxLevel;
    }

    private void LoadLevel(string name)
    {
        SetupLevel();
        Managers.Player.Respawn();
        Managers.Enemy.Respawn();
        Time.timeScale = 1f;
        SceneManager.LoadScene(name);
    }

    private void SetupLevel()
    {
        switch (this.curLevel)
        {
            case 1:
                this.startEnemyCount = 3;
                this.enemiesToKill = 3;
                this.playersLivesMax = 3;
                break;
            case 2:
                this.startEnemyCount = 5;
                this.enemiesToKill = 4;
                this.playersLivesMax = 2;
                break;
            case 3:
                this.startEnemyCount = 5;
                this.enemiesToKill = 5;
                this.playersLivesMax = 3;
                break;
        }
    }
}
