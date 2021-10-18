using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE : MonoBehaviour
{

    public CircleCollider2D circle;
    
    //Holds and sets timer
    private float timer;
    public float maxTime;
    private bool startTimer; //Be sure to have this updated and set wherever the input will be handled
    
    // Start is called before the first frame update
    void Start()
    {
        circle.enabled = false; //Or however colliders are deactivated
        timer = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                circle.enabled = false;
                timer = maxTime;
                startTimer = false;
            }
        }
    }

    /// <summary>
    /// When the AOE button is pressed, the trigger will activte for a short amount of time
    /// </summary>
    void ActivateTrigger()
    {
        circle.enabled = true;
        startTimer = true;
    }
}
