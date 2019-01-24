using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bonus")) {
            var bonus = collision.gameObject.GetComponent<BonusItem>();
            GameController.GetInstance().BonusDetermination(bonus);
            Destroy(collision.gameObject);
        }
    }

 
}
