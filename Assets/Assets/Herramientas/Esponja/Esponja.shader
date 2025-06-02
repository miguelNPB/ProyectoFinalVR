Shader "Custom/SpongeShader" {
    Properties {
        _MainColor ("Color", Color) = (0.96, 0.90, 0.71, 1) // Color amarillo claro de esponja
        _NoiseScale ("Noise Scale", Range(0.1, 10)) = 2.0
        _Porosity ("Porosity", Range(0, 1)) = 0.5
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #include "UnityCG.cginc"
        
        fixed4 _MainColor;
        float _NoiseScale;
        float _Porosity;
        
        // Función de ruido simple (mejorada)
        float noise(float3 pos) {
            return frac(sin(dot(pos.xy, float2(12.9898, 78.233))) * 43758.5453);
        }
        
        // Estructura Input necesaria para Surface Shaders
        struct Input {
            float3 worldPos; // Coordenadas del mundo para el ruido
        };
        
        void surf (Input IN, inout SurfaceOutputStandard o) {
            float3 worldPos = IN.worldPos * _NoiseScale;
            float n = noise(worldPos);
            
            // Simula poros: si el ruido es menor que _Porosity, oscurece el color
            float porosity = step(n, _Porosity);
            fixed4 col = _MainColor * (1 - porosity * 0.3);
            
            o.Albedo = col.rgb;
            o.Metallic = 0.0;
            o.Smoothness = 0.1; // Bajo brillo para simular esponja
            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}