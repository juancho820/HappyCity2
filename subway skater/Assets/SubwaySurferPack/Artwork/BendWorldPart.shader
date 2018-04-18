Shader "HappyCity/particles" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Curvature("Curvature", Float) = 0.003
	}
		SubShader{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		Blend DstColor Zero
		AlphaTest Greater .01
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert vertex:vert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
	uniform float _Curvature;
	fixed4 _Color;


	struct Input {
		float2 uv_MainTex;
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

		void surf(Input IN, inout SurfaceOutput o) {
		// Albedo comes from a texture tinted by color
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
		o.Alpha = c.a;
		o.Albedo = (1-c.rgb) * _Color;		
	}
	ENDCG
	}
		FallBack "Diffuse"
}