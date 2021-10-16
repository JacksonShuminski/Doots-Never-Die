using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class postprocess : MonoBehaviour
{
    public Material mat;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //mat.SetFloat("_ScreenBulge", 0.2f);
        Graphics.Blit(source, destination, mat);
    }
}
