#pragma once
#include <d3dx9.h>

LPD3DXMESH g_pMesh = NULL;							 // mesh
LPD3DXMESH &ptr_g_pMesh = g_pMesh;					 // pointer to mesh

D3DMATERIAL9* g_pMeshMaterials = NULL;				//material for mesh
LPDIRECT3DTEXTURE9* g_pMeshTextures = NULL;			//textures for mesh

DWORD g_dwNumMaterials = 0;							//number of mesh materials