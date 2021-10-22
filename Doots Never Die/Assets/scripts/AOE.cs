using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE : MonoBehaviour
{

    public GameObject circle;
    private DestroyOnTimer destroyTimer;
    
    //Holds and sets timer
    private float timer;
    public float maxTime;
    private bool startTimer; //Be sure to have this updated and set wherever the input will be handled
    
    // Start is called before the first frame update
    void Start()
    {
        timer = maxTime;
        destroyTimer = circle.GetComponent<DestroyOnTimer>();
        destroyTimer.maxTime = maxTime; //Syncs up the timers
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //When space is pressed, activate AOE
        if(Input.GetKeyDown(KeyCode.Space) && timer == maxTime)
        {
            ActivateTrigger();
        }
        
        
        
        if (startTimer)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
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
        Instantiate(circle, gameObject.transform.position, gameObject.transform.rotation);
        startTimer = true;
    }
}
