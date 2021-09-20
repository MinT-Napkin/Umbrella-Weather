using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    private float moveVelocity;

    // Update is called once per frame
    void Update()
    {
        moveVelocity = 0;

        //Left Right Movement
        if (Input.GetKey (KeyCode.A)) 
        {
            moveVelocity = -walkSpeed;
        }
        if (Input.GetKey (KeyCode.D)) 
        {
            moveVelocity = walkSpeed;
        }

         GetComponent<Rigidbody2D>().velocity = new Vector2 (moveVelocity, GetComponent<Rigidbody2D> ().velocity.y);
    }
}
