using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectElongation : MonoBehaviour,IEffect {


    public void Activate()
    {
        transform.localScale = new Vector3(3, 0.2f, 0);
        Invoke("Deactivate", 5);
    }

    public void Deactivate()
    {
        transform.localScale = new Vector3(1.5f, 0.2f, 0);
        Destroy(this);
    }


    void Start () {
        Activate();
    }
	

}
