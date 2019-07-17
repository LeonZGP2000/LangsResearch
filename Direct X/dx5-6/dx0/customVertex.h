#pragma once
#include <d3dx9.h>
#include "mesh.h"

// ************************** Geometry **************************

//5
HRESULT InitGeometry(
	LPDIRECT3DDEVICE9       g_pd3dDevice,    //3D device
	LPD3DXMESH              &ptr_g_pMesh,
	DWORD                   &g_dwNumMaterials)
{

	LPD3DXBUFFER pD3DXMtrlsBuffer;

	//laod mesh
	if (FAILED(D3DXLoadMeshFromX("Tiger.x", D3DXMESH_SYSTEMMEM, g_pd3dDevice, NULL, &pD3DXMtrlsBuffer, NULL, &g_dwNumMaterials, &ptr_g_pMesh)))
	{
		MessageBox(NULL, "Could not find  mesh file", "D3X - Textures", MB_OK);
		return E_FAIL;
	}

	D3DXMATERIAL* d3dxMaterials = (D3DXMATERIAL*)pD3DXMtrlsBuffer->GetBufferPointer();
	g_pMeshMaterials = new D3DMATERIAL9[g_dwNumMaterials];

	if (g_pMeshMaterials == NULL)
	{
		return E_OUTOFMEMORY;
	}
	g_pMeshTextures = new LPDIRECT3DTEXTURE9[g_dwNumMaterials];
	if (g_pMeshTextures == NULL)
	{
		return E_OUTOFMEMORY;
	}


	for (DWORD i = 0; i < g_dwNumMaterials; i++)
	{
		//copy of material
		g_pMeshMaterials[i] = d3dxMaterials[i].MatD3D;
		// Set the ambient color for the material (D3DX does not do this)
		g_pMeshMaterials[i].Ambient = g_pMeshMaterials[i].Diffuse;
		g_pMeshTextures[i] = NULL;

		if (d3dxMaterials[i].pTextureFilename != NULL && lstrlenA(d3dxMaterials[i].pTextureFilename) > 0)
		{
			//create texture !!!
			if (FAILED(D3DXCreateTextureFromFileA(g_pd3dDevice, d3dxMaterials[i].pTextureFilename, &g_pMeshTextures[i])))
			{
				MessageBox(NULL, "Could not find texture map", "Meshes.exe", MB_OK);
			}
		}

	}
	
	//finish working with buffer
	pD3DXMtrlsBuffer->Release();

	return S_OK;
}