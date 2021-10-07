using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 direction;

    private void Setup(Vector3 direction)
    {
        this.direction = direction;
        transform.eulerAngles = new Vector3(0, 0, Vector3.Angle(direction, transform.forward));
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 100f;
        transform.position += direction * speed * Time.deltaTime;
        
    }
}
