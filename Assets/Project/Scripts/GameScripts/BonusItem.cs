using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusItem : MonoBehaviour {
    [SerializeField]
    private BonusController.BonusState bonusState;

    public BonusController.BonusState GetState() {
        return bonusState;
    }
}
