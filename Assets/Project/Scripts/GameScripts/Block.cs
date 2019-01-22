using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private enum BlockType { basic, unbreakable, repeatedly }
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
            switch (currentType)
            {
                case BlockType.basic:
                    DestroyBlock(Data.POINT_FOR_BASIC_BLOCK, false);
                    break;
                case BlockType.repeatedly:
                    Health -= 1;
                    if (Health <= 0)
                        DestroyBlock(Data.POINT_FOR_REPEATEDLY_BLOCK, false);
                    else
                    {
                        textHealth.text = Health.ToString();
                        GameController.GetInstance().AddScore(25);
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
            switch (currentType)
            {
                case BlockType.basic:
                    DestroyBlock(Data.POINT_FOR_BASIC_BLOCK, false);
                    break;
                case BlockType.repeatedly:
                    DestroyBlock(Data.POINT_FOR_REPEATEDLY_BLOCK, false);
                    break;
                case BlockType.unbreakable:
                    DestroyBlock(Data.POINT_FOR_BASIC_BLOCK, true);
                    break;
            }
        }
    }



    private void DestroyBlock(int score, bool ignoreSubtraction)
    {
        Destroying = true;
        Instantiate(effectDestroy, transform.position, Quaternion.identity);
        GameController.GetInstance().AddScore(score);
        if (!ignoreSubtraction)
            GameController.GetInstance().SubtractionBlock();
        GameController.GetInstance().GetBonusController().CheckBonus(transform.position);
        Destroy(gameObject);
    }


 
}
