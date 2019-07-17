#pragma once
#include <d3dx9.h>

struct CUSTOMVERTEX
{
	D3DXVECTOR3 position;
	D3DXVECTOR3 normal; 
};

#define D3DFVF_CUSTOMVERTEX (D3DFVF_XYZ|D3DFVF_NORMAL)

LPDIRECT3DVERTEXBUFFER9 g_pVB = NULL;         // vextex buffer
LPDIRECT3DVERTEXBUFFER9 &ptr_g_pVB = g_pVB;   // pointer to vextex buffer

HRESULT InitGeometry(
	LPDIRECT3DDEVICE9 g_pd3dDevice, //3D device
	LPDIRECT3DVERTEXBUFFER9 &g_pVB, //pointer to empty bufffer
	int pointsCpount)               //vertex count are set hardcway 
{
		//Create the vertex buffer
		int length = 50 * 2* sizeof(CUSTOMVERTEX);
		if (FAILED(g_pd3dDevice->CreateVertexBuffer(
			length,
			0,
			D3DFVF_CUSTOMVERTEX,
			D3DPOOL_DEFAULT,
			&g_pVB,
			NULL)))
		{
			return E_FAIL;
		}

		//create and fill cylinder
		CUSTOMVERTEX* pVertices; 
	
		if (FAILED(g_pVB->Lock(0, 0, (void**)&pVertices, 0)))
		{
			return E_FAIL;
		}
	
		for (DWORD i = 0; i < 50 ; i++)
		{
			FLOAT theta = (2 * D3DX_PI * i) / (50 - 1);
			pVertices[2 * i + 0].position = D3DXVECTOR3( sinf(theta),-1.0f, cosf(theta) );
			pVertices[2 * i + 0].normal = D3DXVECTOR3(sinf(theta), 0.0f, cosf(theta));
			pVertices[2 * i + 1].position = D3DXVECTOR3(sinf(theta), 1.0f, cosf(theta));
			pVertices[2 * i + 1].normal = D3DXVECTOR3(sinf(theta), 0.0f, cosf(theta));
		}
	
		g_pVB->Unlock();
	
		return S_OK;
}

