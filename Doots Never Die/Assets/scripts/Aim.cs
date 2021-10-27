using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    // Base Variables
    [SerializeField] private GameObject dootpf;
    public List<GameObject> projectileList;
    public float firerate;
    private float cooldown_acc;


    // Getting position data for GameObject
    private GameObject aimTransform;
    private GameObject bugleEndTransform;
    private bool scaleSwitch = false;

    /*
    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 bugleEndPointPosition;
        public Vector3 shootPosition;
    }
    */

    //-------------------------------------------------------------------------------------------------------------
    private void Awake()
    {
        aimTransform = GameObject.Find("Aim");
        bugleEndTransform = GameObject.Find("BugleEndPosition");
        cooldown_acc = 0;
    }

    // Update is called once per frame
    //-------------------------------------------------------------------------------------------------------------
    void Update()
    {
        PlayerAim();
        PlayerShoot();
    }

    // Base aiming method
    //-------------------------------------------------------------------------------------------------------------
    private void PlayerAim()
    {
        Vector3 mousePosition = GetMousePosition();

        Vector3 aimDirection = (mousePosition - aimTransform.transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        //Checking to see which way the player is facing and rotating bugle accordingly
        if (transform.localScale.x > 0)
        {
            aimTransform.transform.eulerAngles = new Vector3(0, 0, angle);
        }
        else
        {
            aimTransform.transform.eulerAngles = new Vector3(0, 0, angle - 180);
        }
    }

    //-------------------------------------------------------------------------------------------------------------
    private void PlayerShoot()
    {
        if (cooldown_acc > firerate)
        {
            if (Input.GetMouseButton(0))
            {
                GameObject shot = Instantiate(dootpf, bugleEndTransform.transform.position, Quaternion.identity);
                shot.GetComponent<Projectile>().skel_vel = GetComponent<Player>().moveAmount;

                if (transform.localScale.x < 0)
                {
                    shot.transform.up = bugleEndTransform.transform.right*-1;
                }else
                {
                    shot.transform.up = bugleEndTransform.transform.right;
                }

                projectileList.Add(shot);
                cooldown_acc = 0;
            }
        }
        else
        {
            cooldown_acc += Time.deltaTime;
        }
    }

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
