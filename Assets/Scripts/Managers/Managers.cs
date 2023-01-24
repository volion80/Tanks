using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DataManager))]
[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(EnemyManager))]
[RequireComponent(typeof(LevelManager))]
public class Managers : MonoBehaviour
{
    public static DataManager Data {get; private set;}
    public static LevelManager Level {get; private set;}
    public static PlayerManager Player {get; private set;}
    public static EnemyManager Enemy {get; private set;}

    private List<IGameManager> _startSequence;

    public static Action ManagersStarted;
    public static Action<int, int> ManagersProgress;


    void Awake() {
        DontDestroyOnLoad(gameObject);
        
        Data = GetComponent<DataManager>();
        Level = GetComponent<LevelManager>();
        Player = GetComponent<PlayerManager>();
        Enemy = GetComponent<EnemyManager>();

        _startSequence = new List<IGameManager>();
        _startSequence.Add(Player);
        _startSequence.Add(Enemy);
        _startSequence.Add(Level);
        _startSequence.Add(Data);
        
        StartCoroutine(StartupManagers());
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartupManagers()
    {
        foreach (IGameManager manager in _startSequence) {
            manager.Startup();
        }
        yield return null;
        
        int numModules = _startSequence.Count;
        int numReady = 0;

        while (numReady < numModules)
        {
            int lastReady = numReady;
            numReady = 0;

            foreach (IGameManager manager in _startSequence)
            {
                if (manager.status == ManagerStatus.Started)
                    numReady++;
            }

            if (numReady > lastReady)
            {
                Debug.Log("Progress: " + numReady + "/" + numModules);
                ManagersProgress?.Invoke(numReady, numModules);
            }

            yield return null;
        }
        
        Debug.Log("All managers started up");
        ManagersStarted?.Invoke();
    }
}
