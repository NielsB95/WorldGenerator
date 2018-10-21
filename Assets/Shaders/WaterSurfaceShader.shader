Shader "Custom/WaterSurfaceShader" {
    
    // The properties which can be set from the inspector.
    Properties {        
        // Center of the planet. We need this to determine the elevation of a certain point.
        // default is the world's origin.
        _Center ("Center", Vector) = (0, 0, 0, 0)
        
        // MinHeight and MaxHeight of the planet.
        _Max ("Max", float) = 0
        _Min ("Min", float) = 0
        
        _Scale ("Scale", float) = 1
    }
    
    SubShader
        {
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
                
                vertexOutput vert (vertexInput v)
                {
                    vertexOutput o;

                    o.worldPosition = v.vertex;
                    o.vertex = UnityObjectToClipPos(v.vertex);

                    return o;
                }
                
                // The list of properties used in this shader.
                fixed4 _Center;
                float _Min;
                float _Max;
                float _Scale;
                
                // Colouring stuff
                float _ColorCount;
                float _ColorThresholds[100];
                float4 _Colors[100];
            
                half4 frag (vertexOutput i) : SV_Target
                {
                    //float dist = distance(_Center, i.worldPosition) / _Scale;
                    //float percentage = (dist - _Min) / (_Max - _Min);
                    
                        
                    return (0, 0, 1, 1);
                }
                ENDCG
            }
        }
}