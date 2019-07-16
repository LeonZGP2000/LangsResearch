#pragma once
#include <windows.h>
#include <d3d9.h>

//vertex point
struct CUSTOMVERTEX
{
	//float x, y, z, rhw;  // coords + transformed position for the vertex //2
	float x, y, z; //3
	DWORD color;         // color
};

LPDIRECT3DVERTEXBUFFER9 g_pVB = NULL;         // vextex buffer
LPDIRECT3DVERTEXBUFFER9 &ptr_g_pVB = g_pVB;   // pointer to vextex buffer

//FVF vertex 
//#define D3DFVF_CUSTOMVERTEX (D3DFVF_XYZRHW | D3DFVF_DIFFUSE ) // 2
#define D3DFVF_CUSTOMVERTEX (D3DFVF_XYZ | D3DFVF_DIFFUSE ) // 3

/*
Vertex buffer initialization 
(crete array of 3D points)
-------------------------------
Передача параметра g_pVB по ссылчке, чтобы изменить значения
https://metanit.com/cpp/tutorial/3.3.php
*/
HRESULT InitVB(
	LPDIRECT3DDEVICE9 g_pd3dDevice, //3D device
	LPDIRECT3DVERTEXBUFFER9 &g_pVB, //pointer to empty bufffer
	int pointsCpount)               //vertex count are set hardcway 
{
	CUSTOMVERTEX vertices[] =
	{
		//2
		//{ 150.0f,  50.0f, 0.5f, 1.0f, 0xffff0000, }, // x, y, z, rhw, color
		//{ 250.0f, 250.0f, 0.5f, 1.0f, 0xff00ff00, },
		//{  50.0f, 250.0f, 0.5f, 1.0f, 0xff00ffff, }
		//3
		{ -1.0f,-1.0f, 0.0f, 0xffff0000, },
		{  1.0f,-1.0f, 0.0f, 0xff0000ff, },
		{  0.0f, 1.0f, 0.0f, 0xffffffff, },
	};

	//Create the vertex buffer
	if (FAILED(g_pd3dDevice->CreateVertexBuffer(pointsCpount * sizeof(CUSTOMVERTEX),
		0,
		D3DFVF_CUSTOMVERTEX,
		D3DPOOL_DEFAULT,
		&g_pVB,
		NULL)))
	{
		return E_FAIL;
	}

	//fill vertex buffer
	void* pVertices;

	if (FAILED(g_pVB->Lock(0, sizeof(pVertices), (void**)&pVertices, 0)))
	{
		return E_FAIL;
	}
	memcpy(pVertices, vertices, sizeof(vertices));//copy points to vertex buffer
	g_pVB->Unlock();

	return S_OK;
}