using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Base Variables
    private Rigidbody2D rigidbody;   // Body for movement
    private Vector3 currentPosition; //Position of the player
    public float speed;
    public Vector3 moveAmount = Vector3.zero;
    
    // Start is called before the first frame update
    //-------------------------------------------------------------------------------------------------------------
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    //-------------------------------------------------------------------------------------------------------------
    void FixedUpdate()
    {
        moveAmount = Vector3.zero;
        currentPosition = transform.position;

        // Movement by keyboard inputs that adjust our move value
        if(Input.GetKey(KeyCode.A))
        {
            moveAmount.x -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveAmount.x += 1;
        }

        if (Input.GetKey(KeyCode.W))
        {
            moveAmount.y += 1;
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            moveAmount.y -= 1;
        }
        moveAmount = moveAmount.normalized * speed;
        rigidbody.MovePosition(currentPosition + moveAmount * Time.deltaTime);
        Vector3 newScale = transform.localScale;
        if (moveAmount.x > 0 && newScale.x < 0 || moveAmount.x < 0 && newScale.x > 0) {
            newScale.x *= -1;
        }
        transform.localScale = newScale;
    }
}
