// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Transparent" {
	Properties{
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
		_Curvature("Curvature", Float) = 0.003
		_Soft("Soft", Range(0, 1)) = 0.1
		_Color("Color", Color) = (1,1,1,1)
	}

		SubShader{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		LOD 100

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass{
		CGPROGRAM
	#pragma vertex vert
	#pragma fragment frag
	#pragma multi_compile_fog

	#include "UnityCG.cginc"

		struct appdata_t {
		float4 vertex : POSITION;
		float2 texcoord : TEXCOORD0;
	};

	struct v2f {
		float4 vertex : SV_POSITION;
		half2 texcoord : TEXCOORD0;
		UNITY_FOG_COORDS(1)
	};

	sampler2D _MainTex;
	float _Curvature;
	float _Soft;
	float4 _MainTex_ST;
	fixed4 _Color;

	v2f vert(appdata_t v)
	{
		v2f o;

		float4 vv = mul(unity_ObjectToWorld, v.vertex);
		vv.xyz -= _WorldSpaceCameraPos.xyz;
		vv = float4(0.0f, (vv.z * vv.z) * -_Curvature, 0.0f, 0.0f);

		vv = mul(unity_WorldToObject, vv);
		o.vertex = UnityObjectToClipPos(v.vertex + vv);
		o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
		UNITY_TRANSFER_FOG(o, o.vertex);
		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{
		fixed4 col = tex2D(_MainTex, i.texcoord) * _Color;
	UNITY_APPLY_FOG(i.fogCoord, col);
	col.a = col.a * _Soft;
	return col;
	//return fixed4(1.0, 1.0, 1.0, 1.0);
	}
		ENDCG
	}
	}

}