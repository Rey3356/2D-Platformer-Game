using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelFinish : MonoBehaviour
{
    [SerializeField] private GameObject GameCompleteScreen;
    public bool playerDeath;
    void Start()
    {
        playerDeath = false;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<PlayerController>() != null)
        {
            Debug.Log("Level is Over");
            //Scene nextScene = SceneManager.GetSceneByBuildIndex((SceneManager.GetActiveScene().buildIndex) + 1);
            int nextBI = LevelManager.Instance.LevelCompleteMarker();
            if (LevelManager.Instance.GetLevelStatus(LevelManager.Instance.Levels[nextBI]) == LevelStatus.Unlocked)
            {
                if(playerDeath == false)
                {
                    Time.timeScale = 0f;
                    GameCompleteScreen.SetActive(true);
                }               
            }
        }
    }
}
