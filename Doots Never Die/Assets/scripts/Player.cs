using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Code for movement
    private Rigidbody2D rigidbody;
    private Vector3 currentPosition; //Position of the player
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = transform.position;
        Vector3 moveAmount = Vector3.zero;

        //Movement
        if(Input.GetKey(KeyCode.A))
        {
            moveAmount = new Vector3(-1, 0, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveAmount = new Vector3(1, 0, 0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            moveAmount = new Vector3(0, 1, 0);
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            moveAmount = new Vector3(0, -1, 0);
        }

        moveAmount = moveAmount.normalized * Time.deltaTime * speed;
        rigidbody.MovePosition(currentPosition + moveAmount);

    }
}
