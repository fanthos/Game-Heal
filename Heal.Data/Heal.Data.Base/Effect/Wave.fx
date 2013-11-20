
float Intensity = 0.5f;
float2 coord = (0.2,0.15);
sampler2D clrSampler : register(s0);

float4 Wave1(float2 Tex : TEXCOORD0) : COLOR0
{
	float4 Color;
	float2 locate;
	float r = sqrt(pow(Tex.x-.5,2)+pow(Tex.y-.5,2))*2;
	float d = acos((Tex.x-.5)*2/r);	
	if(Tex.y-.5<0)
	{
		d=-d;
	}
	r += 0.03*sin((r+Intensity)*20);
	locate.y = r/2*sin(d)+.5;
	locate.x = r/2*cos(d)+.5;
	Color =  tex2D(clrSampler, locate);
	return Color;
}

float4 Wave2(float2 Tex : TEXCOORD0) : COLOR0
{
	float4 Color;
	float2 locate;
	locate.x = Tex.x + sin((Tex.y / coord.y) * 6.283 + Intensity) * coord.x * .03 * sin(Intensity);
	locate.y = Tex.y + sin((Tex.x / coord.x) * 6.283 + Intensity) * coord.y * .03 * sin(Intensity + 1.5708);
	Color =  tex2D(clrSampler, locate);
	return Color;
}

/*
float4 Wave3(float2 Tex : TEXCOORD0) : COLOR0
{
	float4 Color;
	float2 locate;
	float2 a;
	float2 b;
	a.x = sqrt(pow(Tex.x-.5,2)+pow(Tex.y-.5,2))*2;
	a.y = acos((Tex.x-.5)*2/a.x);	
	if(Tex.y-.5<0)
	{
		a.y=-a.y;
	}
	b.x = 0.03*sin((a.x+Intensity)*6.283)*a.x;
	b.y = 0.01*sin(Intensity*6.283)*a.x;
	locate.y = a.x/2*sin(d)+.5;
	locate.x = r/2*cos(d)+.5;
	Color =  tex2D(clrSampler, locate);
	return Color;
}
*/

technique myEffect
{
    pass wave1
    {
		PixelShader = compile ps_2_0 Wave1();
	}
    pass wave2
    {
		PixelShader = compile ps_2_0 Wave2();
	}
    pass wave3
    {
		//PixelShader = compile ps_2_0 Wave3();
	}
}

