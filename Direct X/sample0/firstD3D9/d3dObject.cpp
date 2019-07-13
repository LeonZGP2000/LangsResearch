// d3dObject.cpp
// http://www.gamedev.ru/
// 3D объект - Куб.
// Урок: http://www.gamedev.ru/articles/read.shtml?id=10102
// wat (wat@gamedev.ru)


#include <math.h>
#include <d3d9.h>

#include "d3dObject.h"

void SetWorld();

extern IDirect3DDevice9 * pDevice;

IDirect3DVertexBuffer9 * pVB;
IDirect3DIndexBuffer9 * pIB;

// количество вершин
int NumVerts = 24;
// количество треугольников
int NumTriangles = 12;
// количество индексов.
int NumInds = 36;



bool CreateObject()
{
  const MyVertex pMyVerts[] = {
    -1.0f, -1.0f, -1.0f, 0xffffffff,
    -1.0f,  1.0f, -1.0f, 0xffffffff,
     1.0f,  1.0f, -1.0f, 0xffffffff,
     1.0f, -1.0f, -1.0f, 0xffffffff,

     1.0f, -1.0f, -1.0f, 0xff00ffff,
     1.0f,  1.0f, -1.0f, 0xff00ffff,
     1.0f,  1.0f,  1.0f, 0xff00ffff,
     1.0f, -1.0f,  1.0f, 0xff00ffff,

     1.0f, -1.0f,  1.0f, 0xffffff00,
     1.0f,  1.0f,  1.0f, 0xffffff00,
    -1.0f,  1.0f,  1.0f, 0xffffff00,
    -1.0f, -1.0f,  1.0f, 0xffffff00,

    -1.0f, -1.0f,  1.0f, 0xffff0000,
    -1.0f,  1.0f,  1.0f, 0xffff0000,
    -1.0f,  1.0f, -1.0f, 0xffff0000,
    -1.0f, -1.0f, -1.0f, 0xffff0000,

    -1.0f,  1.0f, -1.0f, 0xff00ff00,
    -1.0f,  1.0f,  1.0f, 0xff00ff00,
     1.0f,  1.0f,  1.0f, 0xff00ff00,
     1.0f,  1.0f, -1.0f, 0xff00ff00,

     1.0f, -1.0f, -1.0f, 0xff0000ff,
     1.0f, -1.0f,  1.0f, 0xff0000ff,
    -1.0f, -1.0f,  1.0f, 0xff0000ff,
    -1.0f, -1.0f, -1.0f, 0xff0000ff,
  };
  
  const unsigned short pInds[]={
    0,1,3,3,1,2,
    4,5,7,7,5,6,
    8,9,11,11,9,10,
    12,13,15,15,13,14,
    16,17,19,19,17,18,
    20,21,23,23,21,22,
  };

  // вспомогательные переменные
  HRESULT hr;
  void * pBuf;

  // создание вершинного буфера
  hr = pDevice->CreateVertexBuffer( sizeof(MyVertex) * NumVerts,
       0 , D3DFVF_MYVERTEX, D3DPOOL_DEFAULT, &pVB, 0 );
  if( FAILED(hr) )
    return false;

  // заполнение вершинного буфера
  hr = pVB->Lock( 0, sizeof(MyVertex) * NumVerts, &pBuf, 0 );
  if( FAILED(hr) )
    return false;
  memcpy( pBuf, pMyVerts, sizeof(MyVertex) * NumVerts);
  pVB->Unlock();

  // создание индексного буфера
  hr = pDevice->CreateIndexBuffer( sizeof(short) * NumInds, 
       0, D3DFMT_INDEX16, D3DPOOL_DEFAULT,&pIB, 0);
  if( FAILED(hr) )
    return false;

  // заполнение индексного буфера
  hr = pIB->Lock( 0, sizeof(short) * NumInds, &pBuf, 0 );
  if( FAILED(hr) )
    return false;
  memcpy( pBuf, pInds, sizeof(short) * NumInds);
  pIB->Unlock();

  return true;
}

void DrawObject()
{
  // выставляем матрицу для объекта в мировых координатах
  SetWorld();

  // устанавливаем вершинный формат
  pDevice->SetFVF( D3DFVF_MYVERTEX );
  // устанавливаем вершинный буфер
  pDevice->SetStreamSource( 0, pVB, 0, sizeof(MyVertex) );

  // устанавливаем индексный буфер
  pDevice->SetIndices(pIB);

  // вывод объекта
  pDevice->DrawIndexedPrimitive(D3DPT_TRIANGLELIST, 0, 0, NumVerts, 0, NumTriangles);
}

void DeleteObject()
{
  if(pVB)
    pVB->Release();
  if(pIB)
    pIB->Release();

  pVB = 0;
  pIB = 0;
}


void SetWorld()
{
  float b = float(GetTickCount()%0xffff)/0xffff * 3.141593f * 80;

  float cs = (float)cos(b);
  float sn = (float)sin(b);

  float cs2 = (float)cos(b/2);
  float sn2 = (float)sin(b/2);

  D3DMATRIX World = {
    cs*cs2, cs*sn2, sn, 0,
     -sn2, cs2,  0, 0,
   -sn*cs2, -sn*sn2, cs, 0,
     0, 0,  0, 1,
  };

  pDevice->SetTransform(D3DTS_WORLD, &World);
}
