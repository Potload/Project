// Shader created with Shader Forge v1.10 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.10;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,nrsp:0,limd:0,spmd:1,grmd:0,uamb:True,mssp:True,bkdf:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:2,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:False,dith:0,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:6855,x:32719,y:32712,varname:node_6855,prsc:2|emission-9875-OUT,alpha-7903-OUT;n:type:ShaderForge.SFN_Lerp,id:9875,x:32450,y:32807,varname:node_9875,prsc:2|A-1071-RGB,B-4000-RGB,T-676-OUT;n:type:ShaderForge.SFN_Color,id:1071,x:32133,y:32675,ptovrint:False,ptlb:node_1071,ptin:_node_1071,varname:node_1071,prsc:2,glob:False,c1:1,c2:0.5586206,c3:0,c4:1;n:type:ShaderForge.SFN_Color,id:4000,x:32147,y:32855,ptovrint:False,ptlb:node_4000,ptin:_node_4000,varname:node_4000,prsc:2,glob:False,c1:0.3683509,c2:0.0999135,c3:0.4852941,c4:1;n:type:ShaderForge.SFN_Fresnel,id:676,x:32302,y:33036,varname:node_676,prsc:2;n:type:ShaderForge.SFN_OneMinus,id:7903,x:32505,y:33019,varname:node_7903,prsc:2|IN-676-OUT;proporder:1071-4000;pass:END;sub:END;*/

Shader "Shader Forge/NewShader" {
    Properties {
        _node_1071 ("node_1071", Color) = (1,0.5586206,0,1)
        _node_4000 ("node_4000", Color) = (0.3683509,0.0999135,0.4852941,1)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _node_1071;
            uniform float4 _node_4000;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float node_676 = (1.0-max(0,dot(normalDirection, viewDirection)));
                float3 emissive = lerp(_node_1071.rgb,_node_4000.rgb,node_676);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,(1.0 - node_676));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
