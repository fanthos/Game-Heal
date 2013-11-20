sampler2D Texture0;
float2    TexSize = float2(800,600);
float    Intensity = 0.5f;
float4 mosaic1( float2 texCoord  : TEXCOORD0 ) : COLOR
{
   //�õ���ǰ�����������ͼ���С����ֵ��
   float2  intXY = float2((texCoord.x - 0.5) * TexSize.x , (texCoord.y - 0.5) * TexSize.y);
   //���������˿��С����ȡ����
   float2  XYMosaic   = float2(int(intXY.x/Intensity / 200) * Intensity * 200,
                               int(intXY.y/Intensity / 200) * Intensity * 200 );
   //����������ת���������������
   float2  UVMosaic   = float2(XYMosaic.x/TexSize.x + 0.5 , XYMosaic.y/TexSize.y + 0.5);
   return tex2D( Texture0, UVMosaic );
}

technique myEffect
{
    pass p0
    {
		PixelShader = compile ps_2_0 mosaic1();
	}
}
