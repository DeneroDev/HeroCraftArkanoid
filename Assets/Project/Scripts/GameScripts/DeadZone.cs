using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball")) {
            if (GameController.GetInstance().SetStateEnd(true))
                GameController.GetInstance().DeleteBalls(collision.gameObject);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Bonus")) {
            Destroy(collision.gameObject);
        }
           
    }
}
