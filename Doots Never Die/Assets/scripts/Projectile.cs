using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 direction;
    private GameObject player;
    private Aim aim;
    private int timer;

    void Start()
    {
        player = GameObject.Find("Skeleton");
        aim = player.GetComponent<Aim>();
        direction = Input.mousePosition - transform.position;
        transform.eulerAngles = new Vector3(0, 0, Vector3.Angle(direction, transform.forward));
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 5f;
        transform.position += direction.normalized * speed * Time.deltaTime;

        if(timer > 300)
        {
            aim.projectileList.Remove(gameObject);
            Destroy(gameObject);
            timer = 0;
        }

        timer++;
    }
}
