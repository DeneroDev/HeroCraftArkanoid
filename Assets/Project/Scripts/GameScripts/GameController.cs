using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour {
    private static GameController instance;
    public enum GameState { menu,pregame,game,postgame,pause,end }
    private GameState currentState = GameState.menu;
    [Header("StartConfig")]
    [SerializeField]
    [Range(100, 1000)]
    private float startForce = 500;
    [SerializeField]
    private Vector2 startVector;
    [Range(3, 7)]
    [SerializeField]
    private float maxVelocityBall = 10;
    [Range(1, 5)]
    [SerializeField]
    private float minVelocityBall = 3;
    [Header("GameComponents")]
    [SerializeField]
    private GameObject ballPrefab;

    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private LevelGeneration levelGeneration;
    [SerializeField]
    private SoundController soundController;
    public enum BonusState { multiplication, invulnerability, elongation }
    private List<GameObject> bonusList = new List<GameObject>();
    [Header("BonusComponent")]
    [SerializeField]
    private GameObject BonusMultiplication;
    [SerializeField]
    private GameObject BonusInvulnerability;
    [SerializeField]
    private GameObject BonusElongation;
    public GameState CurrentState { get {return currentState;} set {currentState = value;}}
    public float MinVelocityBall{get { return minVelocityBall;} set{minVelocityBall = value;}}
    public float MaxVelocityBall{get{return maxVelocityBall;}set{maxVelocityBall = value;}}
    public float StartForce { get {return startForce;} set {startForce = value;} }
    public Vector2 StartVector { get { return startVector;} set {startVector = value;}}
    private int score = 0;
    private int blocksCount;
    private int currentLevel = 1;
    private List<Block> levelsBlocks;
    private List<GameObject> balls = new List<GameObject>();
    private GameState previusPauseState;
    [Header("UpdateUIMethods")]
    public UnityEvent updateScoreUI;
    public UnityEvent updateUI;

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

    public void SetCurrentLevel(int level)
    {
        currentLevel = level;
    }

    public void SetStatePreGame() {
        GenerationLevel();
        currentState = GameState.pregame;
        updateUI.Invoke();
    }

    public void SetStatePostGame()
    {
        currentState = GameState.postgame;
        updateUI.Invoke();
    }

    public void SetStateGame()
    {
        currentState = GameState.game;
        updateUI.Invoke();
        var ball = Instantiate(ballPrefab, new Vector3(0, -4.6f, 0), Quaternion.identity);
        AddBall(ball);
        ball.GetComponent<BallController>().ActivatedBall();
    }

    public void SetPauseState()
    {
        CurrentState = GameState.pause;
        updateUI.Invoke();
    }

    public void BackFromPause()
    {
        CurrentState = GameState.game;
        updateUI.Invoke();
    }

    public void SetStateMenu()
    {
        CurrentState = GameState.menu;
        updateUI.Invoke();
    }

    public void SetStateEnd()
    {
        if (balls.Count <= 0)
        {
            currentState = GameState.postgame;
            updateUI.Invoke();
        }
        else
        {
            currentState = GameState.postgame;
            updateUI.Invoke();
            for (int i = 0; i < balls.Count; i++)
            {
                if (balls != null)
                    balls[i].GetComponent<BallController>().SleepRb();
            }
        }
    }

    public void AddScore(int score)
    {
        this.score += score;
        updateScoreUI.Invoke();
    }

    public void GenerationLevel() {
        levelsBlocks = levelGeneration.ImageGenerationLevel(currentLevel);
        blocksCount = 0;
        for (int i = 0; i < levelsBlocks.Count; i++) {
            if(levelsBlocks[i].GetCurrentType()!=Block.BlockType.unbreakable)
                blocksCount++;
        }
    }
    

 

    public void AddBall(GameObject ball) {
        balls.Add(ball);
    }

    public int GetBallsCount() {
        return balls.Count;
    }

    public void DeleteBall(GameObject ball) {
        balls.Remove(ball);
        Destroy(ball);
        if(balls.Count<=0)
            SetStateEnd();
    }

   
    public void ReturnInitially() {
        // uiController.OffEndGamePanel();
        // uiController.ScoreUpdate(0);
        score = 0;
        updateScoreUI.Invoke();
        levelsBlocks.RemoveAll(levelsBlocks => { if(levelsBlocks!=null) Destroy(levelsBlocks.gameObject); return true; });
        bonusList.RemoveAll(bonusItem => { Destroy(bonusItem); return true; });
        balls.RemoveAll(balls => { Destroy(balls); return true; });

        playerController.transform.localPosition = new Vector3(0, -4.9f,0);
    }




    public void SubtractionBlock() {
        blocksCount--;
        soundController.PlaySoundBreakBlock();
        if (blocksCount <= 0)
            SetStateEnd();
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
            CurrentState = GameState.menu;
            updateUI.Invoke();
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

    public void CloseApplication() {
        Application.Quit();
    }

    #region Bonus
    public void BonusDetermination(BonusItem bonus)
    {
        switch (bonus.GetState())
        {
            case BonusState.elongation:
                ActivateBonusElongation();
                break;
            case BonusState.invulnerability:
                ActivateBonusInvulnerability();
                break;
            case BonusState.multiplication:
                ActivateBonusMultiplication();
                break;
        }
    }

    public void ActivateBonusMultiplication()
    {
        GameObject ball = null;
        for (int i = 0; i < balls.Count; i++)
        {
            if (balls[i] != null)
            {
                ball = balls[i].gameObject;
                break;
            }
        }
        if (ball != null)
        {
            var newBall = Instantiate(ballPrefab, ball.transform.position, Quaternion.identity);
            newBall.GetComponent<BallController>().ActivatedBall();
            AddBall(newBall);
        }
    }

    public void ActivateBonusElongation()
    {
        playerController.gameObject.AddComponent<EffectElongation>();
    }

    public void ActivateBonusInvulnerability()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            if (balls[i] != null)
            {
                balls[i].AddComponent<EffectInvulnerability>();
            }
        }
    }



    public void AddBonusItemToList(GameObject bonus)
    {
        bonusList.Add(bonus);
    }

    public void CheckBonus(Vector3 SpawnPosition)
    {
        var chanceBonus = Random.Range(0, 100.99f);
        if (chanceBonus > (100 - Data.BONUS_CHANCE_PRECENT_ELONGATION))
        {
            if (chanceBonus > (100 - Data.BONUS_CHANCE_PRECENT_MULTIPLICATION))
                if (chanceBonus > (100 - Data.BONUS_CHANCE_PRECENT_INVULNEARBILITY))
                    AddBonusItemToList(Instantiate(BonusInvulnerability, SpawnPosition, Quaternion.identity));
                else
                    AddBonusItemToList(Instantiate(BonusMultiplication, SpawnPosition, Quaternion.identity));
            else
                AddBonusItemToList(Instantiate(BonusElongation, SpawnPosition, Quaternion.identity));
        }
    }
    #endregion
}
