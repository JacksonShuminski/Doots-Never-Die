using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    //Prefab for projectile
    [SerializeField] private Transform doot;

    //Aim field
    [SerializeField] private Aim aim;

    private void Awake()
    {
        aim.OnShoot += Shoot_OnShoot;
    }

    private void Shoot_OnShoot(object sender, Aim.OnShootEventArgs e)
    {
        Instantiate(doot, e.bugleEndPointPosition, Quaternion.identity);
    }
}
