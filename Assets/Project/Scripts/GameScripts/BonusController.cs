using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour {
    public enum BonusState { multiplication, invulnerability, elongation }
    private List<GameObject> bonusList = new List<GameObject>();
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
                        ball = Instantiate(balls[i], balls[i].transform.position, Quaternion.identity);
                        break;
                    }
                }
                if (ball != null) {
                    ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(100, 300));
                    GameController.GetInstance().AddBall(ball);
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
}
