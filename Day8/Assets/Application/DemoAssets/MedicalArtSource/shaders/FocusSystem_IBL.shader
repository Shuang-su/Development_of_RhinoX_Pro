// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "NanFangAR/FocusSystem_IBL"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_RampTex ("RampTex", 2D) = "white" {}
		_MainColor("MainColor",Color) =(1,1,1,1)
		_Selected("SelectedColor",range(0,1)) = 0
		_Color("MainColor",Color) = (1,1,1,1)
		[HDR]_FallOffColor("FallOffColor",Color) = (1,1,1,1)
		_CutOut("Cutoff",float)=0.01
		_Offset("ColorLerpOffset",float)=0.01
		_Alpha("Alpha",range(0,1))=1
		_FocusRange("FocusRange",float)=1
		_FocusPower("FocusPower",float)=1
		
		_IndirctCube("IndirctCube",CUBE)="white" {}
		// _IndirctSpacularCube("IndirctSpacularCube",CUBE)="white" {}
		_Roughness("Roughness",range(0,1))=1
		_Reflectivity("Reflectivity",range(0,1))=1
	}
	SubShader
	{
		
		Tags{ "LightMode" = "ForwardBase" }
		LOD 100

		Pass
		{
		//Tags { "Queue"="AlphaTest" "RenderType"="TransparentCutout" "IgnoreProjector"="True" }
		 Tags {"Queue"="Opaque" "RenderType"="Geometry"}
		// Blend SrcAlpha OneMinusSrcAlpha
		//   Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
 			//AlphaToMask On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#pragma target 3.0
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				half3 normal:NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 pos : SV_POSITION;
				half3 normal:NORMAL;
				half3 viewDir:TEXCOORD2;
				half3 worldPos:TEXCOORD3;
				half3 lightPos:TEXCOORD4;

				//#if defined(SHADOW_DEPTH)&&!defined(SPOT)
				SHADOW_COORDS(5)
				//#endif

			};

			sampler2D _MainTex;
			sampler2D _RampTex;
			samplerCUBE _IndirctCube;
			// samplerCUBE _IndirctSpacularCube;
			float4 _MainTex_ST;
			half4 _Color;
			half4 _FallOffColor;
			uniform half _CutOut;
			half _Offset;
			half _Alpha;
			half _Roughness;
			half _Reflectivity;
			fixed _Selected;

			half3 _ViewPoints;
			half _FocusRange;
			half _FocusPower;
			half4 _MainColor;
			v2f vert (appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.normal = UnityObjectToWorldNormal(v.normal);
				o.viewDir = WorldSpaceViewDir(v.vertex);
				// o.viewDir =normalize(mul(unity_ObjectToWorld,v.vertex).xyz-_WorldSpaceCameraPos);
				o.worldPos =mul(unity_ObjectToWorld,v.vertex).xyz;
				o.lightPos =_WorldSpaceLightPos0;
				TRANSFER_SHADOW(o);
				return o;
			}
			#define DIFFUSE_MIP_LEVEL 5
			#define GLOSSY_MIP_COUNT 6

			fixed4 frag (v2f i) : SV_Target
			{

				//IBL
				half Roughness = 1-_Roughness;
				half oneMinusReflectivity =1-_Reflectivity;
				half3 n = normalize(i.normal);
				half3 v = normalize(i.viewDir);
				half3 l =normalize(i.lightPos);
				half3 refletView =reflect(n,v);

				fixed shadow = SHADOW_ATTENUATION(i);
				fixed4 col = tex2D(_MainTex, i.uv)*_MainColor;
				fixed3 albedo = lerp(0,col.rgb,oneMinusReflectivity);

				fixed3 indirctDiffuse =texCUBElod(_IndirctCube,half4(n,DIFFUSE_MIP_LEVEL));
				fixed3 indirctSpacular =texCUBElod(_IndirctCube,half4(refletView,Roughness*GLOSSY_MIP_COUNT))*_Reflectivity;

				half3 factor = pow(saturate(dot(n, v)),0.5);
				half diffusefator =saturate(dot(n, l))*shadow;
				fixed3 rampColor = tex2D(_RampTex,half2(diffusefator+0.1,1)).rgb;
				fixed3 diffuse = albedo*(indirctDiffuse+rampColor);
				fixed4 c =1;
				c.rgb = diffuse+indirctSpacular+(1-factor)*indirctDiffuse;

				//特效
				half3 selected = pow(1-saturate(dot(v, n)),3)*(_Selected*_FallOffColor.rgb);
				selected += selected;

				float pingpang = lerp(0, 1, sin(_Time.y * 10));
				selected += selected * pingpang;

				fixed4 finalCol = c;
				finalCol.rgb += selected;
				return finalCol;
			}
			ENDCG
		}
	}
	Fallback "Specular"
}
