#include "d3global.h"
#include "global.h"
#include "customVertex.h"
#include "matrix.h"
#include "light.h"
#include "texture.h"
#include "mesh.h"

/*
Window's message handler
*/
LRESULT WINAPI MsgProc(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	if (msg == WM_DESTROY) //exit from app
	{
		CleanUp();
		PostQuitMessage(0);
		return 0;
	}

	return DefWindowProc(hWnd, msg, wParam, lParam);
}

/*
Create 3D Device
*/
HRESULT InitD3D(HWND hWnd)
{
	g_pD3D = Direct3DCreate9(D3D_SDK_VERSION);
	if ( g_pD3D == NULL)
		return E_FAIL;

	D3DPRESENT_PARAMETERS d3dpp;
	ZeroMemory(&d3dpp, sizeof(d3dpp));
	d3dpp.Windowed = true;
	d3dpp.SwapEffect = D3DSWAPEFFECT_DISCARD;
	d3dpp.BackBufferFormat = D3DFMT_UNKNOWN;
	d3dpp.EnableAutoDepthStencil = TRUE;
	d3dpp.AutoDepthStencilFormat = D3DFMT_D16;

	//create 3D Device
	if (FAILED(g_pD3D->CreateDevice(D3DADAPTER_DEFAULT,
		D3DDEVTYPE_HAL, hWnd,
		D3DCREATE_SOFTWARE_VERTEXPROCESSING,
		&d3dpp, &g_pd3dDevice)))
	{
		return E_FAIL;
	}

	// Turn on the zbuffer
	g_pd3dDevice->SetRenderState(D3DRS_ZENABLE, TRUE);
	// Turn on ambient lighting 
	g_pd3dDevice->SetRenderState(D3DRS_AMBIENT, 0xffffffff);

	return S_OK;
}

/*
Releases all previously initialized objects (Before exit)
*/
void CleanUp()
{
	if (g_pMeshMaterials != NULL)
		 delete[] g_pMeshMaterials;

	if (g_pMeshTextures)
	{
		for (DWORD i = 0; i < g_dwNumMaterials; i++)
		{
			if (g_pMeshTextures[i] != NULL)
				g_pMeshTextures[i]->Release();
		}
	}

	if (g_pMesh != NULL)
		g_pMesh->Release();

	if (g_pd3dDevice != NULL)
		g_pd3dDevice->Release();

	if (g_pD3D != NULL)
		g_pD3D->Release();
}

/*
Draw 3D
*/
void Render()
{
	//set фон colour is BLUE
	g_pd3dDevice->Clear(0,NULL, D3DCLEAR_TARGET | D3DCLEAR_ZBUFFER, D3DCOLOR_XRGB(0, 0, 255), 1.0f, 0);

	//begin schene
	if (SUCCEEDED(g_pd3dDevice->BeginScene()))
	{
		//matrix
		SetupMatrices(ptr_g_pd3dDevice); 

		for (DWORD i = 0; i < g_dwNumMaterials; i++)
		{
			//Set the material and texture for this subset
			g_pd3dDevice->SetMaterial(&g_pMeshMaterials[i]);
			g_pd3dDevice->SetTexture(0, g_pMeshTextures[i]);

			//draw mesh
			g_pMesh->DrawSubset(i);
		}

		//end scene
		g_pd3dDevice->EndScene();
	}

	//очистить экран/показать фон
	g_pd3dDevice->Present(NULL, NULL, NULL, NULL);
}

/*
Main
*/
INT WINAPI wWinMain(
	HINSTANCE hInst,  //хендл текущего экземпляра приложения. Фактически, базовый адрес загрузки основного EXE модуля.
	HINSTANCE,        //hPrevInstance - устарел, не используется в настоящее время.
					  //Раньше представлял собой предыдущий экземпляр приложения.Win32 - приложения должны использовать 
					  //CreateMutex для определения множественного запуска.
	LPWSTR,
	INT)
{
	UNREFERENCED_PARAMETER(hInst);

	// Register the window class
	WNDCLASSEX wc =
	{
		sizeof(WNDCLASSEX), CS_CLASSDC, MsgProc, 0L, 0L,
		GetModuleHandle(NULL), NULL, NULL, NULL, NULL,
		"D3 Sample", NULL
	};
	RegisterClassEx(&wc);

	// Create the application's window
	HWND hWnd = CreateWindow("D3 Sample", "D3 Sample. Device. Vertexes",
		WS_OVERLAPPEDWINDOW, 100, 100, 500, 600,
		NULL, NULL, wc.hInstance, NULL);

	// Initialize Direct3D
	if (SUCCEEDED(InitD3D(hWnd)))
	{		
		if (SUCCEEDED(InitGeometry(g_pd3dDevice, ptr_g_pMesh, g_dwNumMaterials)))
		{
			// Show the window
			ShowWindow(hWnd, SW_SHOWDEFAULT);
			UpdateWindow(hWnd);

			// Enter the message loop
			MSG msg;
			ZeroMemory(&msg, sizeof(msg));
			while (msg.message != WM_QUIT)
			{
				if (PeekMessage(&msg, NULL, 0U, 0U, PM_REMOVE))
				{
					TranslateMessage(&msg);
					DispatchMessage(&msg);
				}
				else
					Render();
			}
		}
	}

	UnregisterClass("D3 Sample", wc.hInstance);
	return 0;
}
