Shader "IMShapes/Rectangle"
{
    Properties
    {
        _Colour("Colour", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
        }
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

            float RectangleSDF(float2 uv)
            {
                float2 d = abs(uv - float2(0.5, 0.5)) - float2(0.5, 0.5);
                float2 smoothD = max(d, 0.0);
                float cornerDistance = length(smoothD);
                float edgeDistance = min(max(d.x, d.y), 0.0);
                return cornerDistance + edgeDistance;
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float delta = fwidth(RectangleSDF(i.uv));
                float alpha = 1 - smoothstep(0 - delta, 0 + delta, RectangleSDF(i.uv));
                //return float4(_Colour.rgb, abs(RectangleSDF(i.uv)));
                return float4(_Colour.rgb, alpha);
            }
            ENDCG
        }
    }
}