using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text enemyScoreLabel;
    [SerializeField] private Text livesLabel;
    [SerializeField] private Text levelEndLabel;

    void Start()
    {
        EnemyManager.EnemyHit += EnemyHitCallback;
        PlayerManager.LivesUpdated += LivesUpdatedCallback;
        EnemyManager.LevelComplete += LevelCompleteCallback;
        PlayerManager.LevelFailed += LevelFailedCallback;
        LevelManager.GameComplete += GameCompleteCallback;
        
        levelEndLabel.gameObject.SetActive(false);

        LivesUpdatedCallback();
        EnemyHitCallback();
    }

    private void OnDestroy()
    {
        EnemyManager.EnemyHit -= EnemyHitCallback;
        PlayerManager.LivesUpdated -= LivesUpdatedCallback;
        EnemyManager.LevelComplete -= LevelCompleteCallback;
        PlayerManager.LevelFailed -= LevelFailedCallback;
        LevelManager.GameComplete -= GameCompleteCallback;
    }

    private void EnemyHitCallback() {
        string message = Managers.Enemy.killed + "/" + Managers.Level.enemiesToKill;
        enemyScoreLabel.text = message;
    }

    private void LivesUpdatedCallback()
    {
        string message = (Managers.Level.playersLivesMax - Managers.Player.hurt) + "/" + Managers.Level.playersLivesMax;
        livesLabel.text = message;
    }
    
    private void LevelCompleteCallback() {
        StartCoroutine(CompleteLevel());
    }
    
    private IEnumerator CompleteLevel() {
        levelEndLabel.gameObject.SetActive(true);
        levelEndLabel.text = "Level Complete!";
        yield return new WaitForSeconds(2);
        
        Managers.Level.GoToNext();
    }
    
    private void LevelFailedCallback() {
        StartCoroutine(FailLevel());
    }

    private IEnumerator FailLevel()
    {
        levelEndLabel.gameObject.SetActive(true);
        levelEndLabel.text = "Level Failed";
        yield return new WaitForSeconds(2);
        Managers.Player.Respawn();
        Managers.Enemy.Respawn();
        Managers.Level.RestartCurrent();
    }
    
    private void GameCompleteCallback() {
        levelEndLabel.gameObject.SetActive(true);
        levelEndLabel.text = "Game Complete!";
        
        Time.timeScale = 0f;
    }
    
    public void SaveGame() {
        Managers.Data.SaveGameState();
    }
    
    public void LoadGame() {
        Managers.Data.LoadGameState();
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
