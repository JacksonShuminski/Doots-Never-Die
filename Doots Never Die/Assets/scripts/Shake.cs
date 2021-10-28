using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public Animator camAnim;
    
    /// <summary>
    /// Plays camera shake when this is called
    /// </summary>
    public void CamShake()
    {
        camAnim.SetTrigger("Shake");
    }
}
