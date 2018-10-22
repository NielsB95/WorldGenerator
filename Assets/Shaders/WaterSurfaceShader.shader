Shader "Custom/WaterSurfaceShader" {
    
    // The properties which can be set from the inspector.
    Properties {        
        // Center of the planet. We need this to determine the elevation of a certain point.
        // default is the world's origin.
        _Center ("Center", Vector) = (0, 0, 0, 0)
        
        // MinHeight and MaxHeight of the planet.
        _Max ("Max", float) = 0
        _Min ("Min", float) = 0
        
        _WaveSpeed ("WaveSpeed", float) = 1
        _WaveAmplitude ("Wave amplitude", float) = 1
        _WaveFrequency ("Wave frequency", float) = 1
    }
    
    SubShader
        {
            Tags {"Queue" = "Transparent" "RenderType" = "Transparent"}
            LOD 100
           
            ZWrite off
            Blend SrcAlpha OneMinusSrcAlpha
           
            Pass
            {            
                CGPROGRAM
                // use "vert" function as the vertex shader
                #pragma vertex vert
                // use "frag" function as the pixel (fragment) shader
                #pragma fragment frag
                
                // vertex shader inputs
                struct vertexInput
                {
                    float4 vertex : POSITION; // vertex position
                };
                
                struct vertexOutput
                {
                    float4 worldPosition: TEXCOORD0;
                    float4 vertex : SV_POSITION; // clip space position
                };
                
                float _WaveSpeed;
                float _WaveAmplitude;
                float _WaveFrequency;
                float _Center;
                float _Scale;
                float _Min;
                float _Max;
                
                vertexOutput vert (vertexInput v)
                {
                    vertexOutput o;
                    
                    float dt = _Time * _WaveSpeed;
                    v.vertex.y = v.vertex.y + sin((dt + v.vertex.z + v.vertex.x) * _WaveFrequency) * _WaveAmplitude;
                    
                    o.worldPosition = v.vertex;
                    o.vertex = UnityObjectToClipPos(v.vertex);

                    return o;
                }
            
                float4 frag (vertexOutput i) : SV_Target
                {
                    float dist = distance(_Center, i.worldPosition) / _Scale;
                    float percentage = (dist - _Min) / (_Max - _Min);
                
                
                    return float4(0, 0, 1, 0.4 * (1 - percentage));
                }
                ENDCG
            }
        }
}