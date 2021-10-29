using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTimer : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxTime;
    private float timer;

    //Screen shake
    private Shake shake;
    private GameObject shakeManager;
    
    void Start()
    {
        timer = maxTime;
        
        //Finds the object that holds the shake script and call it
        shakeManager = GameObject.FindGameObjectWithTag("ScreenShake");
        shake = shakeManager.GetComponent<Shake>();
        shake.CamShake();
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
