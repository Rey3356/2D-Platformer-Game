using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

[RequireComponent(typeof(Button))]
public class LevelLoader : MonoBehaviour
{
    private Button button;
    public string LevelName;
    public GameObject LevelStatusDisp;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(onClick);
    }

    private void Start()
    {
        button = GetComponent<Button>();
        Debug.Log("LevelLOad Start");
        if(LevelManager.Instance.GetLevelStatus(LevelName) == LevelStatus.Locked)
        {
            button.image.color = new Color(255.0f, 200.0f, 200.0f, 255.0f);
            Debug.Log("coloring red");
        }
        else
        {
            button.image.color = new Color(255.0f, 255.0f, 255.0f, 255.0f);
            Debug.Log("coloring plain");
        }
        
    }


    private void onClick()
    {
        
        if (LevelName != "MM") //Method Mode
        {
            LevelStatus levelStatus = LevelManager.Instance.GetLevelStatus(LevelName);
            switch (levelStatus)
            {
                case LevelStatus.Locked:
                    Debug.Log(LevelName + " is currently " + levelStatus);
                    PlaySound("incorrectClick");
                    StartCoroutine(LockedStatus(LevelName));
                    break;
                case LevelStatus.Unlocked:
                    PlaySound("correctClick");
                    SceneManager.LoadSceneAsync(LevelName);
                    break;
                case LevelStatus.Completed:
                    PlaySound("correctClick");
                    SceneManager.LoadSceneAsync(LevelName);
                    break;

            }

        }    
        
    }

    public void PlaySound(string choice)
    {
        if(choice == "correctClick")
        {
            SoundManager.Instance.PlaySFX(Sounds.ButtonClick);
        }
        else if (choice == "incorrectClick")
        {
            SoundManager.Instance.PlaySFX(Sounds.ButtonClickWrong);
        }
    }

    IEnumerator LockedStatus(string name)
    {
        LevelStatusDisp.SetActive(true);
        //Debug.Log(name + " is currently Locked!");
        LevelStatusDisp.GetComponent<TextMeshProUGUI>().text = name + " is currently Locked!";
        yield return new WaitForSeconds(2);
        LevelStatusDisp.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void LevelQuit()
    {
        //For going to lobby screen from level
        SceneManager.LoadSceneAsync(0);
    }

    public void Quitter()
    {
        Application.Quit();
    }
}
