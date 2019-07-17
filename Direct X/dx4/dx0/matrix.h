#pragma once
#include <d3dx9.h>

/*
3. Matrix transforms:
 - world
 - view
 - projection
*/
void SetupMatrices(LPDIRECT3DDEVICE9 &g_pd3dDevice)
{
	//worls matrix
	D3DXMATRIXA16 matWorld;
	D3DXMatrixIdentity(&matWorld);
	D3DXMatrixRotationX(&matWorld, timeGetTime() / 500.0f);
	g_pd3dDevice->SetTransform(D3DTS_WORLD, &matWorld);

	//view matrix (eye point)
	D3DXVECTOR3 vEyePt(0.0f, 3.0f, -5.0f);
	D3DXVECTOR3 vLookAt(0.0f, 0.0f, 0.0f);
	D3DXVECTOR3 vUpVec(0.0f, 1.0f, 0.0f);

	D3DXMATRIXA16 matView;
	D3DXMatrixLookAtLH(&matView, &vEyePt, &vLookAt, &vUpVec);
	g_pd3dDevice->SetTransform(D3DTS_VIEW, &matView );

	//projection matrix (Perspective)
	//convert 3D -> 2D
	D3DXMATRIXA16 matProj;
	D3DXMatrixPerspectiveFovLH(&matProj, D3DX_PI / 4, 1.0f, 1.0f, 100.0f);
	g_pd3dDevice->SetTransform( D3DTS_PROJECTION, &matProj );

}