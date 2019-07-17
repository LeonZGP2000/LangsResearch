#pragma once
#include <d3dx9.h>

void SetTextures(LPDIRECT3DDEVICE9 &g_pd3dDevice, LPDIRECT3DTEXTURE9 &ptr_g_pTexture)
{
	g_pd3dDevice->SetTexture(0, ptr_g_pTexture);
	g_pd3dDevice->SetTextureStageState(0, D3DTSS_COLOROP, D3DTOP_MODULATE);
	g_pd3dDevice->SetTextureStageState(0, D3DTSS_COLORARG1, D3DTA_TEXTURE);
	g_pd3dDevice->SetTextureStageState(0, D3DTSS_COLORARG2, D3DTA_DIFFUSE);
	g_pd3dDevice->SetTextureStageState(0, D3DTSS_ALPHAOP, D3DTOP_DISABLE);

#ifdef SHOW_HOW_TO_USE_TCI
	D3DXMATRIXA16 mTextureTransform;
	D3DXMATRIXA16 mProj;
	D3DXMATRIXA16 mTrans;
	D3DXMATRIXA16 mScale;

	g_pd3dDevice->GetTransform(D3DTS_PROJECTION, &mProj);
	D3DXMatrixTranslation(&mTrans, 0.5f, 0.5f, 0.0f);
	D3DXMatrixScaling(&mScale, 0.5f, -0.5f, 1.0f);
	mTextureTransform = mProj * mScale * mTrans;

	g_pd3dDevice->SetTransform(D3DTS_TEXTURE0, &mTextureTransform);
	g_pd3dDevice->SetTextureStageState(0, D3DTSS_TEXTURETRANSFORMFLAGS, D3DTTFF_COUNT4 | D3DTTFF_PROJECTED);
	g_pd3dDevice->SetTextureStageState(0, D3DTSS_TEXCOORDINDEX, D3DTSS_TCI_CAMERASPACEPOSITION);

#endif
}