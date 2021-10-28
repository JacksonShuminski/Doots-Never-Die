using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timer_ui : MonoBehaviour
{

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xScale = Mathf.Clamp(player.timer / player.maxTime, 0,1);
        Vector3 newScale = new Vector3(xScale,1,1);
        transform.localScale = newScale;
    }
}
