Shader "Custom/CRTShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ScreenBulge("Screen Bulge", Range(0.0, 1.0)) = 0.5
        _WebExport("Web Export Bool 0 = off 1 = on", Int) = 0
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
            int _WebExport;

            fixed4 frag(v2f i) : SV_Target
            {
                // fixed4 col = tex2D(_MainTex, i.uv);
                int2 screen_size;
                if (_WebExport == 0)
                {
                    screen_size = int2((int)(i.vertex.x / i.uv.x), (int)(i.vertex.y / (1 - i.uv.y)));
                }
                else
                {
                    screen_size = int2(960, 600);
                }

                // wiggly lines
                float2 disp_uv = i.uv + float2(0, sin(i.vertex.x / 10 + _Time[1] * 5) / 600);

                // bulging glass
                float tv_factor = _ScreenBulge;
                float2 disp_uv_from_center = disp_uv - float2(0.5, 0.5);
                float dist_from_center = sqrt(pow(disp_uv_from_center.x, 2) + pow(disp_uv_from_center.y, 2));
                
                disp_uv.x += pow((float)disp_uv_from_center.y, 2) * disp_uv_from_center.x * tv_factor;
                disp_uv.y += pow((float)disp_uv_from_center.x, 2) * disp_uv_from_center.y * tv_factor;

                int2 disp_vertex = i.vertex;
                //disp_vertex.x -= (pow((float)disp_uv_from_center.y, 2) * disp_uv_from_center.x * tv_factor) * screen_size.x;
                //disp_vertex.y -= (pow((float)disp_uv_from_center.x, 2) * disp_uv_from_center.y * tv_factor) * screen_size.y;

                // does weird stuff
                //disp_uv += disp_uv_from_center / dist_from_center * tv_factor;

                // moving wave glitch 
                float wave_width = 0.01;
                float wave_depth = 0.01;
                float wave_speed = 2;
                float screen_percent = 1;
                float glitch_spot = 1 - ((_Time[0] * wave_speed) % screen_percent);
                if (i.uv.y > glitch_spot - wave_width && i.uv.y < glitch_spot + wave_width)
                {
                    float down = 1-pow(abs(glitch_spot - i.uv.y)*100,1.25);
                    disp_uv.x += wave_depth * down;
                }

                // gets the color
                fixed4 col = tex2D(_MainTex, disp_uv);
                // darkening
                // col.rgb = 0;
                

                // haze effect
                float haze_speed = 1;
                float haze_amplitude = 0.005;
                float delta = sin(_Time[1] * haze_speed) * haze_amplitude;
                // colors on the delta pixels
                fixed4 add_red = tex2D(_MainTex, disp_uv + float2(delta, 0));
                fixed4 add_blue = tex2D(_MainTex, disp_uv + float2(-delta, 0));
                float delta_redness = add_red.r - min(add_red.g, add_red.b);
                float delta_blueness = add_blue.b - min(add_blue.g, add_blue.r);
                float redness = col.r - min(col.g, col.b);
                float blueness = col.b - min(col.g, col.r);

                if (delta_redness > redness) {
                    col.r += delta_redness - redness;
                }
                if (delta_blueness > max(0,blueness)) {
                    col.b += delta_blueness - blueness;
                }

                // tv lines
                float width = 2;
                int thickness = 2;
                if (disp_vertex.y%(width+thickness) < thickness)
                {
                    col.rgb = 0.1;
                }
                

                // dark border
                float border = 50;
                if (i.vertex.y < border) //upper -- exports to lower in web
                {
                    col.rgb = col.rgb * (i.vertex.y / border);
                }
                else if (i.vertex.y > screen_size.y - border) // lower -- upper in the web
                {
                    col.rgb = col.rgb * (screen_size.y - i.vertex.y) / border;
                }
                if (i.vertex.x < border) // left
                {
                    col.rgb = col.rgb * (i.vertex.x / border);
                }
                else if (i.vertex.x > screen_size.x - border)
                {
                    col.rgb = col.rgb * (screen_size.x - i.vertex.x) / border;
                }
                /*float border = 0.05;
                if (i.uv.y < border)
                {
                    col.rgb = col.rgb * i.uv.y / border;
                }
                else if (i.uv.y > 1 - border)
                {
                    col.rgb = col.rgb * (1 - i.uv.y) / border;
                }
                if (i.uv.x < border)
                {
                    col.rgb = col.rgb * i.uv.x / border;
                }
                else if (i.uv.x > 1 - border)
                {
                    col.rgb = col.rgb * (1 - i.uv.x) / border;
                }*/

                // Flashing light
                // col.rgb = col.rgb + sin(_Time[1]*10) * 0.05;

                // Flashing dark
                //col.rgb = col.rgb * 0.8 - 0.3 + sin(_Time[1]*2) * 0.05;

                // just invert the colors
                // col.rgb = 1 - col.rgb;
                return col;
            }
            ENDCG
        }
    }
}
