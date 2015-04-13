// Shader created with Shader Forge v1.10 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.10;sub:START;pass:START;ps:flbk:,lico:0,lgpr:1,nrmq:0,nrsp:0,limd:0,spmd:1,grmd:0,uamb:True,mssp:True,bkdf:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:False,hqlp:False,tesm:0,blpr:2,bsrc:0,bdst:0,culm:2,dpts:2,wrdp:False,dith:0,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:True;n:type:ShaderForge.SFN_Final,id:1,x:34836,y:32642,varname:node_1,prsc:2|emission-59-OUT,voffset-172-OUT;n:type:ShaderForge.SFN_Color,id:2,x:33875,y:32489,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_3298,prsc:2,glob:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Color,id:3,x:33713,y:32460,ptovrint:False,ptlb:Glow,ptin:_Glow,varname:node_4306,prsc:2,glob:False,c1:0,c2:0.8896552,c3:0,c4:1;n:type:ShaderForge.SFN_TexCoord,id:4,x:32513,y:32718,varname:node_4,prsc:2,uv:0;n:type:ShaderForge.SFN_Lerp,id:5,x:34255,y:32723,varname:node_5,prsc:2|A-3-RGB,B-2-RGB,T-48-OUT;n:type:ShaderForge.SFN_Add,id:8,x:33214,y:32488,varname:node_8,prsc:2|A-17-OUT,B-14-OUT;n:type:ShaderForge.SFN_Vector1,id:9,x:32744,y:32592,varname:node_9,prsc:2,v1:-2;n:type:ShaderForge.SFN_Abs,id:13,x:33390,y:32536,varname:node_13,prsc:2|IN-8-OUT;n:type:ShaderForge.SFN_Multiply,id:14,x:33004,y:32736,varname:node_14,prsc:2|A-19-OUT,B-4-V;n:type:ShaderForge.SFN_Vector1,id:15,x:32812,y:32460,varname:node_15,prsc:2,v1:-1;n:type:ShaderForge.SFN_Slider,id:16,x:32371,y:32401,ptovrint:False,ptlb:thickness,ptin:_thickness,varname:node_7635,prsc:2,min:1,cur:1,max:4;n:type:ShaderForge.SFN_Multiply,id:17,x:32990,y:32405,varname:node_17,prsc:2|A-16-OUT,B-15-OUT;n:type:ShaderForge.SFN_Multiply,id:19,x:32960,y:32570,varname:node_19,prsc:2|A-17-OUT,B-9-OUT;n:type:ShaderForge.SFN_Clamp01,id:20,x:33569,y:32638,varname:node_20,prsc:2|IN-13-OUT;n:type:ShaderForge.SFN_Add,id:25,x:33190,y:33031,varname:node_25,prsc:2|A-56-OUT,B-31-OUT;n:type:ShaderForge.SFN_Vector1,id:27,x:32657,y:33351,varname:node_27,prsc:2,v1:2;n:type:ShaderForge.SFN_Abs,id:29,x:33396,y:33079,varname:node_29,prsc:2|IN-25-OUT;n:type:ShaderForge.SFN_Multiply,id:31,x:33170,y:33186,varname:node_31,prsc:2|A-27-OUT,B-4-U;n:type:ShaderForge.SFN_Slider,id:35,x:33942,y:33338,ptovrint:False,ptlb:Strength,ptin:_Strength,varname:node_952,prsc:2,min:0,cur:0.2,max:3;n:type:ShaderForge.SFN_Clamp01,id:41,x:33553,y:33153,varname:node_41,prsc:2|IN-29-OUT;n:type:ShaderForge.SFN_OneMinus,id:45,x:33603,y:33005,varname:node_45,prsc:2|IN-41-OUT;n:type:ShaderForge.SFN_Multiply,id:48,x:33937,y:32840,varname:node_48,prsc:2|A-51-OUT,B-45-OUT;n:type:ShaderForge.SFN_OneMinus,id:51,x:33708,y:32796,varname:node_51,prsc:2|IN-20-OUT;n:type:ShaderForge.SFN_Vector1,id:53,x:33858,y:33141,varname:node_53,prsc:2,v1:2;n:type:ShaderForge.SFN_Vector1,id:55,x:34180,y:32510,varname:node_55,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:56,x:33004,y:32994,varname:node_56,prsc:2,v1:-1;n:type:ShaderForge.SFN_Lerp,id:59,x:34572,y:32709,varname:node_59,prsc:2|A-55-OUT,B-149-OUT,T-63-OUT;n:type:ShaderForge.SFN_Multiply,id:60,x:34217,y:33105,varname:node_60,prsc:2|A-35-OUT,B-48-OUT;n:type:ShaderForge.SFN_Vector1,id:61,x:34007,y:33139,varname:node_61,prsc:2,v1:2;n:type:ShaderForge.SFN_Clamp01,id:63,x:34357,y:33178,varname:node_63,prsc:2|IN-60-OUT;n:type:ShaderForge.SFN_Multiply,id:149,x:34028,y:32034,varname:node_149,prsc:2|A-5-OUT,B-150-RGB;n:type:ShaderForge.SFN_VertexColor,id:150,x:33774,y:31881,varname:node_150,prsc:2;n:type:ShaderForge.SFN_Cos,id:151,x:33657,y:33473,varname:node_151,prsc:2|IN-155-OUT;n:type:ShaderForge.SFN_Tau,id:152,x:33278,y:33596,varname:node_152,prsc:2;n:type:ShaderForge.SFN_Time,id:153,x:32787,y:33423,varname:node_153,prsc:2;n:type:ShaderForge.SFN_Frac,id:154,x:32865,y:33307,varname:node_154,prsc:2|IN-167-OUT;n:type:ShaderForge.SFN_Multiply,id:155,x:33378,y:33450,varname:node_155,prsc:2|A-170-OUT,B-152-OUT;n:type:ShaderForge.SFN_Append,id:158,x:33889,y:33504,varname:node_158,prsc:2|A-159-OUT,B-163-OUT;n:type:ShaderForge.SFN_Vector1,id:159,x:33712,y:33393,varname:node_159,prsc:2,v1:0;n:type:ShaderForge.SFN_Append,id:161,x:34099,y:33504,varname:node_161,prsc:2|A-158-OUT,B-159-OUT;n:type:ShaderForge.SFN_Multiply,id:163,x:33802,y:33634,varname:node_163,prsc:2|A-151-OUT,B-164-OUT;n:type:ShaderForge.SFN_Slider,id:164,x:33465,y:33698,ptovrint:False,ptlb:SinHeight,ptin:_SinHeight,varname:node_6898,prsc:2,min:0,cur:14.68232,max:22;n:type:ShaderForge.SFN_Multiply,id:167,x:32861,y:33637,varname:node_167,prsc:2|A-153-TSL,B-169-OUT;n:type:ShaderForge.SFN_Slider,id:169,x:32524,y:33701,ptovrint:False,ptlb:SinSpeed,ptin:_SinSpeed,varname:node_2574,prsc:2,min:0,cur:23.78641,max:70;n:type:ShaderForge.SFN_Subtract,id:170,x:33101,y:33596,varname:node_170,prsc:2|A-154-OUT,B-4-U;n:type:ShaderForge.SFN_Multiply,id:172,x:34397,y:33407,varname:node_172,prsc:2|A-45-OUT,B-161-OUT;proporder:2-3-16-35-164-169;pass:END;sub:END;*/

