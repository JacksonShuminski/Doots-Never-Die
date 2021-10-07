using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : MonoBehaviour
{
    // Test Comment
    private Rigidbody2D rigidBody;
    private BoxCollider2D collider;
    private Vector3 currentPosition;
    private Vector3 previousPosition;
    private Vector3 moveAmount;
    private bool scared;
    private int scareDuration;
    public Player player;
    public Aim aim;
    private int timer;

    public int hp;
    public int speed;
    // Start is called before the first frame update
    void Start()
    {
        scared = false;
        timer = 0;
        hp = 40;
        rigidBody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        moveAmount = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

        //I will need to know how projectiles are done to make this work
        if (aim.projectileList.Count > 0)
        {
            for (int i = 0; i < aim.projectileList.Count; i++)
            {
                CheckForDamage(aim.projectileList[i].GetComponent<BoxCollider2D>());
            }
        }

        //if the child is too scared, they leave the game
        if(hp <= 0)
        {
            Destroy(gameObject);
            return;
        }

        //if the child has been scared for 5 seconds, it is no longer scared. A projectile resets this counter
        //also, the counter will reset on its own in 5 seconds so that the int doesn't increase
        //to infinity and cause memory issues
        if(scareDuration > 300)
        {
            scared = false;
            scareDuration = 0;
        }

        //moving directly away from the player if scared
        if(scared == true)
        {
            moveAmount = new Vector3(-player.transform.position.x + currentPosition.x, 
                                     -player.transform.position.y + currentPosition.y, 0);
        }
        
        //moving towards the player if they get close
        else if(Vector3.Distance(player.transform.position, currentPosition) < 5)
        {
            moveAmount = new Vector3(player.transform.position.x - currentPosition.x, 
                                     player.transform.position.y - currentPosition.y, 0);
        }

        //randomized wandering otherwise
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

    }

    void CheckForDamage(Collider2D attack)
    {
        if (collider.IsTouching(attack))
        {
            aim.projectileList.Remove(attack.gameObject);
            Destroy(attack.gameObject);
            hp -= 10;
            scared = true;
            scareDuration = 0;
        }
    }
}
