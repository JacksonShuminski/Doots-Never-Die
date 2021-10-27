using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class postprocess : MonoBehaviour
{
    public Material mat;
    public Player player;
    public AOE aoe_script;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        float wiggle = (1-Mathf.Min(player.timer / player.maxTime, 1)) * 15;

        mat.SetFloat("_Width", source.width);
        mat.SetFloat("_Height", source.height);
        mat.SetFloat("_WiggleFactor", wiggle);

        if (aoe_script.maxTime - aoe_script.timer < 1 && aoe_script.maxTime != aoe_script.timer)
            mat.SetFloat("_Scream", (aoe_script.maxTime - aoe_script.timer));
        else
            mat.SetFloat("_Scream", 1);

        Graphics.Blit(source, destination, mat);
    }
}
