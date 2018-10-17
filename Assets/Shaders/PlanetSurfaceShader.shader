// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/PlanetSurfaceShader" {
	
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
                
                #include "UnityCG.cginc"
                
                // vertex shader inputs
                struct appdata
                {
                    float4 vertex : POSITION; // vertex position
                };

                // vertex shader outputs ("vertex to fragment")
                struct v2f
                {
                    float4 worldPosition: TEXCOORD0;
                    float4 vertex : SV_POSITION; // clip space position
                };

                // vertex shader
                v2f vert (appdata v)
                {
                    v2f o;
                    // transform position to clip space
                    // (multiply with model*view*projection matrix)
                    o.worldPosition = v.vertex;
                    o.vertex = UnityObjectToClipPos(v.vertex);

                    return o;
                }
                
                // The list of properties used in this cahder.
                fixed4 _Center;
                float _Min;
                float _Max;
                float _Scale;
            
                // pixel shader; returns low precision ("fixed4" type)
                // color ("SV_Target" semantic)
                half4 frag (v2f i) : SV_Target
                {
                    //float dist = length(i.worldPosition);
                    float dist = distance(_Center, i.worldPosition) / _Scale;
                
                    float percentage = (dist - _Min) / (_Max - _Min);
                    return half4(percentage, percentage, percentage, 1);
                }
                ENDCG
            }
        }
}