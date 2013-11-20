
float Intensity = 0.5f;
float2 coord = (0.2,0.15);
sampler2D clrSampler : register(s0);
sampler2D clrSampler2 : register(s1);

float4 Screw(float2 Tex : TEXCOORD0) : COLOR0
{
	float4 Color;
	float2 locate;
	float r = sqrt(pow(Tex.x-.5,2)+pow(Tex.y-.5,2))*2;
	float d = acos((Tex.x-.5)*2/r);	
	if(Tex.y-.5<0)
	{
		d=-d;
	}
	d -= (1-Intensity)*sqrt(r)*50;
	r *= (Intensity);
	locate.y = r/2*sin(d)+.5;
	locate.x = r/2*cos(d)+.5;
	Color =  tex2D(clrSampler, locate) * Intensity;
	return Color;
}

float4 ScrewSwitch(float2 Tex : TEXCOORD0) : COLOR0
{
	float4 Color;
	float2 a,b;
	float m,n;
	float r = sqrt(pow(Tex.x-.5,2)+pow(Tex.y-.5,2))*2;
	float d = acos((Tex.x-.5)*2/r);	
	if(Tex.y-.5<0)
	{
		d=-d;
	}
	m = d+(1-Intensity)*sqrt(r)*50;
	n = r*(Intensity);
	a.y = n/2*sin(m)+.5;
	a.x = n/2*cos(m)+.5;
	m = d-(Intensity)*sqrt(r)*50;
	n = r*(1-Intensity);
	b.y = n/2*sin(m)+.5;
	b.x = n/2*cos(m)+.5;
	Color =  tex2D(clrSampler, a) * Intensity + tex2D(clrSampler2, b) * (1-Intensity);
	return Color;
}

technique myEffect
{
    pass p0
    {
		PixelShader = compile ps_2_0 Screw();
	}
    pass wave4
    {
		PixelShader = compile ps_2_0 ScrewSwitch();
	}
}

