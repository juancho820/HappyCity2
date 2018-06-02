Shader "Custom/DiffuseAlphaNormalSpecular" {
 Properties {
     _MainTex ("Diffuse", 2D) = "white" {}
     _AlphaMap ("Alpha", 2D) = "white" {}
     _BumpMap ("Normalmap", 2D) = "bump" {}
     _SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
     _SpecularMap ("Specular", 2D) = "white" {}
     _Shininess ("Shininess", Range (0.01, 1)) = 0.5
 }
 SubShader {
     Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
     LOD 300
     CGPROGRAM
     #pragma surface surf BlinnPhong alpha decal:blend
     sampler2D _MainTex;
     sampler2D _AlphaMap;
     sampler2D _BumpMap;
     sampler2D _SpecularMap;
     half _Shininess;
     struct Input {
         float2 uv_MainTex;
         float2 uv_AlphaMap;
         float2 uv_SpecularMap;
         float2 uv_BumpMap;
     };
     void surf (Input IN, inout SurfaceOutput o) {
         half4 d = tex2D (_MainTex, IN.uv_MainTex);
         half4 a = tex2D (_AlphaMap, IN.uv_AlphaMap);
         half4 b = tex2D (_BumpMap, IN.uv_BumpMap);
         half4 s = tex2D (_SpecularMap, IN.uv_SpecularMap);
         o.Albedo = d.rgb;
         o.Alpha = Luminance(a.rgb);
         o.Normal = UnpackNormal(b);
         o.Specular = _Shininess;
         o.Gloss = s.rgb * o.Alpha;;
     }
     ENDCG
 } 
 FallBack "Transparent/Diffuse"  
 }