Shader "IMShapes/Circle"
{
    Properties
    {
        _Colour("Colour", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" }
        ZWrite Off
        Cull Off
        Blend SrcAlpha OneMinusSrcAlpha

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

            float4 _Colour;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				float2 centreOffset = (i.uv.xy - 0.5) * 2;
				float squareDistance = dot(centreOffset, centreOffset);
				float distance = sqrt(squareDistance);
                
                float delta = fwidth(distance);
				float alpha = 1 - smoothstep(1 - delta, 1 + delta, squareDistance);
                
                return float4(_Colour.rgb, alpha);
            }
            ENDCG
        }
    }
}
