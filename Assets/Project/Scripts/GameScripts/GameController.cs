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
    public GameState CurrentState { get {return currentState;} set {currentState = value;}}
    public SoundController SoundController{get{ return soundController;}}
    public LevelGeneration LevelGeneration{get{return levelGeneration;}}
    public PlayerController PlayerController{get{return playerController;}}
    public BonusController BonusController{get{return bonusController;}}
    public BallManager BallManager{get{return ballManager;}}
    public DataManager DataManager{get{return dataManager;}}
    private int currentLevel = 1;
    private bool initGame = false;
    private GameState previusPauseState;
    [Header("UpdateUIMethod")]
    public UnityEvent EventUpdateUI;



    public static GameController GetInstance() {
        if (instance == null)
            instance = new GameController();
        return instance;
    }

    void Start() {
        instance = this;
        SetGameState("menu");
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

    public void SetGameState(string state) {
        var newState = (GameState)System.Enum.Parse(typeof(GameState), state, true);
        previusPauseState = CurrentState;
        CurrentState = newState;
        if (CurrentState == GameState.pregame) {
            LevelGeneration.ImageGenerationLevel(currentLevel);
        }
        if (CurrentState == GameState.game) {
            if (!initGame && previusPauseState!=GameState.pause) {
                initGame = true;
                ballManager.CreateBall();
                ballManager.ActivateAllBalls();
            }
        }
        if (CurrentState == GameState.goodend) {
            initGame = false;
            ballManager.SleepAllBalls();
        }
        if (CurrentState == GameState.badend)
        {
            initGame = false;
            ballManager.RemoveAllBall();
        }
        if (CurrentState == GameState.menu){
            ReturnInitially();
        }
        if(EventUpdateUI != null)
            EventUpdateUI.Invoke();
    }


  

   
    public void ReturnInitially() {
        levelGeneration.ClearLevel();
        ballManager.RemoveAllBall();
        dataManager.RefreshScore();
        PlayerController.ResetPlayer(dataManager);
    }


    public void RetryCurrentLevel() {
        ReturnInitially();
        SetGameState("pregame");
        if (EventUpdateUI != null)
            EventUpdateUI.Invoke();
    }

    public void NextLevelChange()
    {
        if (currentLevel == 4)
        {
            ReturnInitially();
            SetGameState("menu");
        }
        else
        {
            ReturnInitially();
            SetCurrentLevel(currentLevel+1);
            SetGameState("pregame");
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
            SetGameState("badend");
        }
        if(levelGeneration.blocksCount<=0)
        {
            SetGameState("goodend");
        }
    }

}
