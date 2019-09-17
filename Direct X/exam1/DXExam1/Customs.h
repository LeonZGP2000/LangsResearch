#include <d3d9.h>
#include <windows.h>

struct CUSTOMVERTEX
{
	FLOAT x, y, z;      
	DWORD color;        
};

struct line_vertex
{
	float x, y, z;
	DWORD color;
};

LPDIRECT3DINDEXBUFFER9  indexBuffer = NULL;
LPDIRECT3DVERTEXBUFFER9 vertexBuffer = NULL;
LPDIRECT3DINDEXBUFFER9 &ptr_indexBuffer = indexBuffer;
LPDIRECT3DVERTEXBUFFER9 &ptr_vertexBuffer = vertexBuffer;

