using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AudioManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status {get; private set;}
    
    [SerializeField] private AudioSource musicSource;
    
    [SerializeField] private string level1Music;
    [SerializeField] private string level2Music;

    public void Startup() {
        status = ManagerStatus.Started;
    }

    public void PlayLevelMusic()
    {
        string levelMusic = string.Empty;
        switch (Managers.Level.curLevel)
        {
            case 1:
                levelMusic = level1Music;
                break;
            case 2:
                levelMusic = level2Music;
                break;
        }
        if (!string.IsNullOrEmpty(levelMusic))
            PlayMusic(Resources.Load("Music/" + levelMusic) as AudioClip);
    }

    private void PlayMusic(AudioClip clip) {
        musicSource.clip = clip;
        musicSource.Play();
    }
    
    public void StopMusic() {
        musicSource.Stop();
    }
}
