// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shockwave"
{
	Properties
	{
		_Displacement("_Displacement", Float) = 0.1
		_Distance("_Distance", Float) = 0.4
		_Center("_Center", Vector) = (0.5,0.5,0,0)
		_Width("_Width", Float) = 0.2
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Opaque" }
		LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend Off
		Cull Back
		ColorMask RGBA
		ZWrite On
		ZTest LEqual
		Offset 0 , 0
		
		
		
		Pass
		{
			Name "Unlit"
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			

			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 ase_texcoord : TEXCOORD0;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_texcoord : TEXCOORD0;
			};

			uniform sampler2D _TextureSample0;
			uniform float _Distance;
			uniform float _Width;
			uniform float2 _Center;
			uniform float _Displacement;
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_texcoord.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.zw = 0;
				float3 vertexValue =  float3(0,0,0) ;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				fixed4 finalColor;
				float temp_output_9_0 = ( _Distance - ( _Width * 0.5 ) );
				float2 uv07 = i.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float temp_output_13_0 = length( ( _Center - uv07 ) );
				float smoothstepResult14 = smoothstep( _Distance , temp_output_9_0 , temp_output_13_0);
				float smoothstepResult15 = smoothstep( temp_output_9_0 , ( _Distance - _Width ) , temp_output_13_0);
				float2 temp_output_18_0 = ( ( ( smoothstepResult14 - smoothstepResult15 ) * _Displacement ) + float2( 0,0 ) + uv07 );
				
				
				finalColor = tex2D( _TextureSample0, temp_output_18_0 );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=16700
66;-112;1752;841;-72.67639;424.3378;1;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;7;-504.1161,281.5452;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;5;-466.485,130.3754;Float;False;Property;_Center;_Center;2;0;Create;True;0;0;False;0;0.5,0.5;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;11;-497.5164,-18.84607;Float;False;Constant;_Half;Half;5;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-496.0143,-166.055;Float;False;Property;_Width;_Width;3;0;Create;True;0;0;False;0;0.2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-507.4084,-316.741;Float;False;Property;_Distance;_Distance;1;0;Create;True;0;0;False;0;0.4;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;8;-220.8549,184.8775;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-287.5051,-40.22447;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;9;-76.23631,-276.6445;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LengthOpNode;13;-37.2522,183.6199;Float;False;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;12;-78.75145,-148.374;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;14;186.5922,-228.8575;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;15;187.8498,-65.37546;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;16;397.8614,-132.0258;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;3;435.0137,96.53786;Float;False;Property;_Displacement;_Displacement;0;0;Create;True;0;0;False;0;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;651.8868,-77.951;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;18;810.3385,81.75815;Float;False;3;3;0;FLOAT;0;False;1;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ScreenColorNode;19;1025.407,78.23754;Float;False;Global;_GrabScreen0;Grab Screen 0;5;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;20;957.8972,-175.9163;Float;True;Property;_TextureSample0;Texture Sample 0;4;0;Create;True;0;0;False;0;8aba6bb20faf8824d9d81946542f1ce1;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;1;1331.559,90.00217;Float;False;True;2;Float;ASEMaterialInspector;0;1;Shockwave;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;True;0;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;True;False;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;True;0;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;1;True;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;8;0;5;0
WireConnection;8;1;7;0
WireConnection;10;0;6;0
WireConnection;10;1;11;0
WireConnection;9;0;4;0
WireConnection;9;1;10;0
WireConnection;13;0;8;0
WireConnection;12;0;4;0
WireConnection;12;1;6;0
WireConnection;14;0;13;0
WireConnection;14;1;4;0
WireConnection;14;2;9;0
WireConnection;15;0;13;0
WireConnection;15;1;9;0
WireConnection;15;2;12;0
WireConnection;16;0;14;0
WireConnection;16;1;15;0
WireConnection;17;0;16;0
WireConnection;17;1;3;0
WireConnection;18;0;17;0
WireConnection;18;2;7;0
WireConnection;19;0;18;0
WireConnection;20;1;18;0
WireConnection;1;0;20;0
ASEEND*/
//CHKSM=4059A3600BD63F10628E68CCA6840FCBF6BFC9A1