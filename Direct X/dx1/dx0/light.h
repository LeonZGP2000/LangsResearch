#pragma once
#include <windows.h>
#include <mmsystem.h>
#include <d3d9.h>

void SetupLights(LPDIRECT3DDEVICE9 &g_pd3dDevice) //yellow
{
	D3DMATERIAL9 mtrl;
	ZeroMemory(&mtrl, sizeof(D3DMATERIAL9));

	mtrl.Diffuse.r = mtrl.Ambient.r = 1.0f;
	mtrl.Diffuse.g = mtrl.Ambient.g = 1.0f;
	mtrl.Diffuse.b = mtrl.Ambient.b = 0.0f;
	mtrl.Diffuse.a = mtrl.Ambient.a = 1.0f;

	g_pd3dDevice->SetMaterial(&mtrl);

	//light
	D3DXVECTOR3 vecDir;
	D3DLIGHT9 light;
	ZeroMemory(&light, sizeof(D3DLIGHT9));

	light.Type = D3DLIGHT_DIRECTIONAL;
	light.Diffuse.r = 1.0f;
	light.Diffuse.g = 1.0f;
	light.Diffuse.b = 1.0f;

	vecDir = D3DXVECTOR3(cosf(timeGetTime() / 350.0f), 1.0f,
		sinf( timeGetTime() / 350.0f));

	D3DXVec3Normalize( (D3DXVECTOR3*) &light.Direction, &vecDir);

	light.Range = 1000.0f;
	g_pd3dDevice->SetLight(0, &light);
	g_pd3dDevice->LightEnable(0, TRUE);
	g_pd3dDevice->SetRenderState( D3DRS_LIGHTING,  TRUE );

	//ambient -> on
	g_pd3dDevice->SetRenderState(D3DRS_AMBIENT, 0x00202020);
}