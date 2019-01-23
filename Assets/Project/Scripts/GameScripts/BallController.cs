using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {
    private Rigidbody2D rigidbody2d;


	void Update () {
        if(GameController.GetInstance().CurrentState == GameController.GameState.game)
            SpeedLimittedBall(GameController.GetInstance().MaxVelocityBall, GameController.GetInstance().MinVelocityBall);
    }

    public void ActivatedBall() {
        if (rigidbody2d == null)
            rigidbody2d = GetComponent<Rigidbody2D>();
        Debug.Log(GameController.GetInstance().StartVector);
       rigidbody2d.AddForce(GameController.GetInstance().StartVector* GameController.GetInstance().StartForce);
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

