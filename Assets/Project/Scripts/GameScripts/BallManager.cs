using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallManager : MonoBehaviour {
    private List<GameObject> balls = new List<GameObject>();
    public List<GameObject> Balls { get { return balls; } }
    [SerializeField]
    private GameObject ballPrefab;
    public UnityEvent EventCheckEndGame;


    public void AddBall(GameObject ball)
    {
        Balls.Add(ball);
    }

    public int GetBallsCount()
    {
        return Balls.Count;
    }

    public void RemoveAllBall() {
            balls.RemoveAll(ball=> { Destroy(ball); return true; });
    }

    public void DeleteBall(GameObject ball)
    {
        balls.Remove(ball);
        Destroy(ball);
        if (EventCheckEndGame != null)
            EventCheckEndGame.Invoke();
    }

    public void SleepAllBalls() {
        for (int i = 0; i < balls.Count; i++)
        {
            if (balls[i] != null)
                balls[i].GetComponent<BallController>().SleepRb();
        }
    }

    public void ActivateAllBalls()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            if (balls[i] != null)
                balls[i].GetComponent<BallController>().ActivatedBall();
        }
    }

    public void CreateBall() {
        AddBall(Instantiate(ballPrefab, GameController.GetInstance().GameSetting.SpawnPositionBall, Quaternion.identity));
    }


}
