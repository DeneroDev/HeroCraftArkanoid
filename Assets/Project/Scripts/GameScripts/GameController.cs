using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    private static GameController instance;
    public enum GameState { start, game, pause, end }
    private GameState currentState = GameState.start;
    [Header("StartConfig")]
    [SerializeField]
    [Range(100, 1000)]
    private float _startForce = 500;
    [SerializeField]
    private Vector2 _startVector;
    [Range(5, 10)]
    [SerializeField]
    private float _maxVelocityBall = 10;
    [Range(1, 5)]
    [SerializeField]
    private float _minVelocityBall = 3;
    [Header("GameComponents")]
    [SerializeField]
    private GameObject BallPrefab;
    [SerializeField]
    private UIController uiController;
    [SerializeField]
    private PlayerController playerC;
    [SerializeField]
    private LevelGeneration levelGeneration;
    [SerializeField]
    private SoundController SoundController;
    [SerializeField]
    private BonusController bonusController;
    private int score = 0;
    private int blocksCount;
    private int currentLevel = 1;

    private List<GameObject> levelsBlocks = new List<GameObject>();
    private List<GameObject> balls = new List<GameObject>();

    public GameState CurrentState
    {
        get
        {
            return currentState;
        }

        set
        {
            currentState = value;
        }
    }

    public float MinVelocityBall
    {
        get
        {
            return _minVelocityBall;
        }

        set
        {
            _minVelocityBall = value;
        }
    }

    public float MaxVelocityBall
    {
        get
        {
            return _maxVelocityBall;
        }

        set
        {
            _maxVelocityBall = value;
        }
    }

    public float StartForce
    {
        get
        {
            return _startForce;
        }

        set
        {
            _startForce = value;
        }
    }

    public Vector2 StartVector
    {
        get
        {
            return _startVector;
        }

        set
        {
            _startVector = value;
        }
    }

    public void CloseApplication() {
        Application.Quit();
    }

    public static GameController GetInstance() {
        if (instance == null)
            instance = new GameController();
        return instance;
    }

    void Start() {
        instance = this;
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

    public void SetPauseState() {
        CurrentState = GameState.pause;
        uiController.OnPause();
    }

    public void BackFromPause() {
        CurrentState = GameState.game;
        uiController.OffPause();
    }

    public void GenerationLevel() {
        levelGeneration.ImageGenerationLevel(currentLevel);
    }

    public void SetCurrentLevel(int level) {
        currentLevel = level;
    }

    public void SetStateGame() {
        currentState = GameState.game;
        var ball = Instantiate(BallPrefab, new Vector3(0, -4.6f, 0), Quaternion.identity);
        AddBall(ball);
        ball.GetComponent<BallController>().ActivatedBall();
        uiController.OffPreGamePanel();
    }

    public void AddBall(GameObject ball) {
        balls.Add(ball);
    }

    public int GetBallsCount() {
        return balls.Count;
    }

    public void DeleteBalls(GameObject ball) {
        balls.Remove(ball);
        Destroy(ball);
    }

    public bool SetStateEnd(bool dead)
    {
        if (dead)
        {
            if ((balls.Count-1) <= 0) {
                currentState = GameState.end;
                uiController.OnEndGamePanel(dead);
                return false;
            }
            else
                return true;
        }
        else
        {
            currentState = GameState.end;
            uiController.OnEndGamePanel(dead);
            for (int i = 0; i < balls.Count; i++) {
                if (balls != null)
                    balls[i].GetComponent<BallController>().SleepRb();
            }
            return false;
        }
    }

    public void AddScore(int score) {
        this.score += score;
        uiController.ScoreUpdate(this.score);
    }

    


    public void ReturnInitially() {
        uiController.OffEndGamePanel();
        blocksCount = 0;
        levelsBlocks.RemoveAll(levelsBlocks => { Destroy(levelsBlocks); return true; });
        bonusController.GetBonusList().RemoveAll(bonusController=> { Destroy(bonusController); return true; });
        balls.RemoveAll(balls => { Destroy(balls); return true; });
        score = 0;
        playerC.transform.localPosition = new Vector3(0, -4.9f,0);
    }

    public void SubtractionBlock() {
        blocksCount--;
        SoundController.PlaySoundBreakSound();
        if (blocksCount <= 0)
            SetStateEnd(false);
    }
    public void AddBlock(GameObject block,bool ignoreCount) {
        levelsBlocks.Add(block);
        if(!ignoreCount)
            blocksCount = blocksCount+2;
    }


    public PlayerController GetPlayerController() {
        return playerC;
    }

    public List<GameObject> GetBallsList() {
        return balls;
    }

    public BonusController GetBonusController()
    {
        return bonusController;
    }

    public void RetryCurrentLevel() {
        ReturnInitially();
        GenerationLevel();
    }

    public void NextLevelChange()
    {
        if (currentLevel == 4)
        {
            ReturnInitially();
            uiController.OnMenu();
        }
        else
        {
            ReturnInitially();
            currentLevel++;
            GenerationLevel();
        }
    }

    public int GetScore() {
        return score;
    }
}
