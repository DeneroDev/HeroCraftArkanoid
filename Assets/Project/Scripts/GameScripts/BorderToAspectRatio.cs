using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderToAspectRatio : MonoBehaviour {
    [SerializeField]
    private Transform leftWall;
    [SerializeField]
    private Transform rightWall;
    // Use this for initialization
    void Start () {
        string AspectRatio = System.Math.Round((float)Screen.width / Screen.height,2).ToString();
        switch (AspectRatio) {
            case "1.25":
                rightWall.transform.localPosition = new Vector3(-7.85f, leftWall.transform.localPosition.y, 0);
                leftWall.transform.localPosition = new Vector3(7.85f, leftWall.transform.localPosition.y, 0);
                break;
            case "1.33":
                rightWall.transform.localPosition = new Vector3(-8.25f, leftWall.transform.localPosition.y, 0);
                leftWall.transform.localPosition = new Vector3(8.25f, leftWall.transform.localPosition.y, 0);
                break;
            case "1.5":
                rightWall.transform.localPosition = new Vector3(-9.1f, leftWall.transform.localPosition.y, 0);
                leftWall.transform.localPosition = new Vector3(9.1f, leftWall.transform.localPosition.y, 0);
                break;
            case "1.6":
                rightWall.transform.localPosition = new Vector3(-9.6f, leftWall.transform.localPosition.y, 0);
                leftWall.transform.localPosition = new Vector3(9.6f, leftWall.transform.localPosition.y, 0);
                break;
            case "1.78":
                rightWall.transform.localPosition = new Vector3(-10.5f, leftWall.transform.localPosition.y, 0);
                leftWall.transform.localPosition = new Vector3(10.5f, leftWall.transform.localPosition.y, 0);
                break;
            default:
                rightWall.transform.localPosition = new Vector3(-7.85f, leftWall.transform.localPosition.y, 0);
                leftWall.transform.localPosition = new Vector3(7.85f, leftWall.transform.localPosition.y, 0);
                break;
        }
	}
	

}
