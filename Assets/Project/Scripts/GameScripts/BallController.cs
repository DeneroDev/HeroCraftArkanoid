using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {
    private Rigidbody2D _rigidbody2d;


	void Update () {
        if(GameController.GetInstance().CurrentState == GameController.GameState.game)
            SpeedLimittedBall(GameController.GetInstance().MaxVelocityBall, GameController.GetInstance().MinVelocityBall);
    }

    public void ActivatedBall() {
        if (_rigidbody2d == null)
            _rigidbody2d = GetComponent<Rigidbody2D>();
        Debug.Log(GameController.GetInstance().StartVector);
       _rigidbody2d.AddForce(GameController.GetInstance().StartVector* GameController.GetInstance().StartForce);
    }

    public void SleepRb() {
        _rigidbody2d.Sleep();
    }

    private void SpeedLimittedBall(float maxVelocity, float minVelocity)
    {
                if (_rigidbody2d.velocity.x < -maxVelocity)
                    _rigidbody2d.velocity = new Vector2(-maxVelocity, _rigidbody2d.velocity.y);
                if (_rigidbody2d.velocity.y < -maxVelocity)
                    _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, -maxVelocity);
                if (_rigidbody2d.velocity.x > maxVelocity)
                    _rigidbody2d.velocity = new Vector2(maxVelocity, _rigidbody2d.velocity.y);
                if (_rigidbody2d.velocity.y > maxVelocity)
                    _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, maxVelocity);
                if (_rigidbody2d.velocity.y < minVelocity && _rigidbody2d.velocity.y > 0)
                    _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, minVelocity);
                if (_rigidbody2d.velocity.x < minVelocity && _rigidbody2d.velocity.x > 0)
                    _rigidbody2d.velocity = new Vector2(minVelocity, _rigidbody2d.velocity.y);
                if (_rigidbody2d.velocity.y > -minVelocity && _rigidbody2d.velocity.y < 0)
                    _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, -minVelocity);
                if (_rigidbody2d.velocity.x > -minVelocity && _rigidbody2d.velocity.x < 0)
                    _rigidbody2d.velocity = new Vector2(-minVelocity, _rigidbody2d.velocity.y);
        //Debug.Log("X:"+_rigidbody2d.velocity.x +"/ Y:" +_rigidbody2d.velocity.y);
    }

    }

