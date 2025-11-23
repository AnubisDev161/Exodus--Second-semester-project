Shader "Unlit/BillboardShader"
{
    Properties
    {
        _MainTex ("Icon Texture", 2D) = "white" {}
        _Color ("Tint Color", Color) = (1,1,1,1)
        _Size ("Size", Float) = 1.0
    }
    
    SubShader
    {
        Tags 
        { 
            "Queue"="Transparent+100"  // Renders after everything
            "RenderType"="Transparent"
            "DisableBatching"="True"   // Prevents batching from breaking billboarding
        }
        
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        ZTest Always  // Always renders regardless of depth
        
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
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            float _Size;
            
            v2f vert (appdata v)
            {
                v2f o;
                
                // Get camera-facing rotation
                float3 forward = UNITY_MATRIX_V._m20_m21_m22;
                float3 up = UNITY_MATRIX_V._m10_m11_m12;
                float3 right = normalize(cross(up, forward));
                
                // Billboard the vertices
                float3 worldPos = mul(unity_ObjectToWorld, float4(0,0,0,1)).xyz;
                float3 vertexOffset = v.vertex.x * right + v.vertex.y * up;
                worldPos += vertexOffset * _Size;
                
                o.pos = mul(UNITY_MATRIX_VP, float4(worldPos, 1.0));
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                return col;
            }
            ENDCG
        }
    }
}

