Shader "Custom/CRTShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ScreenBulge("Screen Bulge", Range(0.0, 2.0)) = 0.5
        _WiggleFactor("Wiggle factor", Range(0.0, 35.0)) = 0.5
        _WebExport("Web Export Bool 0 = off 1 = on", Int) = 0
        _Scream("Scream wave", Range(0.0, 1.0)) = 0.0
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float _ScreenBulge;
            float _WiggleFactor;
            float _Scream;
            int _WebExport;

            int2 findScreenSize(v2f i)
            {
                int2 screen_size;
                if (_WebExport == 0)
                {
                    screen_size = int2((int)(i.vertex.x / i.uv.x), (int)(i.vertex.y / (1 - i.uv.y)));
                }
                else
                {
                    screen_size = int2((int)(i.vertex.x / i.uv.x), (int)(i.vertex.y / (i.uv.y)));
                    //i.vertex.y = screen_size.y - i.vertex.y;
                }
                return screen_size;
            }

            // glitch that looks like a wave going down the screen
            // uv               The original uv coordinate of the pixel
            // wave_width       The percent of the screen the wave should take up vertically
            // wave_depth       The percent of the screen the wave will pull the uv
            // wave_speed       The speed at which the wave moves down
            // screen_percent   How far down the line will move
            float2 GlitchWaveEffectUV(float2 uv, float wave_width = 0.01, float wave_depth = 0.02, float wave_speed = 2, float hardness = 1.25, float screen_percent = 1, float start = 0)
            {
                // moving wave glitch 
                float2 disp = float2(0,0);
                float glitch_spot = 1 - ((_Time[0] * wave_speed) % screen_percent + start);
                if (uv.y > glitch_spot - wave_width && uv.y < glitch_spot + wave_width)
                {
                    float delta_x = 1 - pow(abs(glitch_spot - uv.y) / wave_width, hardness);
                    disp.x += wave_depth * delta_x;
                }
                return disp;
            }

            float2 ScreamWaveEffect(float2 uv, int2 screen_size)
            {
                float2 pixels_from_center = uv - float2(0.5, 0.5);
                pixels_from_center = pixels_from_center * screen_size;

            }

            // wiggly lines
            float2 WigglyLineEffectUV(float2 uv, int2 vertex, float scale)
            {
                float2 uv_from_center = uv - float2(0.5, 0.5);
                float dist_from_center = sqrt(pow(uv_from_center.x, 2) + pow(uv_from_center.y, 2));
                float2 disp = float2(sin(vertex.y / 10 + _Time[1] * 5) / 600, sin(vertex.x / 10 + _Time[1] * 5) / 600) * dist_from_center * scale;
                return disp;
            }

            // bulging TV glass
            float2 GlassBend(float2 uv, float tv_factor)
            {
                float2 disp = float2(0, 0);
                disp.x += pow((uv.y - 0.5), 2) * (uv.x - 0.5) * tv_factor;
                disp.y += pow((uv.x - 0.5), 2) * (uv.y - 0.5) * tv_factor;
                return disp;
            }

            // haze effect
            fixed4 HazeEffect(fixed4 col, float2 uv, float haze_speed = 1, float haze_amplitude = 0.002)
            {
                float delta = sin(_Time[1] * haze_speed) * haze_amplitude;
                // colors on the delta pixels
                fixed4 add_red = tex2D(_MainTex, uv + float2(delta, 0));
                fixed4 add_blue = tex2D(_MainTex, uv + float2(-delta, 0));
                float delta_redness = add_red.r - min(add_red.g, add_red.b);
                float delta_blueness = add_blue.b - min(add_blue.g, add_blue.r);
                float redness = col.r - min(col.g, col.b);
                float blueness = col.b - min(col.g, col.r);

                if (delta_redness > redness) {
                    col.r += delta_redness - redness;
                }
                if (delta_blueness > max(0, blueness)) {
                    col.b += delta_blueness - blueness;
                }

                return col;
            }

            // returns true when underneath the tv line pixles
            bool TVLinePixel(int2 vertex, float width = 2, int thickness = 2)
            {
                return (vertex.y % (width + thickness) < thickness);
            }

            // borders that clap
            float BorderDarkness(int2 vertex, float2 uv, int2 screen_size, float border = 75)
            {
                float darkness = 1;
                //upper -- lower in web
                if (uv.y * screen_size.y < border)
                    darkness *= (uv.y * screen_size.y / border);
                // lower -- upper in the web
                else if (uv.y * screen_size.y > screen_size.y - border)
                    darkness *= (screen_size.y - uv.y * screen_size.y) / border;
                // left
                if (uv.x * screen_size.x < border)
                    darkness *= (uv.x * screen_size.x / border);
                // right
                else if (uv.x * screen_size.x > screen_size.x - border)
                    darkness *= (screen_size.x - uv.x * screen_size.x) / border;
                
                // outer box
                if (uv.x < 0 || uv.x > 1 || uv.y < 0 || uv.y > 1)
                    darkness = 0;

                return darkness;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // finds the screensize in pixels
                int2 screen_size = findScreenSize(i);
                
                // wiggly lines and bulging glass
                float2 disp_uv = i.uv + WigglyLineEffectUV(i.uv, i.vertex, _WiggleFactor) + GlassBend(i.uv, _ScreenBulge);

                // moving wave glitchs
                disp_uv = disp_uv + GlitchWaveEffectUV(i.uv, 0.01, 0.01, 2, 1.25, 0.25, 0.75);
                disp_uv = disp_uv + GlitchWaveEffectUV(i.uv, 0.03, 0.02, 3, 10, 1, 0);

                // gets the color
                fixed4 col = tex2D(_MainTex, disp_uv);

                // haze effect
                col = HazeEffect(col, disp_uv);

                // tv lines
                if (TVLinePixel(i.vertex))
                    col.rgb = 0;

                // dark border
                col.rgb = col.rgb * BorderDarkness(i.vertex, i.uv + GlassBend(i.uv, _ScreenBulge), screen_size);

                return col;
            }
            ENDCG
        }
    }
}
