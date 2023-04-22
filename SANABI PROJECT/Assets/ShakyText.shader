Shader "Custom/ShakyTextShader" {
    Properties{
        _MainTex("Texture", 2D) = "white" {}
        _ShakeAmount("Shake Amount", Range(0, 1)) = 0.1
    }

        SubShader{
            Tags {"Queue" = "Transparent" "RenderType" = "Transparent"}

            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float _ShakeAmount;

                v2f vert(appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target {
                    float2 offset = _ShakeAmount * (i.uv - 0.5);
                    float4 tex = tex2D(_MainTex, i.uv + offset);
                    return tex;
                }
                ENDCG
            }
        }
            FallBack "Diffuse"
}
