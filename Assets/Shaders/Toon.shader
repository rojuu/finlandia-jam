Shader "Custom/Toon" {
	Properties {
		_MainTex("Texture", 2D) = "white" {}
		_CelShadingLevels("Shading Levels", Float) = 3
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Toon
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _RampTex;
		half _CelShadingLevels;

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 LightingToon(SurfaceOutput s, fixed3 lightDir, fixed atten) {
			half NdotL = dot(s.Normal, lightDir);
			half cel = floor(NdotL * _CelShadingLevels) /
				(_CelShadingLevels - 0.5); // Snap

			fixed4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * cel * atten;
			c.a = s.Alpha;

			return c;
		}

		half4 LightingSimpleLambert(SurfaceOutput s, half3 lightDir, half atten) {
			half NdotL = dot(s.Normal, lightDir);
			half4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * (NdotL * atten);
			c.a = s.Alpha;
			return c;
		}

		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
		}

		ENDCG
	}
	FallBack "Diffuse"
}
