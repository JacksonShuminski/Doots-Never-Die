using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour
{
    // Base Variables
    public Transform target;
    public float smoothness; //Time it takes for the camera to move
    private Vector3 velocity;

    // Update is called once per frame
    //-------------------------------------------------------------------------------------------------------------
    void Update()
    {
        Vector3 targetPosition = target.TransformPoint(0, 0, -10); //Set to when the player starts to go off screen
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothness);

        if (target.gameObject.GetComponent<Player>().gameState == GameState.Pause)
        {
            GetComponent<AudioSource>().volume = 0.07f;
        }
        else
        {
            GetComponent<AudioSource>().volume = 0.54f;
        }
    }
}
