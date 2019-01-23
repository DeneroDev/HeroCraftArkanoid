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


    public void BonusDetermination(BonusItem bonus) {
        switch (bonus.GetState())
        {
            case BonusState.elongation:
                ActivateBonusElongation(gameObject);
                break;
            case BonusState.invulnerability:
                ActivateBonusInvulnerability();
                break;
            case BonusState.multiplication:
                ActivateBonusMultiplication();
                break;
        }
    }

    public void ActivateBonusMultiplication()
    {
        GameController.GetInstance().AddNewBall();
    }

    public void ActivateBonusElongation(GameObject player)
    {
        player.AddComponent<EffectElongation>();
    }

    public void ActivateBonusInvulnerability()
    {
        var balls = GameController.GetInstance().GetBallsList();
        for (int i = 0; i < balls.Count; i++)
        {
            if (balls[i] != null)
            {
                balls[i].AddComponent<EffectInvulnerability>();
            }
        }
    }



    public void AddBonusItemToList(GameObject bonus) {
        bonusList.Add(bonus);
    }

    public List<GameObject> GetBonusList() {
        return bonusList;
    }

    public void RemoveAllBonus() {
        bonusList.RemoveAll(bonusController => { Destroy(bonusController); return true; });
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
