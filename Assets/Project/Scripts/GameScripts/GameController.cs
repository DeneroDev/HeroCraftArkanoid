using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour {
    private static GameController instance;
    public enum GameState { menu,pregame,game,goodend,badend,pause,end }
    private GameState currentState;
    [Header("GameComponents")]
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private LevelGeneration levelGeneration;
    [SerializeField]
    private SoundController soundController;
    [SerializeField]
    private BonusController bonusController;
    [SerializeField]
    private BallManager ballManager;
    [SerializeField]
    private DataManager dataManager;
    [SerializeField]
    private GameSetting gameSetting;
    public GameState CurrentState { get {return currentState;} set {currentState = value;}}
    public SoundController SoundController{get{ return soundController;}}
    public LevelGeneration LevelGeneration{get{return levelGeneration;}}
    public PlayerController PlayerController{get{return playerController;}}
    public BonusController BonusController{get{return bonusController;}}
    public BallManager BallManager{get{return ballManager;}}
    public DataManager DataManager{get{return dataManager;}}
    public GameSetting GameSetting{get{return gameSetting;}}

    private int currentLevel = 1;
    private bool initGame = false;
    private GameState previusPauseState;
    [Header("UpdateUIMethod")]
    public UnityEvent EventUpdateUI;



    public static GameController GetInstance() {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    void Start() {
        SetGameState(GameState.menu);
    }


    void Update() {
        switch (CurrentState) {
            case GameState.game:
                Time.timeScale = 1f;
                break;
            case GameState.pause:
                Time.timeScale = 0;
                break;
        }
    }

    public void SetCurrentLevel(int level)
    {
        currentLevel = level;
    }

    public void SetGameState(GameState newState)
    {
        previusPauseState = CurrentState;
        CurrentState = newState;
        switch (CurrentState) {
            case GameState.pregame:
                LevelGeneration.ImageGenerationLevel(currentLevel);
                break;
            case GameState.game:
                if (!initGame && previusPauseState != GameState.pause)
                {
                    initGame = true;
                    ballManager.CreateBall();
                    ballManager.ActivateAllBalls();
                }
                break;
            case GameState.goodend:
                initGame = false;
                ballManager.SleepAllBalls();
                break;
            case GameState.badend:
                initGame = false;
                ballManager.RemoveAllBall();
                break;
            case GameState.menu:
                ReturnInitially();
                break;
        }

        if (EventUpdateUI != null)
            EventUpdateUI.Invoke();
    }

    public void SetGameState(string state) {
        var newState = (GameState)System.Enum.Parse(typeof(GameState), state, true);
        SetGameState(newState);
        }


  

   
    public void ReturnInitially() {
        levelGeneration.ClearLevel();
        ballManager.RemoveAllBall();
        dataManager.RefreshScore();
        PlayerController.ResetPlayer(gameSetting);
    }


    public void RetryCurrentLevel() {
        ReturnInitially();
        SetGameState(GameState.pregame);
        if (EventUpdateUI != null)
            EventUpdateUI.Invoke();
    }

    public void NextLevelChange()
    {
        if (currentLevel == 4)
        {
            ReturnInitially();
            SetGameState(GameState.menu);
        }
        else
        {
            ReturnInitially();
            SetCurrentLevel(currentLevel+1);
            SetGameState(GameState.pregame);
        }
        if (EventUpdateUI != null)
            EventUpdateUI.Invoke();
    }


    public void CloseApplication() {
        Application.Quit();
    }

    public void CheckEndGame() {
        if (ballManager.GetBallsCount() <= 0)
        {
            SetGameState(GameState.badend);
        }
        if(levelGeneration.blocksCount<=0)
        {
            SetGameState(GameState.goodend);
        }
    }

}
