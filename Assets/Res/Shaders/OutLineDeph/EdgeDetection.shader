Shader "Custom/EdgeDetection"
{
    Properties
    {
        _Color("Color", Color) = (1, 1, 1, 1)
    }

    SubShader
    {
        Tags {"RenderType"="Opaque" "Queue"="Geometry"}
        
        // Если нужно поддерживать прозрачность, замените на "Blend SrcAlpha OneMinusSrcAlpha"
        Blend Off
        ZWrite On
        Cull Back

        Pass
        {
            Name "EdgeDetection"
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            CBUFFER_START(UnityPerMaterial)
                half4 _Color;
            CBUFFER_END

            TEXTURE2D(_CameraDepthTexture);
            SAMPLER(sampler_CameraDepthTexture);

            struct Attributes
            {
                float4 positionOS   : POSITION;
                float3 normalOS     : NORMAL;
            };

            struct Varyings
            {
                float4 positionCS   : SV_POSITION;
                float4 screenPos    : TEXCOORD0;
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                
                OUT.positionCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.screenPos = ComputeScreenPos(OUT.positionCS);

                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                half2 uv = IN.screenPos.xy / IN.screenPos.w;
                uv = (uv + 1) * 0.5; // Convert from [-1, 1] to [0, 1]

                half2 onePixel = half2(1.0 / _ScreenParams.x, 1.0 / _ScreenParams.y);

                half depthCenter = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, uv);
                half depthLeft = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, uv - half2(onePixel.x, 0));
                half depthRight = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, uv + half2(onePixel.x, 0));
                half depthUp = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, uv + half2(0, onePixel.y));
                half depthDown = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, uv - half2(0, onePixel.y));

                half edgeStrength = abs(depthLeft - depthRight) + abs(depthUp - depthDown);

                return _Color * edgeStrength;
            }
            ENDHLSL
        }
    }
}