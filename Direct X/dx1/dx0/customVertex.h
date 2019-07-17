#pragma once
#include <d3dx9.h>

//4
//struct CUSTOMVERTEX
//{
//	D3DXVECTOR3 position;
//	D3DXVECTOR3 normal; 
//};

//5
struct CUSTOMVERTEX
{
	D3DXVECTOR3 position;
	D3DCOLOR color;
#ifndef SHOW_HOW_TO_USE_TCI
	FLOAT tu, tv; // texture coords.
#endif
};

//#define D3DFVF_CUSTOMVERTEX (D3DFVF_XYZ|D3DFVF_NORMAL) //4
//5
#ifdef SHOW_HOW_TO_USE_TCI
#define D3DFVF_CUSTOMVERTEX (D3DFVF_XYZ|D3DFVF_DIFFUSE)
#else
#define D3DFVF_CUSTOMVERTEX (D3DFVF_XYZ|D3DFVF_DIFFUSE|D3DFVF_TEX1)
#endif

LPDIRECT3DVERTEXBUFFER9 g_pVB = NULL;         // vextex buffer
LPDIRECT3DVERTEXBUFFER9 &ptr_g_pVB = g_pVB;   // pointer to vextex buffer

// ************************** Geometry **************************

//5
HRESULT InitGeometry(
	LPDIRECT3DDEVICE9       g_pd3dDevice,    //3D device
	LPDIRECT3DVERTEXBUFFER9 &g_pVB,          //pointer to empty bufffer
	LPDIRECT3DTEXTURE9      &ptr_g_pTexture) //pointer to etxture              
{

	const char* textureName = "banana.bmp";
	textureName = "w2.png"; // :) котик

	//textures
	if (FAILED(D3DXCreateTextureFromFile(g_pd3dDevice, textureName, &ptr_g_pTexture)))
	{
		MessageBox(NULL, "Could not find texture file ", "D3X - Textures", MB_OK);
		return E_FAIL;
	}

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
			return E_FAIL;
		for (DWORD i = 0; i < 50; i++)
		{
			FLOAT theta = (2 * D3DX_PI * i) / (50 - 1);

			pVertices[2 * i + 0].position = D3DXVECTOR3(sinf(theta), -1.0f, cosf(theta));
			pVertices[2 * i + 0].color = 0xffffffff;
#ifndef SHOW_HOW_TO_USE_TCI
			pVertices[2 * i + 0].tu = ((FLOAT)i) / (50 - 1);
			pVertices[2 * i + 0].tv = 1.0f;
#endif

			pVertices[2 * i + 1].position = D3DXVECTOR3(sinf(theta), 1.0f, cosf(theta));
			pVertices[2 * i + 1].color = 0xff808080;
#ifndef SHOW_HOW_TO_USE_TCI
			pVertices[2 * i + 1].tu = ((FLOAT)i) / (50 - 1);
			pVertices[2 * i + 1].tv = 0.0f;
#endif
		}
		g_pVB->Unlock();
	
		return S_OK;
}

//4 OLD *********************************************************************************************
HRESULT InitGeometry_4_MaterialAndLight(
	LPDIRECT3DDEVICE9 g_pd3dDevice, //3D device
	LPDIRECT3DVERTEXBUFFER9 &g_pVB, //pointer to empty bufffer
	int pointsCpount)               //vertex count are set hardcway 
{
	//Create the vertex buffer
	int length = 50 * 2 * sizeof(CUSTOMVERTEX);
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

	for (DWORD i = 0; i < 50; i++)
	{
		FLOAT theta = (2 * D3DX_PI * i) / (50 - 1);
		pVertices[2 * i + 0].position = D3DXVECTOR3(sinf(theta), -1.0f, cosf(theta));
		//pVertices[2 * i + 0].normal = D3DXVECTOR3(sinf(theta), 0.0f, cosf(theta));
		pVertices[2 * i + 1].position = D3DXVECTOR3(sinf(theta), 1.0f, cosf(theta));
	//	pVertices[2 * i + 1].normal = D3DXVECTOR3(sinf(theta), 0.0f, cosf(theta));
	}

	g_pVB->Unlock();

	return S_OK;
}


