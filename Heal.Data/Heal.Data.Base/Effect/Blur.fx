sampler2D clrSampler;

float4 Blur1(float2 texCoord : TEXCOORD0 ) : COLOR0
{
	float2 texSize = float2(800,600);
   float2  _xy = float2(texCoord.x * texSize.x , texCoord.y * texSize.y);
     //最终的输出颜色
     float4 final_color = float4(0.0,0.0,0.0,0.0);
     //对图像做滤波操作
     for(int i = -2 ; i <= 2 ; i ++ )
     {
		 for(int j = -2 ; j <= 2 ; j ++ )
		 {
        //计算采样点，得到当前像素附近的像素的坐标
          float2 _xy_new = float2(_xy.x + i ,
                                _xy.y + j);

        float2 _uv_new = float2(_xy_new.x/texSize.x , _xy_new.y/texSize.y);
        //采样并乘以滤波器权重，然后累加
          final_color += tex2D( clrSampler, _uv_new ) /25;
          }
     } 
     return final_color;
}
technique Technique1
{
    pass Pass1
    {
        // TODO: set renderstates here.

        //VertexShader = compile vs_1_1 VertexShaderFunction();
        PixelShader = compile ps_2_0 Blur1();
    }
}
