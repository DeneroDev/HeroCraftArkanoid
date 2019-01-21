using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingController : MonoBehaviour {

    private Rigidbody2D rigidbody2d;
    [Range(5,20)]
    [SerializeField]
    private float accelerationVelocity = 15;
    private readonly float border = 8.2f;
    private bool visible = false;

    public float Border
    {
        get
        {
            return border;
        }
    }


    // Use this for initialization
    void Start () {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (GameController.GetInstance().CurrentState == GameController.GameState.game && visible)
            rigidbody2d.velocity = new Vector2(Input.GetAxis("Mouse X") * accelerationVelocity, rigidbody2d.velocity.y);
        else
            rigidbody2d.velocity = new Vector2(0, 0);
    }



    private void OnBecameInvisible()
    {
        visible = false;
        if (transform.localPosition.x < 0)
            rigidbody2d.MovePosition(new Vector2(transform.localPosition.x+1, transform.localPosition.y));
        if (transform.localPosition.x > 0)
            rigidbody2d.MovePosition(new Vector2(transform.localPosition.x-1, transform.localPosition.y));
    }
    private void OnBecameVisible()
    {
        visible = true;
    }
}
