#include "d3global.h"
#include "global.h"
#include "customVertex.h"
#include "matrix.h"
#include "light.h"
#include "texture.h"

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

	// Turn off culling, so we see the front and back of the triangle
	g_pd3dDevice->SetRenderState(D3DRS_CULLMODE, D3DCULL_NONE);
	// Turn off D3D lighting, since we are providing our own vertex colors
	g_pd3dDevice->SetRenderState(D3DRS_LIGHTING, FALSE);
	// Turn on the zbuffer
	g_pd3dDevice->SetRenderState(D3DRS_ZENABLE, TRUE);

	return S_OK;
}

/*
Releases all previously initialized objects (Before exit)
*/
void CleanUp()
{
	//textures  (5)
	if (g_pTexture != NULL)
		g_pTexture->Release();

	//vertex (3)
	if (g_pVB != NULL)
		g_pVB->Release();

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
		//light (not use in 5)
		//SetupLights(ptr_g_pd3dDevice);

		//matrix
		SetupMatrices(ptr_g_pd3dDevice); 

		//texture
		SetTextures(ptr_g_pd3dDevice, ptr_g_pTexture);

		//render here (VERTEX BUFFER)
		g_pd3dDevice->SetStreamSource(0, g_pVB, 0, sizeof(CUSTOMVERTEX));
		g_pd3dDevice->SetFVF(D3DFVF_CUSTOMVERTEX);
		g_pd3dDevice->DrawPrimitive(D3DPT_TRIANGLESTRIP, 0, 2*50-2);

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
		if (SUCCEEDED(InitGeometry(g_pd3dDevice, ptr_g_pVB, ptr_g_pTexture)))
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
