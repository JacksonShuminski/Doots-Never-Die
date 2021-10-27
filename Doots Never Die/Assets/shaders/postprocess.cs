using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class postprocess : MonoBehaviour
{
    public Material mat;
    public Player player;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        float wiggle = (1-Mathf.Min(player.timer / player.maxTime, 1)) * 25;

        mat.SetFloat("_Width", source.width);
        mat.SetFloat("_Height", source.height);
        mat.SetFloat("_WiggleFactor", wiggle);


        Graphics.Blit(source, destination, mat);
    }
}
