using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectElongation : MonoBehaviour,IEffect {

    private DataManager dataManager;
    public void Activate()
    {
        transform.localScale = dataManager.SizePlayerOnElongation;
        Invoke("Deactivate", dataManager.TimeElongation);
    }

    public void Deactivate()
    {
        transform.localScale = dataManager.SizePlayer;
        Destroy(this);
    }



    private void Awake()
    {
        dataManager = GameController.GetInstance().DataManager;
    }

    void Start ()
    {
        Activate();
    }
	

}
