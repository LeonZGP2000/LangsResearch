#pragma once
#include <d3d9.h>


FLOAT CalcTheta(DWORD i)
{
	return (2 * D3DX_PI *i) / (50 - 1);
}