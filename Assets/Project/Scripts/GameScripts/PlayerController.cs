using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public void ResetPlayer(GameSetting gameSetting)
    {
        transform.localPosition = gameSetting.StartPositionPlayer;
        transform.localScale = gameSetting.SizePlayer;
    }




}
