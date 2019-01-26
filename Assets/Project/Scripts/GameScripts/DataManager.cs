using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataManager:MonoBehaviour {
   
    private int score = 0;

    public UnityEvent EventUpdateScoreUI;

    public void AddScore(int score)
    {
        this.score += score;
        if(EventUpdateScoreUI!=null)
            EventUpdateScoreUI.Invoke();
    }

    public void RefreshScore() {
        score = 0;
        if (EventUpdateScoreUI != null)
            EventUpdateScoreUI.Invoke();
    }

    public int GetScore()
    {
        return score;
    }
}
