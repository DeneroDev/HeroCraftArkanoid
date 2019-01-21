using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bonus")) {
            var bonus = collision.gameObject.GetComponent<BonusItem>();
            GameController.GetInstance().GetBonusController().BonusActivated(bonus.GetState());
            Destroy(collision.gameObject);
        }
    }

    public void ActivatedElongationBonus() {
        transform.localScale = new Vector3(3, 0.2f, 0);
        Invoke("DeactivatedElongationBonus", 5);
    }

    public void DeactivatedElongationBonus() {
        transform.localScale = new Vector3(1.5f, 0.2f, 0);
    }
}
