// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "NanFangAR/FocusSystem_IBL_Transparent"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color("MainColor",Color) = (1,1,1,1)
		_FallOffColor("FallOffColor",Color) = (1,1,1,1)
		_Selected("_Selected",range(0,1)) = 0

		_CutOut("Cutoff",float)=0.01
		_Offset("ColorLerpOffset",float)=0.01
		_Alpha("Alpha",range(0,1))=1
		_FocusRange("FocusRange",float)=1
		_FocusPower("FocusPower",float)=1

		_IndirctCube("IndirctCube",CUBE)="white" {}
		 _AmoutAlpha("AmoutAlpha",range(0,1))=0.5
		_Roughness("Roughness",range(0,1))=1
		_Reflectivity("Reflectivity",range(0,1))=1
	}
	SubShader
	{
		
		Tags{ "LightMode" = "ForwardBase" }
		LOD 100

		Pass
		{
		zwrite off
		//Tags { "Queue"="AlphaTest" "RenderType"="TransparentCutout" "IgnoreProjector"="True" }
		 Tags {"Queue"="Transparent" "RenderType"="Transparent"}
		 Blend SrcAlpha OneMinusSrcAlpha
		   //Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
 			//AlphaToMask On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				half3 normal:NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				half3 normal:NORMAL;
				half3 viewDir:TEXCOORD2;
				half3 worldPos:TEXCOORD3;
				half3 lightPos:TEXCOORD4;
			};

			sampler2D _MainTex;
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
			half _AmoutAlpha;
			fixed _Selected;
			half3 _ViewPoints;
			half _FocusRange;
			half _FocusPower;
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.normal = UnityObjectToWorldNormal(v.normal);
				o.viewDir = WorldSpaceViewDir(v.vertex);
				// o.viewDir =normalize(mul(unity_ObjectToWorld,v.vertex).xyz-_WorldSpaceCameraPos);
				o.worldPos =mul(unity_ObjectToWorld,v.vertex).xyz;
				o.lightPos =_WorldSpaceLightPos0-mul(unity_ObjectToWorld,v.vertex);
				return o;
			}
			#define DIFFUSE_MIP_LEVEL 5
			#define GLOSSY_MIP_COUNT 6

			fixed4 frag (v2f i) : SV_Target
			{

				//IBL 光照模型
				half Roughness = 1-_Roughness;
				half oneMinusReflectivity =1-_Reflectivity;
				half3 n = normalize(i.normal);
				half3 v = normalize(i.viewDir);
				half3 l =normalize(i.lightPos);
				half3 refletView =reflect(n,v);

				fixed4 col = tex2D(_MainTex, i.uv);
				fixed3 albedo = lerp(0,col.rgb,oneMinusReflectivity);
				fixed3 indirctDiffuse =texCUBElod(_IndirctCube,half4(n,DIFFUSE_MIP_LEVEL));
				fixed3 indirctSpacular =texCUBElod(_IndirctCube,half4(refletView,Roughness*GLOSSY_MIP_COUNT))*_Reflectivity;
				half3 factor = saturate(dot(n, v));
				half3 diffusefator =saturate(dot(n, l));
				fixed3 diffuse = albedo*(diffusefator+indirctDiffuse)*_Color.rgb;

				fixed4 c =1;
				c.rgb =diffuse+indirctSpacular+ pow(1 - factor.r, 3);

				half3 selected = fixed3(1, 0.2, 0);
				//selected += selected;

				float pingpang = saturate(lerp(0, 1, sin(_Time.y * 10)))*_Selected;
				selected = selected * pingpang;

				//特效
				//half dis =_FocusRange-saturate(distance(_ViewPoints, i.worldPos))/_FocusPower;
				//fixed alpha  = _CutOut-factor;
				// clip(alpha);
				c.rgb += selected.rgb;
				return fixed4(c.rgb, _AmoutAlpha+(1- factor.r));
			}
			ENDCG
		}
	}
}
