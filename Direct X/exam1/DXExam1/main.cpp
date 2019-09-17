#include "WinApi32.h"

/*
	Start point
*/
INT WINAPI wWinMain(
	HINSTANCE hInst,  
	HINSTANCE,        
	LPWSTR,
	INT)
{
	HINSTANCE &ptrhInst = hInst;
	return CreateWin32Windows(ptrhInst, NULL, NULL, NULL);
}
