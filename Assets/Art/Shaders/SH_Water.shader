Shader "Custom/SH_Water"
{
    Properties
    {
        [NoScaleOffset]_MainTex ("Texture", 2D) = "white" {}
        _Color1 ("Color 1", Color) = (1,1,1,1)
        _Color2 ("Color 2", Color) = (1,1,1,1)
        _Tiling1 ("Tiling 1", Float) = 1
        _Tiling2 ("Tiling 2", Float) = 1
        _Scrolling ("Offset 1(w,x) - Offset 2(y,z)", Vector) = (0,0,0,0)
        _TimeStep ("Time Step", Float) = 10
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed _Tiling1, _Tiling2, _TimeStep;
            fixed4 _Color1, _Color2, _Scrolling;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = mul(unity_ObjectToWorld, v.vertex).xz;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed time = round(_Time.y * _TimeStep) / _TimeStep;


                fixed noise1 = tex2D(_MainTex, i.uv*_Tiling1 + time * _Scrolling.xy);
                fixed noise2 = tex2D(_MainTex, i.uv*_Tiling2 + time * _Scrolling.zw);

                fixed noise = noise1 * noise2;

                fixed4 col = lerp(_Color1, _Color2, noise);
                //col.rgb = noise.rrr;

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
