using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyLevelOne;
    [SerializeField] private GameObject enemyLevelTwo;

    private GameObject[] _startPoints;
    private Random _random;
    private GameObject _enemy;

    void Start()
    {
        PlayerManager.PlayerLivesDecreased += PlayerLivesDecreasedCallback;
        EnemyManager.EnemyHit += EnemyHitCallback;
        _startPoints = GameObject.FindGameObjectsWithTag("Start Point");
        _random = new Random();

        SetEnemy();
        SetMusic();
        
        CreateEnemies();
    }

    private void OnDestroy()
    {
        PlayerManager.PlayerLivesDecreased -= PlayerLivesDecreasedCallback;
        EnemyManager.EnemyHit -= EnemyHitCallback;
    }

    void Update()
    {
        
    }

    private void SetEnemy()
    {
        int level = Managers.Level.curLevel;
        switch (level)
        {
            case 1:
                _enemy = enemyLevelOne;
                break;
            case 2:
                _enemy = enemyLevelTwo;
                break;
        }
    }

    private void SetMusic()
    {
        Managers.Audio.PlayLevelMusic();
    }

    void CreateEnemy()
    {
        int startPointIndex = _random.Next(_startPoints.Length);
        Vector3 startPointPosition = _startPoints[startPointIndex].transform.position;
        
        GameObject enemy = Instantiate(_enemy);
        enemy.transform.position = new Vector3(startPointPosition.x, startPointPosition.y, startPointPosition.z);
        float angle = UnityEngine.Random.Range(0, 360);
        enemy.transform.Rotate(0, angle, 0);
    }

    void CreateEnemies()
    {
        for (int i = 0; i < Managers.Level.startEnemyCount; i++)
        {
            CreateEnemy();
        } 
    }

    void CreatePlayer()
    {
        Instantiate(playerPrefab);
    }
    
    private IEnumerator CreateNewPlayer()
    {
        yield return new WaitForSeconds(1);
        CreatePlayer();
    }

    private void PlayerLivesDecreasedCallback()
    {
        StartCoroutine(CreateNewPlayer());
    }

    private void EnemyHitCallback()
    {
        CreateEnemy();
    }
}
