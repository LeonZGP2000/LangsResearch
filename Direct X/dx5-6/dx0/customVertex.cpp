//#include "customVertex.h"
//#include <d3d9.h>
//
//struct CUSTOMVERTEX
//{
//	float x, y, z, rhw;  // coords + transformed position for the vertex
//	DWORD color;         // color
//};
//
////FVF vertex 
//#define D3DFVF_CUSTOMVERTEX (D3DFVF_XYZRHW | D3DFVF_DIFFUSE )
//
//HRESULT InitVB(LPDIRECT3DDEVICE9 g_pd3dDevice, LPDIRECT3DVERTEXBUFFER9 g_pVB, int pointsCpount)
//{
//	CUSTOMVERTEX vertices[] =
//	{
//		{ 150.0f,  50.0f, 0.5f, 1.0f, 0xffff0000, }, // x, y, z, rhw, color
//		{ 250.0f, 250.0f, 0.5f, 1.0f, 0xff00ff00, },
//		{  50.0f, 250.0f, 0.5f, 1.0f, 0xff00ffff, }
//	};
//
//	//Create the vertex buffer
//	if (FAILED(g_pd3dDevice->CreateVertexBuffer(pointsCpount * sizeof(CUSTOMVERTEX),
//		0,
//		D3DFVF_CUSTOMVERTEX,
//		D3DPOOL_DEFAULT,
//		&g_pVB,
//		NULL)))
//	{
//		return E_FAIL;
//	}
//
//	//fill vertex buffer
//	void* pVertices;
//
//	if (FAILED(g_pVB->Lock(0, sizeof(pVertices), (void**)&pVertices, 0)))
//	{
//		return E_FAIL;
//	}
//	memcpy(pVertices, vertices, sizeof(vertices));
//	g_pVB->Unlock();
//
//	return S_OK;
//}