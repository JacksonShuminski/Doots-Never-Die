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
    private float wobble = 0;

    //Timer/HP
    public float maxTime;
    public float timer; //Isn't going to be set in editor
    
    // Start is called before the first frame update
    //-------------------------------------------------------------------------------------------------------------
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        timer = maxTime;
    }

    // Update is called once per frame
    //-------------------------------------------------------------------------------------------------------------
    void FixedUpdate()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime; //Deceases the timer
        }
        
        moveAmount = Vector3.zero;
        currentPosition = transform.position;

        // Movement by keyboard inputs that adjust our move value
        if(Input.GetKey(KeyCode.A))
            moveAmount.x -= 1;
        if (Input.GetKey(KeyCode.D))
            moveAmount.x += 1;
        if (Input.GetKey(KeyCode.W))
            moveAmount.y += 1;
        if (Input.GetKey(KeyCode.S))
            moveAmount.y -= 1;
        
        moveAmount = moveAmount.normalized * speed;
        rigidbody.MovePosition(currentPosition + moveAmount * Time.deltaTime);

        // have the rotation of the skeleton wobble like he's walking
        if (moveAmount.magnitude > 0)
        {
            wobble += Time.deltaTime;
            transform.rotation = Quaternion.Euler(new Vector3(0,0,Mathf.Sin(wobble*35)*7));
        }
        else
        {
            transform.rotation = Quaternion.identity;
            wobble = 0;
        }

        // reverses the scale of the skeleton
        Vector3 newScale = transform.localScale;
        if (moveAmount.x > 0 && newScale.x < 0 || moveAmount.x < 0 && newScale.x > 0) {
            newScale.x *= -1;
        }
        transform.localScale = newScale;
    }

    /// <summary>
    /// Displays the timer. Uses default Unity UI(Temp) 
    /// </summary>
    private void OnGUI()
    {
        GUI.color = Color.white;
        GUI.skin.box.fontSize = 20;
        GUI.Box(new Rect(100, 340, 200, 100), timer + " seconds");
    }
}
