#pragma once
#include <d3d9.h>

/*
3D Global parameters
*/
LPDIRECT3D9       g_pD3D = NULL;        //IDirect3D9
LPDIRECT3DDEVICE9 g_pd3dDevice = NULL;  //3D Device
LPDIRECT3DDEVICE9 &ptr_g_pd3dDevice = g_pd3dDevice;// pointer to 3D Device

//render scene
void Render();
//clean scene
void CleanUp();