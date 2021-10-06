using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private BoxCollider2D collider;
    private Vector3 currentPosition;
    private Vector3 previousPosition;
    private Vector3 moveAmount;
    private bool scared;
    private int scareDuration;
    public Player player;
    
    public int timer;
    public int hp;
    public int speed;
    // Start is called before the first frame update
    void Start()
    {
        scared = false;
        timer = 0;
        rigidBody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        moveAmount = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

        //I will need to know how projectiles are done to make this work
        /*
        for(int i = 0; i < projectiles.count; i++)
        {
            CheckForDamage(projectiles[i]);
        }
        */

        //if the child has been scared for 5 seconds, it is no longer scared
        if(scareDuration > 300)
        {
            scared = false;
            scareDuration = 0;
        }

        //moving directly away from the player
        if(scared == true)
        {
            moveAmount = new Vector3(-player.transform.position.x + currentPosition.x, 
                                     -player.transform.position.y + currentPosition.y, 0);
        }
        
        //moving towards the player
        else if(Vector3.Distance(player.transform.position, currentPosition) < 5)
        {
            moveAmount = new Vector3(player.transform.position.x - currentPosition.x, 
                                     player.transform.position.y - currentPosition.y, 0);
        }

        //randomized wandering
        else if (timer == 0)
        {
            moveAmount = new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), 0);
        }

        //updating timers
        timer++;
        scareDuration++;
        //if two seconds have passed - change directions
        if(timer > 120)
        {
            timer = 0;
        }

        //where the object was last frame
        previousPosition = currentPosition;

        //actually moving the object
        moveAmount = moveAmount.normalized * Time.deltaTime * speed;
        rigidBody.MovePosition(currentPosition + moveAmount);

        //updating where the object is located
        currentPosition = transform.position;

        //need to check for boundaries of the world and adjust position
        bool collisionCheck = checkForCollisions();

        if (collisionCheck)
        {
            rigidBody.MovePosition(currentPosition);
        }
    }

    /// <summary>
    /// will need to know some info to finish up this one - how are we handling scares?
    /// </summary>
    bool checkForCollisions()
    {
        if(currentPosition.x > 400)
        {
            currentPosition.x = previousPosition.x;
            return true;
        }
        if(currentPosition.x < -400)
        {
            currentPosition.x = previousPosition.x;
            return true;
        }
        if(currentPosition.y > 400)
        {
            currentPosition.y = previousPosition.y;
            return true;
        }
        if(currentPosition.y < -400)
        {
            currentPosition.y = previousPosition.y;
            return true;
        }
        return false;
    }


    void CheckForDamage(Collider2D attack)
    {
        if (collider.IsTouching(attack))
        {
            hp -= 10;
            scared = true;
            scareDuration = 0;
        }
    }
}
