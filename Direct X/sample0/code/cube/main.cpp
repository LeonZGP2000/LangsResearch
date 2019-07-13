#pragma comment(lib,"d3d9.lib")

#include <windows.h>
#include "firstD3D9.h"

// func init
int Init(HWND hWndMain);
void ReleaseAll();
void OnIdle();
int Run();
HWND CreateMainWindow(LPCSTR pWindowName, int nWidth, int nHeight,
                      int nCmdShow=SW_NORMAL, bool bIsOpenGL=false);

namespace{
  // global variables
  HINSTANCE hInstance;
  HWND hWndMain;

  const char * szAppName = "DX";
}

// WinMain
int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance,
                   LPSTR lpCmdLine, int nCmdShow)
{
  hWndMain = CreateMainWindow("Direct X Sample",640,480);
  if(!hWndMain)
    return 0;

  if(!Init(hWndMain))
    return 0;
 
  int nRet = Run();

  ReleaseAll();
  return nRet;
}


int Init(HWND hWndMain)
{
  if(!InitD3D(hWndMain))
  {
    MessageBox(GetActiveWindow(),"Can't Initialise Direct3D9","GameDev.ru", MB_OK|MB_ICONSTOP);
    PostQuitMessage(0);
  }
  return 1;
}

void ReleaseAll()
{
  ReleaseD3D();
}


void OnIdle()
{
  DrawScene();
}


LRESULT WINAPI MainWndProc(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
  switch( msg )
  {
    case WM_LBUTTONDOWN:
      break;
    case WM_SIZE:
      break;
    case WM_DESTROY:
      PostQuitMessage( 0 );
      return 0;
  }
  return CallWindowProc( (WNDPROC)DefWindowProc, hWnd, msg, wParam, lParam );
}


HWND CreateMainWindow(LPCSTR pWindowName, int nWidth, int nHeight,
                      int nCmdShow, bool bIsOpenGL)
{
  WNDCLASS wndclass;
  wndclass.style         = 0;
  wndclass.lpfnWndProc   = (WNDPROC)MainWndProc;
  wndclass.cbClsExtra    = 0;
  wndclass.cbWndExtra    = 0;
  wndclass.hInstance     = hInstance;
  wndclass.hIcon         = LoadIcon (hInstance, szAppName);
  wndclass.hCursor       = LoadCursor (NULL,IDC_ARROW);
  wndclass.hbrBackground = (HBRUSH)(COLOR_WINDOW);
  wndclass.lpszMenuName  = szAppName;
  wndclass.lpszClassName = szAppName;

  if(!RegisterClass(&wndclass))
    return 0;

  DWORD dwStyle = WS_OVERLAPPEDWINDOW;
  if(bIsOpenGL)
    dwStyle |= WS_CLIPSIBLINGS | WS_CLIPCHILDREN;


  HWND hWnd = CreateWindow(szAppName, pWindowName, dwStyle,
                      (GetSystemMetrics(SM_CXSCREEN)-nWidth)/2,
                      (GetSystemMetrics(SM_CYSCREEN)-nHeight)/2,
                      nWidth, nHeight,
                      0, 0, hInstance, NULL);
  if(hWnd)
  {
    ShowWindow(hWnd, nCmdShow);
    UpdateWindow(hWnd);
  }
  return hWnd;
}

int Run()
{
  MSG msg={0};

  while(msg.message!=WM_QUIT)
  {
    if(PeekMessage(&msg, NULL, 0, 0, PM_NOREMOVE))
    {
      if(GetMessage(&msg, NULL, 0, 0))
      {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
      }
    }
    else
      OnIdle();
  }
  return msg.wParam;
}