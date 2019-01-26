using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectElongation : MonoBehaviour,IEffect {

    private GameSetting gameSetting;
    public void Activate()
    {
        transform.localScale = gameSetting.SizePlayerOnElongation;
        Invoke("Deactivate", gameSetting.TimeElongation);
    }

    public void Deactivate()
    {
        transform.localScale = gameSetting.SizePlayer;
        Destroy(this);
    }



    private void Awake()
    {
        gameSetting = GameController.GetInstance().GameSetting;
    }

    void Start ()
    {
        Activate();
    }
	

}
