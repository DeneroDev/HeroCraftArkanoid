using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour {
    public enum BonusState { multiplication, invulnerability, elongation }
    private List<GameObject> bonusList = new List<GameObject>();
    [SerializeField]
    private GameObject BonusMultiplication;
    [SerializeField]
    private GameObject BonusInvulnerability;
    [SerializeField]
    private GameObject BonusElongation;

    public void BonusActivated(BonusState bonus)
    {
        var balls = GameController.GetInstance().GetBallsList();
        switch (bonus)
        {
            case BonusState.invulnerability:
                for (int i = 0; i < GameController.GetInstance().GetBallsCount(); i++) {
                    if (balls[i] != null) {
                        balls[i].transform.GetChild(1).gameObject.SetActive(true);
                        balls[i].transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                    } 
                }
                Invoke("DeactivatedInvulnerabilityBonus", 1.5f);
                break;
            case BonusState.multiplication:
                GameObject ball = null;
                for (int i = 0; i < GameController.GetInstance().GetBallsCount(); i++) {
                    if (balls[i] != null) {
                        ball = balls[i];
                        break;
                    }
                }
                if (ball != null) {
                    var newBall = Instantiate(ball, ball.transform.position, Quaternion.identity);
                    newBall.GetComponent<BallController>().ActivatedBall();
                    GameController.GetInstance().AddBall(newBall);
                }
                break;
            case BonusState.elongation:
                GameController.GetInstance().GetPlayerController().ActivatedElongationBonus();
                break;
        }
    }

    public void DeactivatedInvulnerabilityBonus()
    {
        var balls = GameController.GetInstance().GetBallsList();
        for (int i = 0; i < balls.Count; i++) {
            if (balls[i] != null) {
                balls[i].transform.GetChild(1).gameObject.SetActive(false);
                balls[i].transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            }
        }
           
    }

    public void AddBonusItemToList(GameObject bonus) {
        bonusList.Add(bonus);
    }

    public List<GameObject> GetBonusList() {
        return bonusList;
    }

    public void CheckBonus(Vector3 SpawnPosition)
    {
        var chanceBonus = Random.Range(0, 100.99f);
        if (chanceBonus > (100 - Data.BONUS_CHANCE_PRECENT_ELONGATION))
        {
            if (chanceBonus > (100 - Data.BONUS_CHANCE_PRECENT_MULTIPLICATION))
                if (chanceBonus > (100 - Data.BONUS_CHANCE_PRECENT_INVULNEARBILITY))
                    AddBonusItemToList(Instantiate(BonusInvulnerability, SpawnPosition, Quaternion.identity));
                else
                    AddBonusItemToList(Instantiate(BonusMultiplication, SpawnPosition, Quaternion.identity));
            else
                AddBonusItemToList(Instantiate(BonusElongation, SpawnPosition, Quaternion.identity));
        }
    }
}
