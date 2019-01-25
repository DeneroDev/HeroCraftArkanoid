using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public void ResetPlayer(DataManager dataManager)
    {
        transform.localPosition = dataManager.StartPositionPlayer;
        transform.localScale = dataManager.SizePlayer;
    }




}
