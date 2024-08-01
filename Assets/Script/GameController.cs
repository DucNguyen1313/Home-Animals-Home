using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] protected int animalCount = 0;
    [SerializeField] protected int selectedNumber = 0;

    public List<GameObject> animals;
    public List<GameObject> selectedImages;

    public GameObject winLevelPanel;
    public GameObject nextLevelButton;

    public GameObject retryButton;
    public GameObject menuButton;

    public GameObject cam;
    public GameObject animalsGameObject;

    private void Start()
    {
        PlayerPrefs.SetInt("animal_in_target_area_count", 0);

        selectedNumber = 0;
    }


    void Update()
    {
        animalCount = PlayerPrefs.GetInt("animal_in_target_area_count");

        SetSelectedImage();

        SetIsSelectedForAnimal();

        SetIsShowTimeForAnimal();

        SetWinLevel();

        UpdateSelectedNumber();
    }
    protected void SetSelectedImage()
    {
        foreach (GameObject selectedImage in selectedImages)
        {
            selectedImage.SetActive(false);
        }
        selectedImages[selectedNumber].SetActive(true);
    }

    protected void UpdateSelectedNumber()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedNumber = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedNumber = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedNumber = 2;
        }
    }

    protected void SetIsSelectedForAnimal()
    {
        for (int i = 0; i<= 2; i++)
        {
            if (i == selectedNumber)
            {
                SetIsSelected(animals[i], true);
                continue;
            }
            SetIsSelected(animals[i], false);
        }
    }

    protected void SetIsShowTimeForAnimal()
    {
        if (animalCount != 3)
        {
            SetAllIsShowTime(false);
            return;
        }

        SetAllIsShowTime(true);
    }

    protected void SetWinLevel()
    {
        if (animalCount != 3) return;

        UnlockNewLevel();

        StartCoroutine("WinLevelEvent");
 
    }


    protected void SetIsSelected(GameObject animal, bool value)
    {
        AnimalMovement animalMovement = animal.GetComponent<AnimalMovement>();
        if (animalMovement == null) return;

        animalMovement.IsSelected = value;
    }

    protected void SetAllIsShowTime(bool value)
    {
        foreach (GameObject animal in animals)
        {
            AnimalMovement animalMovement = animal.GetComponent<AnimalMovement>();
            if (animalMovement == null) return;

            animalMovement.IsShowTime = value;
        }
    }

    protected void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex < PlayerPrefs.GetInt("reached_index")) return;

        PlayerPrefs.SetInt("reached_index", SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.SetInt("unlocked_level", PlayerPrefs.GetInt("unlocked_level", 1) + 1);
        PlayerPrefs.Save();

        Debug.Log("Unlock level: " + PlayerPrefs.GetInt("unlocked_level", 1));
    }
    private IEnumerator WinLevelEvent()
    {
        yield return new WaitForSeconds(2f);
        winLevelPanel.SetActive(true);

        if (SceneManager.sceneCountInBuildSettings == SceneManager.GetActiveScene().buildIndex + 1)
        {
            nextLevelButton.SetActive(false);
        }
    }


    public void RetryGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }


    public void GoToNextLevel()
    {
        Debug.Log("Next Level");

        winLevelPanel.SetActive(false);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex+1);
    }

    public void OpenMenuScene()
    {
        SceneManager.LoadScene("HomeScene");
    }
}
