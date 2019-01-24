using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusItem : MonoBehaviour {
    [SerializeField]
    private GameController.BonusState bonusState;

    public GameController.BonusState GetState() {
        return bonusState;
    }
}
