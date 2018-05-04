Shader "HappyCity/CurvedWorld" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_NormalMap("Bumpmap", 2D) = "Normal" {}
		_MetallicTex("TexMetal", 2D) = "metal" {}
		_Metallic("Metallic", Range(0,1)) = 0.0
		_Curvature("Curvature", Float) = 0.003
		_OcclusionTex("OcclusionTex", 2D) = "occlusion" {}
	}
		SubShader{
		Tags{ "RenderType" = "Opaque"}
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard vertex:vert addshadow
		//fullforwardshadows
		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _NormalMap;
		sampler2D _MetallicTex;
		sampler2D _OcclusionTex;
	uniform float _Curvature;

	half _Metallic;
	fixed4 _Color;


	struct Input {
		float2 uv_MainTex;
		float2 uv_NormalMap;
		float2 uv_MetallicTex;
		float2 uv_OcclusionTex;
	};

	// This is where the curvature is applied
	void vert(inout appdata_full v)
	{
		float4 vv = mul(unity_ObjectToWorld, v.vertex);
		vv.xyz -= _WorldSpaceCameraPos.xyz;
		vv = float4(0.0f, (vv.z * vv.z) * -_Curvature, 0.0f, 0.0f);

		v.vertex += mul(unity_WorldToObject, vv);
		
	}

	UNITY_INSTANCING_BUFFER_START(Props)

		UNITY_INSTANCING_BUFFER_END(Props)

		void surf(Input IN, inout SurfaceOutputStandard o) {
		// Albedo comes from a texture tinted by color
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		o.Alpha = c.a;
		o.Albedo = c.rgb;
		o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_NormalMap));
		o.Occlusion = tex2D(_OcclusionTex, IN.uv_OcclusionTex);
		// Metallic and smoothness come from slider variables
		o.Metallic = tex2D(_MetallicTex, IN.uv_MetallicTex) * _Metallic;
		
	}
	ENDCG
	}
		FallBack "Diffuse"
}