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
    private float StartForce = 500;
    [SerializeField]
    private Vector2 StartVector;
    [Range(5, 10)]
    [SerializeField]
    private float maxVelocityBall = 10;
    [Range(1, 5)]
    [SerializeField]
    private float minVelocityBall = 3;
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
    private List<GameObject> levelsBlocks = new List<GameObject>();
    private int blocksCount;
    private int currentLevel = 1;
    private List<GameObject> balls = new List<GameObject>();
    private List<Rigidbody2D> ballsRb = new List<Rigidbody2D>();

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

    public void CloseApplication() {
        Application.Quit();
    }

    public static GameController GetInstance() {
        if (instance == null)
            instance = new GameController();
        return instance;
    }

    // Use this for initialization
    void Start() {
        instance = this;
    }

    // Update is called once per frame
    void Update() {
        switch (CurrentState) {
            case GameState.start:

                break;
            case GameState.game:
                SpeedLimittedBall();
                break;
            case GameState.pause:

                break;
            case GameState.end:

                break;
        }
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
        ballsRb[0].AddForce(StartVector * StartForce);
        uiController.OffPreGamePanel();
    }

    public void AddBall(GameObject ball) {
        balls.Add(ball);
        ballsRb.Add(ball.GetComponent<Rigidbody2D>());
    }

    public int GetBallsCount() {
        return balls.Count;
    }

    public void DeleteBalls(GameObject ball) {
        balls.Remove(ball);
        ballsRb.Remove(ball.GetComponent<Rigidbody2D>());
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
                if (ballsRb[i] != null)
                    ballsRb[i].Sleep();
            }
            return false;
        }
    }

    public void AddScore(int score) {
        this.score += score;
        uiController.ScoreUpdate(this.score);
    }

    private void SpeedLimittedBall() {
        for (int i = 0; i < balls.Count; i++) {
            if (ballsRb[i] != null) {
                if (ballsRb[i].velocity.x < -maxVelocityBall)
                    ballsRb[i].velocity = new Vector2(-maxVelocityBall, ballsRb[i].velocity.y);
                if (ballsRb[i].velocity.y < -maxVelocityBall)
                    ballsRb[i].velocity = new Vector2(ballsRb[i].velocity.x, -maxVelocityBall);
                if (ballsRb[i].velocity.x > maxVelocityBall)
                    ballsRb[i].velocity = new Vector2(maxVelocityBall, ballsRb[i].velocity.y);
                if (ballsRb[i].velocity.y > maxVelocityBall)
                    ballsRb[i].velocity = new Vector2(ballsRb[i].velocity.x, maxVelocityBall);
                if (ballsRb[i].velocity.y < minVelocityBall && ballsRb[i].velocity.y > 0)
                    ballsRb[i].velocity = new Vector2(ballsRb[i].velocity.x, minVelocityBall);
                if (ballsRb[i].velocity.x < minVelocityBall && ballsRb[i].velocity.x > 0)
                    ballsRb[i].velocity = new Vector2(minVelocityBall, ballsRb[i].velocity.y);
                if (ballsRb[i].velocity.y > -minVelocityBall && ballsRb[i].velocity.y < 0)
                    ballsRb[i].velocity = new Vector2(ballsRb[i].velocity.x, -minVelocityBall);
                if (ballsRb[i].velocity.x > -minVelocityBall && ballsRb[i].velocity.x < 0)
                    ballsRb[i].velocity = new Vector2(-minVelocityBall, ballsRb[i].velocity.y);
            }
           
        }
       
    }

    public static bool predicate() {
        return true;
    }

    public void ReturnInitially() {
        uiController.OffEndGamePanel();
        blocksCount = 0;
        levelsBlocks.RemoveAll(levelsBlocks => { Destroy(levelsBlocks); return true; });
        bonusController.GetBonusList().RemoveAll(bonusController=> { Destroy(bonusController); return true; });
        balls.RemoveAll(balls => { Destroy(balls); return true; });
        ballsRb.RemoveAll(balls => true);
        score = 0;
        playerC.transform.localPosition = new Vector3(0, -4.9f,0);
    }

    public void SubBlock() {
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
