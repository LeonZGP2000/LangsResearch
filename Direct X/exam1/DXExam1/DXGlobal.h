#include <d3d9.h>
#include "Matrix.h"


LPDIRECT3D9       dxObject = NULL;			 // IDirect3D9
LPDIRECT3DDEVICE9 dxDevice = NULL;			 // 3D Device
LPDIRECT3DDEVICE9 &ptr_dxDevice = dxDevice;  // pointer to 3D Device

//FVF vertex --
//#define D3DFVF_CUSTOMVERTEX (D3DFVF_XYZ | D3DFVF_DIFFUSE )

void Scene(LPDIRECT3DDEVICE9 &ptr_dxDevice, LPDIRECT3DINDEXBUFFER9 &ptr_indexBuffer, LPDIRECT3DVERTEXBUFFER9 &ptr_vertexBuffer);

D3DCOLOR winBackgroundColor = D3DCOLOR_XRGB(0, 0, 0);
//D3DCOLOR_XRGB(0, 0, 255);

/*
	Init 3D
*/
HRESULT InitD3D(HWND &hWnd)
{
	dxObject = Direct3DCreate9(D3D_SDK_VERSION);
	if (dxObject == NULL)
		return E_FAIL;

	D3DPRESENT_PARAMETERS d3dpp;
	ZeroMemory(&d3dpp, sizeof(d3dpp));
	d3dpp.Windowed = true;
	d3dpp.SwapEffect = D3DSWAPEFFECT_DISCARD;
	d3dpp.BackBufferFormat = D3DFMT_UNKNOWN;
	//d3dpp.EnableAutoDepthStencil = TRUE;
	//d3dpp.AutoDepthStencilFormat = D3DFMT_D16;

	//create 3D Device
	if (FAILED(dxObject->CreateDevice(D3DADAPTER_DEFAULT,
		D3DDEVTYPE_HAL, hWnd,
		D3DCREATE_SOFTWARE_VERTEXPROCESSING,
		&d3dpp, &dxDevice)))
	{
		return E_FAIL;
	}

	dxDevice->SetRenderState(D3DRS_LIGHTING, FALSE);

	return S_OK;
}

void CleanUp()
{
	//if (g_pMeshMaterials != NULL)
	//	delete[] g_pMeshMaterials;

	//if (g_pMeshTextures)
	//{
	//	for (DWORD i = 0; i < g_dwNumMaterials; i++)
	//	{
	//		if (g_pMeshTextures[i] != NULL)
	//			g_pMeshTextures[i]->Release();
	//	}
	//}

	//if (g_pMesh != NULL)
	//	g_pMesh->Release();

	if (ptr_indexBuffer != NULL)
		ptr_indexBuffer->Release();

	if (ptr_vertexBuffer != NULL)
		ptr_vertexBuffer->Release();

	if (dxDevice != NULL)
		dxDevice->Release();

	if (dxObject != NULL)
		dxObject->Release();
}

/*
Draw 3D
*/
void Render()
{
	//set фон colour is BLUE
	dxDevice->Clear(0, NULL, D3DCLEAR_TARGET | D3DCLEAR_ZBUFFER, winBackgroundColor, 1.0f, 0);

	//begin schene
	if (SUCCEEDED(dxDevice->BeginScene()))
	{
		//http://www.firststeps.ru/mfc/directx/dxhelp/r.php?33
		SetupMatrices(ptr_dxDevice);
		ptr_dxDevice->SetFVF(drawFVF);

		Scene(ptr_dxDevice, ptr_indexBuffer, ptr_vertexBuffer);

		dxDevice->EndScene();
	}

	//очистить экран/показать фон
	dxDevice->Present(NULL, NULL, NULL, NULL);
}

void Scene(LPDIRECT3DDEVICE9 &ptr_dxDevice, LPDIRECT3DINDEXBUFFER9 &ptr_indexBuffer, LPDIRECT3DVERTEXBUFFER9 &ptr_vertexBuffer)
{
	//grid
	//CreateGrid(ptr_dxDevice);

	//cubes
	//for (int i = 1; i <= 3, i++;)
	//{
	//	CreateCube(ptr_dxDevice, ptr_indexBuffer, ptr_vertexBuffer, i);
	//}
	CreateCube(ptr_dxDevice, ptr_indexBuffer, ptr_vertexBuffer, 1);

	//spheres
	CreateSphere(ptr_dxDevice);
	//https://docs.microsoft.com/ru-ru/windows/win32/direct3d9/dx9-graphics-reference-d3dx-functions-shape
}