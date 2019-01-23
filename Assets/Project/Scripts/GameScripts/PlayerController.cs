using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private BonusController bonusController;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bonus")) {
            var bonus = collision.gameObject.GetComponent<BonusItem>();
            bonusController.BonusDetermination(bonus);
            Destroy(collision.gameObject);
        }
    }

 
}
