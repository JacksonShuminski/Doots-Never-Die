// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Custom/House"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha("Enable External Alpha", Float) = 0
        _HouseType("Style of house index", Int) = 0
    }

        SubShader
        {
            Tags
            {
                "Queue" = "Transparent"
                "IgnoreProjector" = "True"
                "RenderType" = "Transparent"
                "PreviewType" = "Plane"
                "CanUseSpriteAtlas" = "True"
            }

            Cull Off
            Lighting Off
            ZWrite Off
            Blend One OneMinusSrcAlpha

            Pass
            {
            CGPROGRAM
                #pragma vertex SpriteVert
                #pragma fragment frag
                #pragma target 2.0
                #pragma multi_compile_instancing
                #pragma multi_compile_local _ PIXELSNAP_ON
                #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
                #include "UnitySprites.cginc"


                int _HouseType;

                fixed4 frag(v2f IN) : SV_Target
                {
                    fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;
                    

                    // frames
                    if (c.r == 1 && c.g == 0 && c.b == 0)
                    {
                        switch (_HouseType) {
                        case 0:
                            c = fixed4(0.19, 0.15, 0, 1);
                            break;
                        case 1:
                            c = fixed4(0.39, 0.25, 0, 1);
                            break;
                        case 2:
                            c = fixed4(0.16, 0.16, 0.16, 1);
                            break;
                        case 3:
                            c = fixed4(1, 1, 1, 1);
                            break;
                        default:
                            break;
                        }
                    }

                    // roof
                    if (c.r == 0 && c.g == 1 && c.b == 0)
                    {
                        switch (_HouseType) {
                        case 0:
                            c = fixed4(0.19, 0.15, 0, 1);
                            break;
                        case 1:
                            c = fixed4(0.39, 0.25, 0, 1);
                            break;
                        case 2:
                            c = fixed4(0.16, 0.16, 0.16, 1);
                            break;
                        case 3:
                            c = fixed4(0.18, 0.18, 0.18, 1);
                            break;
                        default:
                            break;
                        }
                    }

                    // walls
                    if (c.r == 0 && c.g == 0 && c.b == 1)
                    {
                        switch (_HouseType) {
                        case 0:
                            c = fixed4(0.37, 0, 0.11, 1);
                            break;
                        case 1:
                            c = fixed4(0.78, 0.22, 0, 1);
                            break;
                        case 2:
                            c = fixed4(0.37, 0.01, 0, 1);
                            break;
                        case 3:
                            c = fixed4(0.6, 0.47, 0.34, 1);
                            break;
                        default:
                            break;
                        }
                    }


                    c.rgb *= c.a;
                    return c;
                }

            ENDCG
            }
        }
}
