using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelGeneration : MonoBehaviour {
    [SerializeField]
    private GameObject basicBlock;
    [SerializeField]
    private GameObject unbreakableBlock;
    [SerializeField]
    private GameObject repeatedlyBlock;
    [SerializeField]
    private List<Texture2D> levelsMap;
    public int blocksCount = 0;
    public UnityEvent EventCheckEndGame;

    public void ImageGenerationLevel(int level) {
        for (int i = 0; i <= 4; i++) {
            for (int j = 0; j <= 12; j++) {
                if (levelsMap[level].GetPixel(j, i) == Color.black) {
                    Instantiate(basicBlock, new Vector3((-6f + ((j) * basicBlock.transform.localScale.x)), (-1 + ((i) * basicBlock.transform.localScale.y))), Quaternion.identity, transform);
                    blocksCount += 2;
                }
                if (levelsMap[level].GetPixel(j, i) == Color.red) {
                    Instantiate(unbreakableBlock, new Vector3((-6f + ((j) * basicBlock.transform.localScale.x)), (-1 + ((i) * basicBlock.transform.localScale.y))), Quaternion.identity, transform);
                }
                if (levelsMap[level].GetPixel(j, i) == Color.magenta) {
                    Instantiate(repeatedlyBlock, new Vector3((-6f + ((j) * basicBlock.transform.localScale.x)), (-1 + ((i) * basicBlock.transform.localScale.y))), Quaternion.identity, transform);
                    blocksCount += 2;
                }
            }
        }
    }


    public void SubtractionBlock()
    {
        blocksCount--;
        if (EventCheckEndGame != null)
            EventCheckEndGame.Invoke();
        if(GameController.GetInstance()!=null)
            GameController.GetInstance().SoundController.PlaySoundBreakBlock();
    }


    public void ClearLevel() {
        blocksCount = 0;
        for (int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

}
