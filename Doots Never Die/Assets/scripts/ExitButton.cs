using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouse = Input.mousePosition;
        Vector3 pointer = GetComponentInParent<Camera>().ScreenToWorldPoint(mouse);
        if (GetComponent<BoxCollider2D>().OverlapPoint(pointer))
        {
            GetComponent<SpriteRenderer>().color = new Color(0.75f, 0.75f, 0.75f);
            if (Input.GetMouseButton(0))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
        }
    }
}
