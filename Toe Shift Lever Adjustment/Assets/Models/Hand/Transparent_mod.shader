Shader "Transparent_Mod" {
	Properties{
		_Color("Main Color", Color) = (1,1,1,0.5)
		_MainTex("Base Texture", 2D) = "white" {}
	}
		SubShader{
			Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
			LOD 200
			Pass {
				ZWrite On
				ColorMask 0
			}
			UsePass "Transparent/Diffuse/FORWARD"
	}
		Fallback "Transparent/VertexLit"
}