Shader "Oberonscourt/lightning" {
    Properties {
        _Color ("Color", Color) = (1,0,0,1)
        _Glow ("Glow", Color) = (0,0.8896552,0,1)
        _thickness ("thickness", Range(1, 4)) = 1
        _Strength ("Strength", Range(0, 3)) = 0.2
        _SinHeight ("SinHeight", Range(0, 22)) = 14.68232
        _SinSpeed ("SinSpeed", Range(0, 70)) = 23.78641
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
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 
            #pragma target 2.0
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform float4 _Glow;
            uniform float _thickness;
            uniform float _Strength;
            uniform float _SinHeight;
            uniform float _SinSpeed;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                float node_45 = (1.0 - saturate(abs(((-1.0)+(2.0*o.uv0.r)))));
                float node_159 = 0.0;
                float4 node_153 = _Time + _TimeEditor;
                v.vertex.xyz += (node_45*float3(float2(node_159,(cos(((frac((node_153.r*_SinSpeed))-o.uv0.r)*6.28318530718))*_SinHeight)),node_159));
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float node_55 = 0.0;
                float node_17 = (_thickness*(-1.0));
                float node_45 = (1.0 - saturate(abs(((-1.0)+(2.0*i.uv0.r)))));
                float node_48 = ((1.0 - saturate(abs((node_17+((node_17*(-2.0))*i.uv0.g)))))*node_45);
                float3 emissive = lerp(float3(node_55,node_55,node_55),(lerp(_Glow.rgb,_Color.rgb,node_48)*i.vertexColor.rgb),saturate((_Strength*node_48)));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCollector"
            Tags {
                "LightMode"="ShadowCollector"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCOLLECTOR
            #define SHADOW_COLLECTOR_PASS
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcollector
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 
            #pragma target 2.0
            uniform float4 _TimeEditor;
            uniform float _SinHeight;
            uniform float _SinSpeed;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_COLLECTOR;
                float2 uv0 : TEXCOORD5;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                float node_45 = (1.0 - saturate(abs(((-1.0)+(2.0*o.uv0.r)))));
                float node_159 = 0.0;
                float4 node_153 = _Time + _TimeEditor;
                v.vertex.xyz += (node_45*float3(float2(node_159,(cos(((frac((node_153.r*_SinSpeed))-o.uv0.r)*6.28318530718))*_SinHeight)),node_159));
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_COLLECTOR(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
/////// Vectors:
                SHADOW_COLLECTOR_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 
            #pragma target 2.0
            uniform float4 _TimeEditor;
            uniform float _SinHeight;
            uniform float _SinSpeed;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                float node_45 = (1.0 - saturate(abs(((-1.0)+(2.0*o.uv0.r)))));
                float node_159 = 0.0;
                float4 node_153 = _Time + _TimeEditor;
                v.vertex.xyz += (node_45*float3(float2(node_159,(cos(((frac((node_153.r*_SinSpeed))-o.uv0.r)*6.28318530718))*_SinHeight)),node_159));
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
/////// Vectors:
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
