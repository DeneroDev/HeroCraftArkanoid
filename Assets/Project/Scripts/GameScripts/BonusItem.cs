using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusItem : MonoBehaviour {
    [SerializeField]
    private BonusController.BonusState bonusState;

    public BonusController.BonusState GetState() {
        return bonusState;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameController.GetInstance().BonusController.BonusDetermination(this);
            Destroy(gameObject);
        }
    }
}
