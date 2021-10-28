using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTimer : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxTime;
    private float timer;

    //Screen shake
    
    
    void Start()
    {
        timer = maxTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
