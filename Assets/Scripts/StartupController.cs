using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartupController : MonoBehaviour
{
    [SerializeField] private Slider progressBar;

    void Start()
    {
        Managers.ManagersStarted += ManagersStartedCallback;
        Managers.ManagersProgress += ManagersProgressCallback;
    }
    
    void OnDestroy()
    {
        Managers.ManagersStarted -= ManagersStartedCallback;
        Managers.ManagersProgress -= ManagersProgressCallback;
    }
    
    void Update()
    {
        
    }

    private void ManagersStartedCallback()
    {
        Managers.Level.GoToNext();
    }

    private void ManagersProgressCallback(int numReady, int numModules)
    {
        float progress = (float)numReady / numModules;
        progressBar.value = progress;
    }
}
