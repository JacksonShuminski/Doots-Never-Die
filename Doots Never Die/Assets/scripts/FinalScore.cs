using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScore : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        float score = PlayerPrefs.GetFloat("score");
        GetComponent<UnityEngine.UI.Text>().text = "Score\n" + (int)(score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
