using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackOfHouseColorTheme : MonoBehaviour
{
    private Material mat;
    private int style;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<SpriteRenderer>().material;
        style = Random.Range(0, 4);
        mat.SetInt("_HouseType", style);
    }

}
