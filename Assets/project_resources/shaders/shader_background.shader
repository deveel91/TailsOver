﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Fisac/Background Gradient Vignette"
{
	Properties
	{
		// Gradient
		_TopColor ("Top Color", Color) = (1,1,1,1)
		_BottomColor ("Bottom Color", Color) = (0,0,0,0)
		_Ratio("Ratio", Range(0,1)) = 0.5

		// Vignette
		_VignetteMax("Max value", Range(0,1)) = 0.5
		_Power("Intensity", Range(0,1)) = 0.5
	}

	SubShader
	{
		Cull Off
        ZWrite Off
        Lighting Off
		ZTest Always
		// Blend Zero SrcColor
                 
		Tags { "QUEUE"="Background" "RenderType"="Opaque" }
		LOD 200
		
		Pass 
		{

			Tags { "LIGHTMODE"="ForwardBase" "QUEUE"="Background" "RenderType"="Opaque" }
		
			CGPROGRAM

				#pragma vertex vert
				#pragma fragment frag
				
				#include "UnityCG.cginc"

				// Gradient
				uniform fixed4 _TopColor;
				uniform fixed4 _BottomColor;
				uniform fixed _Ratio;

				// Vignette
				uniform fixed _Power;
				uniform fixed _VignetteMax;
				
				struct IN
				{
					fixed4 vertex : POSITION;
					fixed2 uv : TEXCOORD0;
				};

				struct OUT
				{
					fixed4 pos : SV_POSITION;
					fixed2 uv : TEXCOORD0;
					fixed4 color : TEXCOORD1;
				};

				OUT vert (IN v)
				{
					OUT o;
					o.pos = UnityObjectToClipPos(v.vertex);
					_Ratio *= 2;
					_Ratio -=1;
					o.uv = v.uv;
					o.color = lerp(_BottomColor,_TopColor,clamp(v.uv.y+(_Ratio),0,1));
					return o;
				}

				fixed4 frag (OUT i) : SV_Target
				{
					fixed2 coords = i.uv;
					coords = (coords - 0.5)*2.0;	
					fixed coordDot = dot (coords,coords); 
					fixed mask = 1.0 - coordDot*_VignetteMax*_Power;

					fixed4 vignette = lerp(fixed4(0, 0, 0, 0), fixed4(1, 1, 1, 1), mask);
					fixed4 frag = vignette * i.color;

					return frag;
				}

			ENDCG
		}
	}
}
