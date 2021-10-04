using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Vector3 currentPosition;
    private Vector3 moveAmount;
    public int timer;
    public int hp;
    public int speed;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        rigidBody = GetComponent<Rigidbody2D>();
        moveAmount = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = transform.position;

        //randomized wandering
        if(timer == 0)
        {
            moveAmount = new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), 0);
        }

        timer++;

        //if two seconds have passed - change directions
        if(timer > 120)
        {
            timer = 0;
        }

        //actually moving the object
        moveAmount = moveAmount.normalized * Time.deltaTime * speed;
        rigidBody.MovePosition(currentPosition + moveAmount);

        //need to check for boundaries of the world and adjust position
    }

    /// <summary>
    /// will need to know some info to finish up this one - how are we handling scares?
    /// </summary>
    void checkForCollisions()
    {

    }
}
