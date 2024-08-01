using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeController : MonoBehaviour
{
    public GameObject homePanel;
    public GameObject infoPanel;
    public GameObject achievementPanel;
    public GameObject achievementPanelText;

    protected TMP_Text achievementText;
    protected string completeGameText = "Congratulations, you have completed the game!!! \r\nYeahhhhh!";
    protected string incompleteGameText = "Win all level to unlock achievement!";

    private void Start()
    {
        this.homePanel.SetActive(true);
        this.infoPanel.SetActive(false);
        this.achievementPanel.SetActive(false);
        this.achievementText = achievementPanelText.GetComponent<TMP_Text>();

        Debug.Log(achievementPanelText.GetComponent<TMP_Text>());
        this.SetAchievementText();


    }

    protected void SetAchievementText()
    {
        Debug.Log("unlocked_level:" + PlayerPrefs.GetInt("unlocked_level").ToString());
        if (SceneManager.sceneCountInBuildSettings != PlayerPrefs.GetInt("unlocked_level"))
        {
            achievementText.text = "Thanks for playing!\n\n" + incompleteGameText;
            return;
        }

        achievementText.text = "Thanks for playing!\n\n" + completeGameText;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenInfoPanel()
    {
        this.homePanel.SetActive(false);
        this.infoPanel.SetActive(true);
        this.achievementPanel.SetActive(false);
    }
    public void OpenStarPanel()
    {
        this.homePanel.SetActive(false);
        this.infoPanel.SetActive(false);
        this.achievementPanel.SetActive(true);
    }


    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToHome()
    {
        this.homePanel.SetActive(true);
        this.infoPanel.SetActive(false);
        this.achievementPanel.SetActive(false);
    }
}