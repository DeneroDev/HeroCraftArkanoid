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

    private List<Block> Blocks = new List<Block>();

    public List<Block> ImageGenerationLevel(int level) {
        Blocks.RemoveAll(block => { Destroy(block.gameObject); return true; });
        for (int i = 0; i <= 4; i++) {
            for (int j = 0; j <= 12; j++) {
                if (levelsMap[level].GetPixel(j, i) == Color.black) {
                    var blockGameobject = Instantiate(basicBlock, new Vector3((-6f + ((j) * basicBlock.transform.localScale.x)), (-1 + ((i) * basicBlock.transform.localScale.y))), Quaternion.identity, transform);
                    Block[] blocks = blockGameobject.GetComponentsInChildren<Block>();
                    for (int n = 0; n < blocks.Length; n++)
                        Blocks.Add(blocks[n]);
                }
                if (levelsMap[level].GetPixel(j, i) == Color.red) {
                    var blockGameobject = Instantiate(unbreakableBlock, new Vector3((-6f + ((j) * basicBlock.transform.localScale.x)), (-1 + ((i) * basicBlock.transform.localScale.y))), Quaternion.identity, transform);
                    Block[] blocks = blockGameobject.GetComponentsInChildren<Block>();
                    for (int n = 0; n < blocks.Length; n++)
                        Blocks.Add(blocks[n]);
                }
                if (levelsMap[level].GetPixel(j, i) == Color.magenta) {
                    var blockGameobject = Instantiate(repeatedlyBlock, new Vector3((-6f + ((j) * basicBlock.transform.localScale.x)), (-1 + ((i) * basicBlock.transform.localScale.y))), Quaternion.identity, transform);
                    Block[] blocks = blockGameobject.GetComponentsInChildren<Block>();
                    for (int n=0; n< blocks.Length; n++)
                        Blocks.Add(blocks[n]);
                }
            }
        }
        return Blocks;

    }

  
	
	
}
