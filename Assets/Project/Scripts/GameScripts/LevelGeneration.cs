using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour {
    [SerializeField]
    private GameObject basicBlock;
    [SerializeField]
    private GameObject unbreakableBlock;
    [SerializeField]
    private GameObject repeatedlyBlock;
    [SerializeField]
    private List<Texture2D> levelsMap;



    public void ImageGenerationLevel(int level) {
        for (int i = 0; i <= 4; i++) {
            for (int j = 0; j <= 12; j++) {
                if (levelsMap[level].GetPixel(j, i) == Color.black) {
                    var block = Instantiate(basicBlock, new Vector3((-6f + ((j) * basicBlock.transform.localScale.x)), (-1 + ((i) * basicBlock.transform.localScale.y))), Quaternion.identity, transform);
                    GameController.GetInstance().AddBlock(block,false);
                }
                if (levelsMap[level].GetPixel(j, i) == Color.red) {
                    var block = Instantiate(unbreakableBlock, new Vector3((-6f + ((j) * basicBlock.transform.localScale.x)), (-1 + ((i) * basicBlock.transform.localScale.y))), Quaternion.identity, transform);
                    GameController.GetInstance().AddBlock(block,true);
                }
                if (levelsMap[level].GetPixel(j, i) == Color.magenta) {
                    var block = Instantiate(repeatedlyBlock, new Vector3((-6f + ((j) * basicBlock.transform.localScale.x)), (-1 + ((i) * basicBlock.transform.localScale.y))), Quaternion.identity, transform);
                    GameController.GetInstance().AddBlock(block,false);
                   
                }
            }
        }
    }

  
	
	
}
