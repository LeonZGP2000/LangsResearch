// firstD3D9.cpp
// http://www.gamedev.ru/
// Основы работы с Direct3D9
// Простейшее приложение Direct3D9.
// Урок: http://www.gamedev.ru/articles/read.shtml?id=10102
// wat (wat@gamedev.ru)

#include <d3d9.h>

#include "firstD3D9.h"
#include "d3dObject.h"


void SetProjecton();
void SetView();

IDirect3D9 * pD3D;
IDirect3DDevice9 * pDevice;


bool InitD3D(HWND hWnd)
{
  // Создание объекта Direct3D
  pD3D = Direct3DCreate9( D3D_SDK_VERSION);
  if( !pD3D)
    return false;
  
  // Создание устройства рендера.
  D3DPRESENT_PARAMETERS d3dpp = {0};
  d3dpp.Windowed = TRUE;
  d3dpp.SwapEffect = D3DSWAPEFFECT_DISCARD;
  d3dpp.BackBufferFormat = D3DFMT_UNKNOWN;
  HRESULT hr;
  hr = pD3D->CreateDevice( D3DADAPTER_DEFAULT, D3DDEVTYPE_HAL, hWnd,
                           D3DCREATE_HARDWARE_VERTEXPROCESSING,
                           &d3dpp, &pDevice );
  if( FAILED(hr) || !pDevice)
    return false;

  // создание 3D объекта
  CreateObject();

  // настройка рендера
  pDevice->SetRenderState(D3DRS_LIGHTING, FALSE);

  // настройка матриц трансформации
  SetView();
  SetProjecton();
  return true;
}



long DrawScene()
{
  pDevice->Clear( 0, NULL, D3DCLEAR_TARGET, D3DCOLOR_XRGB(0,0,128), 1.0f, 0 );
  pDevice->BeginScene();
  
  DrawObject();
  
  pDevice->EndScene();
  pDevice->Present( NULL, NULL, NULL, NULL );
  return 0;
}


void ReleaseD3D()
{
  DeleteObject();

  if(pDevice)
    pDevice->Release();
  if(pD3D)
    pD3D->Release();
}



void SetView()
{
  D3DMATRIX View = {
    1, 0, 0, 0,
    0, 0, 1, 0,
    0, -1, 0, 0,
    0, 0, 4, 1,
  };
  pDevice->SetTransform(D3DTS_VIEW, &View);
}

void SetProjecton()
{
  float Zn = 0.5f;
  float Zf = 100.0f;
  float Vw = 1;
  float Vh = 3.0f/4;

  D3DMATRIX Projection = {
    2*Zn/Vw, 0, 0, 0,
    0, 2*Zn/Vh, 0, 0,
    0, 0, Zf/(Zf-Zn), 1,
    0, 0, -Zf/(Zf-Zn)*Zn, 0,
  };

  pDevice->SetTransform(D3DTS_PROJECTION, &Projection);
}
