using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum GameState
    {
        GamePlay,
        Paused,
        GameOver
    }

    public GameState state;
    public GameState preState;

    [Header("Screens")]
    public GameObject pauseScreen;
    public GameObject resultsScreen;
    [Header("Stat Displays")]
    public TextMeshProUGUI HealthDisplay;
    public TextMeshProUGUI RecoveryDisplay;
    public TextMeshProUGUI MoveSpeedDisplay;
    public TextMeshProUGUI MightDisplay;
    public TextMeshProUGUI ProjectileSpeedDisplay;
    public TextMeshProUGUI MagnetDisplay;
    [Header("Results Screen Displays")]
    public Image chosenCharacterImage;
    public TextMeshProUGUI chosenCharacterName;
    public TextMeshProUGUI levelReachedDisplay;
    public List<Image> chosenWeaponUI = new List<Image>(6);
    public List<Image> chosenPassiveItemsUI = new List<Image>(6);

    public bool isGameOver = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("EXTRA" + this + "DELETED");
            Destroy(gameObject);
        }
        DisableSreens();
    }
    private void Update()
    {
        switch (state)
        {
            case GameState.GamePlay:
                CheckForPauseAndResume();
                break;
            case GameState.Paused:
                CheckForPauseAndResume();
                break;
            case GameState.GameOver:
                
                break;
            default:
                Debug.LogWarning("State does not exist");
                break;
        }
    }
    public void ChangeState(GameState newState)
    {
        state = newState;
    }
    public void PauseGame()
    {
        if (state != GameState.Paused)
        {
            preState = state;
            ChangeState(GameState.Paused);
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
            Debug.Log("Game is Paused");
        }
        
    }
    public void ResumeGame()
    {
        if (state == GameState.Paused)
        {
            ChangeState(preState);
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
            Debug.Log("Game is Resumed");
        }
    }
    void CheckForPauseAndResume()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (state == GameState.Paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    void DisableSreens()
    {
        pauseScreen.SetActive(false);
        resultsScreen.SetActive(false);
    }
    public void GameOver()
    {
        if (state != GameState.GameOver)
        {
            ChangeState(GameState.GameOver);
            Time.timeScale = 0f;
            Debug.Log("Game Over");
            DisplayResults();
        }
    }
    void DisplayResults()
    {
        resultsScreen.SetActive(true);
    }
    public void AssignChosenCharacterUI(CharacterScriptableObject chosenCharacterData)
    {
        chosenCharacterImage.sprite = chosenCharacterData.Icon;
        chosenCharacterName.text = chosenCharacterData.Name;
    }
    public void AssignLevelReachedUI(int levelReachedData)
    {
        levelReachedDisplay.text = levelReachedData.ToString();
    }

    public void AssignWeaponAndPassiveItemsUI(List<Image> chosenWeaponsData, List<Image> chosenPassiveItemsData)
    {
        if (chosenWeaponsData.Count != chosenWeaponUI.Count || chosenPassiveItemsData.Count != chosenPassiveItemsUI.Count)
        {
            Debug.Log("Error Length Data");
            return;
        }
        for (int i = 0; i < chosenWeaponUI.Count; i++)
        {
            if (chosenWeaponsData[i].sprite)
            {
                chosenWeaponUI[i].enabled = true;
                chosenWeaponUI[i].sprite = chosenWeaponsData[i].sprite;
            }
            else
            {
                chosenWeaponUI[i].enabled = false;
            }
        }

        for (int i = 0; i < chosenPassiveItemsUI.Count; i++)
        {
            if (chosenPassiveItemsData[i].sprite)
            {
                chosenPassiveItemsUI[i].enabled = true;
                chosenPassiveItemsUI[i].sprite = chosenPassiveItemsData[i].sprite;
            }
            else
            {
                chosenPassiveItemsUI[i].enabled = false;
            }
        }
    }
}
