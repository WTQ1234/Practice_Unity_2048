// 鱼眼效果,计算公式网上找的
Shader "UI-Effect/FishEye"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _Radius ("半径", Range(0, 0.8)) = 0.1
        _Center_x ("中心点X", Range(-0.5, 0.5)) = 0
        _Center_y ("中心点Y", Range(-0.5, 0.5)) = 0
        _Factor_x ("x轴方向畸变", Range(0, 0.5)) = 0
        _Factor_y ("y 轴方向畸变", Range(0, 0.5)) = 0
        [Toggle] _UseTex("使用效果", Int) = 0
    }
    SubShader
    {
        Tags
        {
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}
        // No culling or depth
        Cull Off
        Lighting Off
        ZWrite Off
        // ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // shader_feature 可能在真机上会失效
            #pragma multi_compile _USETEX_ON

            #include "UnityCG.cginc"

            struct appdata
            {
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            fixed4 _Color;
            float _Factor_x;
            float _Factor_y;
            float _Center_x;
            float _Center_y;
            float _Radius;
            int _UseTex;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color * _Color;
                return o;
            }

            // _Radius ("半径", Range(0, 0.5)) = 0.1
            // _Center_x ("中心点X", Range(-0.5, 0.5)) = 0
            // _Center_y ("中心点Y", Range(-0.5, 0.5)) = 0
            inline fixed2 CalEyeUV(fixed2 uv)
            {
                float2 uv0 = (uv - 0.5) * 2.0;
                float2 uv1;
                uv1.x = 0;
                uv1.y = 0;
                // 暂定
                float disX = abs(uv0.x - _Center_x);
                float disY = abs(uv0.y - _Center_y);
                if (_UseTex == 0)
                {
                    float dis = _Radius * _Radius - (disX * disX + disY * disY);
                    if (dis < 0)
                    {
                        dis = 0;
                    }
                    uv1.x = (1 - uv0.y * uv0.y) * _Factor_y * dis * (uv0.x);
                    uv1.y = (1 - uv0.x * uv0.x) * _Factor_x * dis * (uv0.y);
                }
                return(uv - uv1);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                #if _USETEX_ON
                    float2 eyeUV = CalEyeUV(i.uv);
                    fixed4 col = tex2D(_MainTex, eyeUV) * i.color;
                #else
                    fixed4 col = tex2D(_MainTex, i.uv) * i.color;
                #endif
                return col;
            }
            ENDCG
        }
    }
}

