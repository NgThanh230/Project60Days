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

    [Header("UI")]
    public GameObject pauseScreen;

    //Stats Displays
    public TextMeshProUGUI HealthDisplay;
    public TextMeshProUGUI RecoveryDisplay;
    public TextMeshProUGUI MoveSpeedDisplay;
    public TextMeshProUGUI MightDisplay;
    public TextMeshProUGUI ProjectileSpeedDisplay;
    public TextMeshProUGUI MagnetDisplay;

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
    }
}
