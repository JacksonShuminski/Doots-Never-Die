using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessMenu : MonoBehaviour
{
    public Material mat;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        mat.SetFloat("_Width", source.width);
        mat.SetFloat("_Height", source.height);
        mat.SetFloat("_WiggleFactor", 0);

        mat.SetFloat("_Scream", 1);

        Graphics.Blit(source, destination, mat);
    }
}
