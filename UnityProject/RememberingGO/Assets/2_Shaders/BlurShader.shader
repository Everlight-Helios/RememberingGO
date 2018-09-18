Shader "FadeWipeOut" {
	Properties 
	{
		_MainTex ("L (RGB)", 2D) = "white" {}
		_Color ( "Main Color", Color ) = ( 1, 1, 1, 1 )
		_BlendColor ( "Blend Color", Color ) = ( 0, 0, 0, 0 )
		_Blend ( "Blend", Range ( 0, 1 ) ) = 0.5
		_WipeOut ( "Wipe Out", Range ( 0, 1 ) ) = 0
		_Blur ( "Blur", Range ( 0, 1 ) ) = 0
	}

	SubShader 
	{
		Tags { "Queue" = "Background" "RenderType" = "Opaque" }
		LOD 100

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _STITCHING_NONE _STITCHING_LR _STITCHING_LR_X4

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Blend;
			float _WipeOut;
			float _Blur;
			fixed4 _BlendColor;
			fixed4 _Color;

			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

		struct v2f {
			float4 vertex : SV_POSITION;
			float2 texcoord : TEXCOORD0;
		};

		// Color conversion from http://www.chilliant.com/rgb2hsv.html
		float3 HUEtoRGB(in float H)
		{
			float R = abs(H * 6 - 3) - 1;
			float G = 2 - abs(H * 6 - 2);
			float B = 2 - abs(H * 6 - 4);
			return saturate(float3(R,G,B));
		}
		float Epsilon = 1e-10;

		float3 RGBtoHCV(in float3 InRGB)
		{
			// Based on work by Sam Hocevar and Emil Persson
			float4 P = (InRGB.g < InRGB.b) ? float4(InRGB.bg, -1.0, 2.0/3.0) : float4(InRGB.gb, 0.0, -1.0/3.0);
			float4 Q = (InRGB.r < P.x) ? float4(P.xyw, InRGB.r) : float4(InRGB.r, P.yzx);
			float C = Q.x - min(Q.w, Q.y);
			float H = abs((Q.w - Q.y) / (6 * C + Epsilon) + Q.z);
			return float3(H, C, Q.x);
		}

		float3 RGBtoHSV(in float3 InRGB)
		{
			float3 HCV = RGBtoHCV(InRGB);
			float S = HCV.y / (HCV.z + Epsilon);
			return float3(HCV.x, S, HCV.z);
		}

		float3 HSVtoRGB(in float3 HSV)
		{
			float3 OutRGB = HUEtoRGB(HSV.x);
			return ((OutRGB - 1) * HSV.y + 1) * HSV.z;
		}

		v2f vert (appdata_t v)
		{
			v2f o;
			o.vertex = UnityObjectToClipPos(v.vertex);
			o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
			return o;
		}

		float3 frag (v2f i) : SV_Target
		{
			float3 col;

			float3 orginal = _Color * (tex2D( _MainTex, i.texcoord))/3;

			float3 blur1 = _Color * (tex2D( _MainTex, i.texcoord + (_Blur * 0.01)))/3;
			float3 blur2 = _Color * (tex2D( _MainTex, i.texcoord - (_Blur * 0.01)))/3;

			float3 t1 = orginal + blur1 + blur2;
			float3 fadeTexture = _BlendColor * _Color;
			col = lerp( t1, fadeTexture, _Blend );
			float3 HSV = RGBtoHSV(col);
			HSV.y *= 1 - _WipeOut;
			return HSVtoRGB(HSV);
		}
		ENDCG
		}
	}
}