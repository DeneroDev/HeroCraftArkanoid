using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {
    private Rigidbody2D rigidbody2d;


    void Update () {
        if(GameController.GetInstance().CurrentState == GameController.GameState.game)
            SpeedLimittedBall(GameController.GetInstance().GameSetting.maxVelocityBall, GameController.GetInstance().GameSetting.minVelocityBall);
    }

    public void ActivatedBall() {
        if (rigidbody2d == null)
            rigidbody2d = GetComponent<Rigidbody2D>();
       rigidbody2d.AddForce(GameController.GetInstance().GameSetting.startVector* GameController.GetInstance().GameSetting.startForce);
    }

    public void SleepRb() {
        rigidbody2d.Sleep();
    }

    private void SpeedLimittedBall(float maxVelocity, float minVelocity)
    {
        float i = (rigidbody2d.velocity.x / Mathf.Abs(rigidbody2d.velocity.x));
        float j = (rigidbody2d.velocity.y / Mathf.Abs(rigidbody2d.velocity.y));
        rigidbody2d.velocity = new Vector2(Mathf.Clamp(rigidbody2d.velocity.x, i*minVelocity, i * maxVelocity), Mathf.Clamp(rigidbody2d.velocity.y, j * minVelocity, j * maxVelocity));
           
        Debug.Log("X:"+rigidbody2d.velocity.x +"/ Y:" +rigidbody2d.velocity.y);
    }

    }

