#include "d3global.h"
#include "global.h"
#include "customVertex.h"
#include "matrix.h"

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
	//moved in main loop in 2
	/*else if (msg == WM_PAINT)
	{
		Render();
		ValidateRect(hWnd, NULL);
		return 0;
	}*/
	
	//продолжить получить и обрабатывать остальные сообщения
	return DefWindowProc(hWnd, msg, wParam, lParam); //http://vsokovikov.narod.ru/New_MSDN_API/Window_procedure/fn_defwindowproc.htm
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

	//create 3D Device
	if (FAILED(g_pD3D->CreateDevice(D3DADAPTER_DEFAULT,
		D3DDEVTYPE_HAL, hWnd,
		D3DCREATE_SOFTWARE_VERTEXPROCESSING,
		&d3dpp, &g_pd3dDevice)))
	{
		return E_FAIL;
	}

	//3
	// Turn off culling, so we see the front and back of the triangle
	g_pd3dDevice->SetRenderState(D3DRS_CULLMODE, D3DCULL_NONE);
	// Turn off D3D lighting, since we are providing our own vertex colors
	g_pd3dDevice->SetRenderState(D3DRS_LIGHTING, FALSE);

	return S_OK;
}

/*
Releases all previously initialized objects (Before exit)
*/
void CleanUp()
{
	if (g_pd3dDevice != NULL)
		g_pd3dDevice->Release();

	if (g_pD3D != NULL)
		g_pD3D->Release();

	//vertex
	if (g_pD3D != NULL)
		g_pD3D->Release();
}

/*
Draw 3D
*/
void Render()
{
	if (g_pd3dDevice == NULL) return;

	//set фон colour is BLUE
	g_pd3dDevice->Clear(0,NULL, D3DCLEAR_TARGET, D3DCOLOR_XRGB(0, 0, 0), 1.0f, 0);

	//begin schene
	if (SUCCEEDED(g_pd3dDevice->BeginScene()))
	{
		//Setup matrixes! (3)
		SetupMatrices(ptr_g_pd3dDevice); //add rorations

		//render here (VERTEX BUFFER)
		g_pd3dDevice->SetStreamSource(0, g_pVB, 0, sizeof(CUSTOMVERTEX));
		g_pd3dDevice->SetFVF(D3DFVF_CUSTOMVERTEX);
		g_pd3dDevice->DrawPrimitive(D3DPT_TRIANGLELIST, 0, 1);

		//end scene
		g_pd3dDevice->EndScene();
	}

	//очистить экран/показать фон
	g_pd3dDevice->Present(NULL, NULL, NULL, NULL);


}

/*
wWinMain - https://dic.academic.ru/dic.nsf/ruwiki/377887
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
		int pointsCpount = 3;
		if (SUCCEEDED(InitVB(g_pd3dDevice, ptr_g_pVB, pointsCpount)))
		{
			// Show the window
			ShowWindow(hWnd, SW_SHOWDEFAULT);
			UpdateWindow(hWnd);

			// Enter the message loop
			MSG msg;
			ZeroMemory(&msg, sizeof(msg));
			//1 old
			//while (GetMessage(&msg, NULL, 0, 0))
			//{
			//	TranslateMessage(&msg);
			//	DispatchMessage(&msg);
			//}
			//2 new	
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
