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
    [SerializeField] private GameObject enemyLevelThree;
    
    [SerializeField] private GameObject bigExplosionPrefab;
    [SerializeField] private GameObject smallExplosionPrefab;

    private GameObject[] _startPoints;
    private Random _random;
    private GameObject _enemy;

    void Start()
    {
        PlayerManager.PlayerLivesDecreased += PlayerLivesDecreasedCallback;
        EnemyManager.EnemyHit += EnemyHitCallback;
        
        Shell.shellExplosion += shellExplosionCallBack;
        
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
        
        Shell.shellExplosion -= shellExplosionCallBack;
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
            case 3:
                _enemy = enemyLevelThree;
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

    private void shellExplosionCallBack(Vector3 position, bool onTarget)
    {
        StartCoroutine(ExplodeHitOffTarget(position, onTarget));
    }
    
    private IEnumerator ExplodeHitOffTarget(Vector3 position, bool onTarget)
    {
        GameObject explosionPrefab = onTarget ? bigExplosionPrefab : smallExplosionPrefab;
        GameObject shellExplosion = Instantiate(explosionPrefab) as GameObject;
        shellExplosion.transform.position = position;
        
        yield return new WaitForSeconds(2.0f);
        
        Destroy(shellExplosion);
    }
}
