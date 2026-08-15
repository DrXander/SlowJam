using Ink.Parsed;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public enum GameState
    {
        Playing,
        Paused,
    }

    private int dayCount = 0;


public GameState CurrentState { get; private set; }

    [SerializeField] private GameObject pausedUI;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void States(GameState newState)
    {
        CurrentState = newState;
        switch (CurrentState)
        {
            case GameState.Playing:
                Time.timeScale = 1f;
                pausedUI.SetActive(false);
                break;
            case GameState.Paused:
                Time.timeScale = 0f;
                pausedUI.SetActive(true);
                break;
        }
    }

    public void Update()
    {  
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CurrentState == GameState.Playing)
            {
                States(GameState.Paused);
            }
            else if (CurrentState == GameState.Paused)
            {
                States(GameState.Playing);
            }
        }
    }



}
