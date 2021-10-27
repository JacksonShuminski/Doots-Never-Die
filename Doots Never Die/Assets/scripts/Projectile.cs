using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Base Variables
    private Vector3 direction;
    private GameObject player;
    private Aim aim;
    private int timer;
    public Vector3 skel_vel;

    // "Reset"
    //-------------------------------------------------------------------------------------------------------------
    void Start()
    {
        player = GameObject.Find("DootSkeleton");
        aim = player.GetComponent<Aim>();
        direction =  GetMousePosition().normalized - transform.position.normalized;
        //transform.eulerAngles = new Vector3(0, 0, Vector3.Angle(direction, transform.forward));
        timer = 0;
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f));
        transform.GetChild(0).up = Vector3.up;
    }

    // Update is called once per frame
    //-------------------------------------------------------------------------------------------------------------
    void FixedUpdate()
    {
        // Starting Values
        float speed = 10f;

        transform.position += transform.up * speed * Time.deltaTime;
        transform.position += skel_vel * Time.deltaTime;
        if (timer > 300)
        {
            aim.projectileList.Remove(gameObject);
            timer = 0;
            Destroy(gameObject);
        }

        timer++;
    }

    // Methods used to adjust projectile direction
    //-------------------------------------------------------------------------------------------------------------
    public static Vector3 GetMousePosition()
    {
        Vector3 vec = GetMousePositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    //-------------------------------------------------------------------------------------------------------------
    public static Vector3 GetMousePositionWithZ(Camera worldCamera)
    {
        return GetMousePositionWithZ(Input.mousePosition, worldCamera);
    }

    //-------------------------------------------------------------------------------------------------------------
    public static Vector3 GetMousePositionWithZ(Vector3 screenPOsition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPOsition);
        return worldPosition;
    }
}
