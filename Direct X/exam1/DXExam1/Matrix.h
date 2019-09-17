#include <d3dx9.h>
#include <windows.h>


VOID SetupMatrices(LPDIRECT3DDEVICE9 &ptr_dxDevice)
{
	//FLOAT noRorationAngle = 0;
	//FLOAT rotionAngleUsual = (timeGetTime() % 1000) * (2.0f * D3DX_PI) / 1000.0f;

	D3DXMATRIXA16 matWorld;

	UINT iTime = timeGetTime() % 1000;
	FLOAT fAngle = iTime * (2.0f * D3DX_PI) / 1000.0f;
	D3DXMatrixRotationY(&matWorld, 70);  //30 70
	ptr_dxDevice->SetTransform(D3DTS_WORLD, &matWorld);

	D3DXVECTOR3 vEyePt(0.0f, 3.0f, -5.0f);
	D3DXVECTOR3 vLookatPt(0.0f, 0.0f, 0.0f);
	D3DXVECTOR3 vUpVec(0.0f, 1.0f, 0.0f);
	D3DXMATRIXA16 matView;
	D3DXMatrixLookAtLH(&matView, &vEyePt, &vLookatPt, &vUpVec);
	ptr_dxDevice->SetTransform(D3DTS_VIEW, &matView);

	D3DXMATRIXA16 matProj;
	D3DXMatrixPerspectiveFovLH(&matProj, D3DX_PI / 3, 1.0f, 1.0f, 100.0f);
	ptr_dxDevice->SetTransform(D3DTS_PROJECTION, &matProj);
}
