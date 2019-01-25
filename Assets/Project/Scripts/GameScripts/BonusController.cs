using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour {
    public enum BonusState { multiplication, invulnerability, elongation }
    [SerializeField]
    private GameObject BonusMultiplication;
    [SerializeField]
    private GameObject BonusInvulnerability;
    [SerializeField]
    private GameObject BonusElongation;

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
        var balls = GameController.GetInstance().BallManager.Balls;
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
            var newBall = (GameObject)Instantiate(Resources.Load("Ball"), ball.transform.position, Quaternion.identity);
            newBall.GetComponent<BallController>().ActivatedBall();
            GameController.GetInstance().BallManager.AddBall(newBall);
        }
    }

    public void ActivateBonusElongation()
    {
        var player = GameController.GetInstance().PlayerController.gameObject;
        player.AddComponent<EffectElongation>();
    }

    public void ActivateBonusInvulnerability()
    {
        var balls = GameController.GetInstance().BallManager.Balls;
        for (int i = 0; i < balls.Count; i++)
        {
            if (balls[i] != null)
            {
                balls[i].AddComponent<EffectInvulnerability>();
            }
        }
    }



}
