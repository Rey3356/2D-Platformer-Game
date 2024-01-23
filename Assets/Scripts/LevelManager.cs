using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance { get { return instance; } }

    public string[] Levels = new string[5];
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        SetLevelStatus(Levels[0], LevelStatus.Unlocked);
        SetLevelStatus(Levels[1], LevelStatus.Unlocked);
        SetLevelStatus(Levels[2], LevelStatus.Locked);
        SetLevelStatus(Levels[3], LevelStatus.Locked);
        SetLevelStatus(Levels[4], LevelStatus.Locked);
        Debug.Log("LevelManagerSecond, Level 1 is currently : " + GetLevelStatus(Levels[0]));

    }

    public int LevelCompleteMarker()
    {
        //sets current level to complete on completion
        Scene currentScene = SceneManager.GetActiveScene();
        SetLevelStatus(currentScene.name, LevelStatus.Completed);

        //sets next level to unlocked
        int nextBI = Array.FindIndex(Levels, level => level == currentScene.name) + 1;
        if(nextBI < Levels.Length)
        {
            SetLevelStatus(Levels[nextBI], LevelStatus.Unlocked);
        }
        return nextBI;
    }
    public LevelStatus GetLevelStatus(string level)
    {
        return (LevelStatus) PlayerPrefs.GetInt(level, 0);
    }

    public void SetLevelStatus(string level, LevelStatus levelstatus)
    {
        PlayerPrefs.SetInt(level, (int)levelstatus);
        Debug.Log("Setting Level " + level + " status to : "  + levelstatus);
    }
}

