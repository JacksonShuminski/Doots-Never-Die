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
        float wiggle = (1-(player.timer / player.maxTime)) * 25;
        mat.SetFloat("_WiggleFactor", wiggle);
        Graphics.Blit(source, destination, mat);
    }
}
