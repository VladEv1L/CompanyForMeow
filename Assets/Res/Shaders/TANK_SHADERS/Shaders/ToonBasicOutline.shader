Shader "Custom/ToonBasicOutlineURP"
{
    Properties
    {
        [Header(Base Material)]
        _BaseColor("Base Color", Color) = (0.5, 0.5, 0.5, 1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        
        [Header(Toon Shading)]
        _ToonRamp("Toon Ramp", 2D) = "white" {}
        
        [Header(Outline Settings)]
        _OutlineColor("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineWidth("Outline Width", Range(0.001, 0.05)) = 0.01
    }
    
    SubShader
    {
        Tags 
        { 
            "Queue" = "Geometry-100" 
            "RenderType" = "Opaque" 
            "RenderPipeline" = "UniversalPipeline" 
        }
        
        Pass
        {
            Name "BASE"
            Tags {"LightMode" = "UniversalForward"}
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 positionOS       : POSITION;
                float2 uv               : TEXCOORD0;
                float3 normalOS         : NORMAL;
            };

            struct Varyings
            {
                float4 positionCS       : SV_POSITION;
                float2 uv               : TEXCOORD0;
                float3 normalWS         : TEXCOORD1;
            };

            CBUFFER_START(UnityPerMaterial)
                half4 _BaseColor;
                sampler2D _MainTex;
                sampler2D _ToonRamp;
                half4 _ToonRamp_ST;
                half4 _MainTex_ST;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
                OUT.normalWS = TransformObjectToWorldNormal(IN.normalOS);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                Light light = GetMainLight();
                half3 lightDir = light.direction;
                half NdotL = dot(IN.normalWS, lightDir);
                
                half3 albedo = tex2D(_MainTex, IN.uv).rgb * _BaseColor.rgb;
                half3 toonShade = tex2D(_ToonRamp, float2(NdotL, 0)).rgb;
                half3 finalColor = albedo * toonShade;

                return half4(finalColor, 1.0);
            }
            ENDHLSL
        }

        Pass
        {
            Name "OUTLINE"
            Tags {"LightMode" = "SRPDefaultUnlit"}
            Cull Front
            ZWrite On
            Blend SrcAlpha OneMinusSrcAlpha

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS       : POSITION;
                float3 normalOS         : NORMAL;
            };

            struct Varyings
            {
                float4 positionCS       : SV_POSITION;
            };

            CBUFFER_START(UnityPerMaterial)
                half4 _OutlineColor;
                half _OutlineWidth;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                float3 norm = normalize(mul((float3x3)UNITY_MATRIX_IT_MV, IN.normalOS));
                float2 offset = norm.xy * _OutlineWidth * 0.8; // Увеличиваем сглаживание
                OUT.positionCS = TransformObjectToHClip(IN.positionOS.xyz + float4(offset, 0, 0));
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                return _OutlineColor;
            }
            ENDHLSL
        }
    }
}