#include <windows.h>
#include "Scenario.h"
#include "DXGlobal.h"

const char* winName = "DXExam1";
const char* winTitle = "DX Exam 1 : Basics";

int winWeigth = 500;
int winHeigth = 500;
int winX = 100;
int winY = 100;

void CalculateFormPosition(int& winX, int& winY);
LRESULT WINAPI MsgProc(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam);

/*
	Create Win-32 window
*/
INT CreateWin32Windows(HINSTANCE &hInst, HINSTANCE,	LPWSTR,	INT)
{
	UNREFERENCED_PARAMETER(hInst);

	WNDCLASSEX wc =
	{
		sizeof(WNDCLASSEX), CS_CLASSDC, MsgProc, 0L, 0L,
		GetModuleHandle(NULL), NULL, NULL, NULL, NULL,
		winName, NULL
	};
	RegisterClassEx(&wc);

	//get window coords
	CalculateFormPosition(winX, winY);

	HWND hWnd = CreateWindow(winName, winTitle, WS_OVERLAPPEDWINDOW, 
		winX, winY, winWeigth, winHeigth,
		NULL, NULL, wc.hInstance, NULL);
	HWND &ptrhWnd = hWnd;

	// Init Direct3D
	if (SUCCEEDED(InitD3D(ptrhWnd)))
	{
		//if (SUCCEEDED(InitGeometry(ptr_dxDevice,ptr_verticleBuffer)))
		//{
			ShowWindow(hWnd, SW_SHOWDEFAULT);
			UpdateWindow(hWnd);

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
		//}
	}

	UnregisterClass(winName, wc.hInstance);
	return 0;
}

/*
	Get coords of center of the screen
*/
void CalculateFormPosition(int& winX, int& winY)
{
	RECT desktop;
	const HWND hDesktop = GetDesktopWindow();
	GetWindowRect(hDesktop, &desktop);

	long xMax = desktop.right;
	long yMax = desktop.bottom;
	
	winX = xMax / 2 - winWeigth / 2;
	winY = yMax / 2 - winHeigth / 2;
}

/*
	Window's message handler
*/
LRESULT WINAPI MsgProc(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	if (msg == WM_DESTROY)
	{
		CleanUp();
		PostQuitMessage(0);
		return 0;
	}

	return DefWindowProc(hWnd, msg, wParam, lParam);
}