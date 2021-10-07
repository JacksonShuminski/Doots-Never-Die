using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 bugleEndPointPosition;
        public Vector3 shootPosition;
    }

    //Getting position data for GameObject
    private Transform aimTransform;
    private Transform bugleEndTransform;

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
        bugleEndTransform = transform.Find("BugleEndPosition");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAim();
        PlayerShoot();
    }

    private void PlayerAim()
    {
        Vector3 mousePosition = GetMousePosition();

        Vector3 aimDirection = (mousePosition - aimTransform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        //Checking to see which way the player is facing and rotating bugle accordingly
        if (transform.localScale.x > 0)
        {
            aimTransform.eulerAngles = new Vector3(0, 0, angle);
        }
        else
        {
            aimTransform.eulerAngles = new Vector3(0, 0, angle - 180);
        }
    }

    private void PlayerShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = GetMousePosition();

            OnShoot.Invoke(this, new OnShootEventArgs
            {
                bugleEndPointPosition = bugleEndTransform.position,
                shootPosition = mousePosition,
            }) ;
        }
    }

    public static Vector3 GetMousePosition()
    {
        Vector3 vec = GetMousePositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMousePositionWithZ(Camera worldCamera)
    {
        return GetMousePositionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMousePositionWithZ(Vector3 screenPOsition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPOsition);
        return worldPosition;
    }
}
