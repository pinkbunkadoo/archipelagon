
Shader "Archipelago/Base Textured" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Emission ("Emission", Color) = (0,0,0,0)
        _Transparency ("Transparency", Range(0, 1)) = 0
    }

    SubShader {
        Tags { "Queue" = "AlphaTest" "RenderType" = "Cutout" }
        // Tags { "RenderType" = "Opaque" }
        CGPROGRAM
        
        #pragma surface surf Lambert alpha

        sampler2D _MainTex;

        struct Input {
            float2 uv_MainTex;
        };

        fixed3 _Emission;
        float _Transparency;

        void surf (Input IN, inout SurfaceOutput o) {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            // o.Albedo = c.rgb + _Emission;
            o.Albedo = c.rgb;
            o.Alpha = c.a * (1 - _Transparency);
        }
        ENDCG
    }
    Fallback "Diffuse"
}