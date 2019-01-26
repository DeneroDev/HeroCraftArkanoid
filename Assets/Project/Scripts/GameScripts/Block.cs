using UnityEngine;

public class Block : MonoBehaviour
{
    public enum BlockType { basic, unbreakable, repeatedly }
    [SerializeField]
    private BlockType currentType;
    [SerializeField]
    private GameObject effectDestroy;
    [SerializeField]
    private TextMesh textHealth;
    private int Health;
    private bool Destroying = false;

    private void Start()
    {
        if (currentType == BlockType.repeatedly)
        {
            Health = Random.Range(1, 3);
            textHealth.text = Health.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball") && !Destroying)
        {
            var gameSetting = GameController.GetInstance().GameSetting;
            switch (currentType)
            {
                case BlockType.basic:
                    DestroyBlock(gameSetting.PointForBasicBlock, false);
                    break;
                case BlockType.repeatedly:
                    Health -= 1;
                    if (Health <= 0)
                        DestroyBlock(gameSetting.PointForRepeatedlyBlock, false);
                    else
                    {
                        textHealth.text = Health.ToString();
                        GameController.GetInstance().DataManager.AddScore(gameSetting.PointForRepeatedlyBlock/2);
                    }
                    break;
                case BlockType.unbreakable: break;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("BallInvulnerability") && !Destroying)
        {
            var gameSetting = GameController.GetInstance().GameSetting;
            switch (currentType)
            {
                case BlockType.basic:
                    DestroyBlock(gameSetting.PointForBasicBlock, false);
                    break;
                case BlockType.repeatedly:
                    DestroyBlock(gameSetting.PointForRepeatedlyBlock, false);
                    break;
                case BlockType.unbreakable:
                    DestroyBlock(gameSetting.PointForBasicBlock, true);
                    break;
            }
        }
    }



    private void DestroyBlock(int score, bool ignoreSubtraction)
    {
        Destroying = true;
        Instantiate(effectDestroy, transform.position, Quaternion.identity);
        GameController.GetInstance().DataManager.AddScore(score);
        if (!ignoreSubtraction)
            GameController.GetInstance().LevelGeneration.SubtractionBlock();
        CheckBonus(transform.position);
        Destroy(gameObject);
    }

    public BlockType GetCurrentType() {
        return currentType;
    }

    public void CheckBonus(Vector3 SpawnPosition)
    {
        var gameSetting = GameController.GetInstance().GameSetting;
        var chanceBonus = Random.Range(0, 100.99f);
        var parent = GameController.GetInstance().LevelGeneration.transform;
        if (chanceBonus > (100 - gameSetting.BonusChancePrecentEllongation))
        {
            if (chanceBonus > (100 - gameSetting.BonusChancePrecentMultiplication))
                if (chanceBonus > (100 - gameSetting.BonusChancePrecentInvulnerability))
                    Instantiate(Resources.Load("BonusInvulnerability"), SpawnPosition, Quaternion.identity,parent);
                else
                    Instantiate(Resources.Load("BonusMultiplication"), SpawnPosition, Quaternion.identity,parent);
            else
                Instantiate(Resources.Load("BonusElongation"), SpawnPosition, Quaternion.identity,parent);
        }
    }

}
