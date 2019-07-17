#pragma once

struct RGB_colour_filter
{
	float R;
	float G;
	float B;
};

RGB_colour_filter GetRedRGBColourFilte(float R, float G, float B)
{
	RGB_colour_filter filter;
	filter.R = R;
	filter.G = G;
	filter.B = B;
	return filter;
};


