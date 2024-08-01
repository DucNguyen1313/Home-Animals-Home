using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;

    private void Awake()
    {
        int unlockedLevel = PlayerPrefs.GetInt("unlocked_level", 1);

        if (SceneManager.sceneCountInBuildSettings == unlockedLevel)
        {
            unlockedLevel -= 1;
        }
        Debug.Log(unlockedLevel);
        Debug.Log(buttons.Length);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void OpenLevel(int levelId)
    {
        string levelName = "GameSceneLevel" + levelId; ;
        SceneManager.LoadScene(levelName);
    }


}
