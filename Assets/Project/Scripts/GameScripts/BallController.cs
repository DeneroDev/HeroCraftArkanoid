using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {
    private Rigidbody2D rigidbody2d;
    private DataManager dataManager;

    private void Awake()
    {
        dataManager = GameController.GetInstance().DataManager;
    }

    void Update () {
        if(GameController.GetInstance().CurrentState == GameController.GameState.game)
            SpeedLimittedBall(dataManager.maxVelocityBall, dataManager.minVelocityBall);
    }

    public void ActivatedBall() {
        if (rigidbody2d == null)
            rigidbody2d = GetComponent<Rigidbody2D>();
       rigidbody2d.AddForce(dataManager.startVector* dataManager.startForce);
    }

    public void SleepRb() {
        rigidbody2d.Sleep();
    }

    private void SpeedLimittedBall(float maxVelocity, float minVelocity)
    {
                if (rigidbody2d.velocity.x < -maxVelocity)
                    rigidbody2d.velocity = new Vector2(-maxVelocity, rigidbody2d.velocity.y);
                if (rigidbody2d.velocity.y < -maxVelocity)
                    rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, -maxVelocity);
                if (rigidbody2d.velocity.x > maxVelocity)
                    rigidbody2d.velocity = new Vector2(maxVelocity, rigidbody2d.velocity.y);
                if (rigidbody2d.velocity.y > maxVelocity)
                    rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, maxVelocity);
                if (rigidbody2d.velocity.y < minVelocity && rigidbody2d.velocity.y > 0)
                    rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, minVelocity);
                if (rigidbody2d.velocity.x < minVelocity && rigidbody2d.velocity.x > 0)
                    rigidbody2d.velocity = new Vector2(minVelocity, rigidbody2d.velocity.y);
                if (rigidbody2d.velocity.y > -minVelocity && rigidbody2d.velocity.y < 0)
                    rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, -minVelocity);
                if (rigidbody2d.velocity.x > -minVelocity && rigidbody2d.velocity.x < 0)
                    rigidbody2d.velocity = new Vector2(-minVelocity, rigidbody2d.velocity.y);
        //Debug.Log("X:"+rigidbody2d.velocity.x +"/ Y:" +rigidbody2d.velocity.y);
    }

    }

