#include <d3d9.h>
#include <windows.h>
#include "Customs.h"

#define drawFVF (D3DFVF_XYZ|D3DFVF_DIFFUSE)

DWORD defaultColor = 0xfff0ff0f;
DWORD cube2Color   = 0xffff0000;
DWORD cube3Color   = 0xff00ff00;

const char* cube1textureName   = "cubeTexture";
const char* sphere1textureName = "sp1Texture";
const char* sphere2textureName = "sp2Texture";

/*
	Create object
	http://www.directxtutorial.com/Lesson.aspx?lessonid=9-4-7
*/
void CreateCube(
	LPDIRECT3DDEVICE9 &ptr_dxDevice, 
	LPDIRECT3DINDEXBUFFER9 &ptr_indexBuffer, 
	LPDIRECT3DVERTEXBUFFER9 &ptr_vertexBuffer,
	int cubeNumber
)
{
	// create the vertices using the CUSTOMVERTEX struct
	CUSTOMVERTEX vertices[] =
	{
		{ -0.2f,  0.2f, -0.2f, 0xffff0000,},
		{  0.2f,  0.2f, -0.2f, 0xff00ff00,},
		{ -0.2f, -0.2f, -0.2f, 0xffff0000,},
		{  0.2f, -0.2f, -0.2f, 0xff00ff00,},
		{ -0.2f,  0.2f,  0.2f, 0xffff0000,},
		{  0.2f,  0.2f,  0.2f, 0xff00ff00,},
		{ -0.2f, -0.2f,  0.2f, 0xffff0000,},
		{  0.2f, -0.2f,  0.2f, 0xff00ff00,},
	};

	// create a vertex buffer interface called v_buffer
	ptr_dxDevice->CreateVertexBuffer(
		8 * sizeof(CUSTOMVERTEX),
		0,
		drawFVF,
		D3DPOOL_MANAGED,
		&ptr_vertexBuffer,
		NULL);

	VOID* pVoid;    // a void pointer

	// lock v_buffer and load the vertices into it
	ptr_vertexBuffer->Lock(0, 0, (void**)&pVoid, 0);
	memcpy(pVoid, vertices, sizeof(vertices));
	ptr_vertexBuffer->Unlock();

	// create the indices using an int array
	short indices[] =
	{
		0, 1, 2,    // side 1
		2, 1, 3,
		4, 0, 6,    // side 2
		6, 0, 2,
		7, 5, 6,    // side 3
		6, 5, 4,
		3, 1, 7,    // side 4
		7, 1, 5,
		4, 5, 0,    // side 5
		0, 5, 1,
		3, 7, 2,    // side 6
		2, 7, 6,
	};

	// create an index buffer interface called i_buffer
	ptr_dxDevice->CreateIndexBuffer(36 * sizeof(short),
		0,
		D3DFMT_INDEX16,
		D3DPOOL_MANAGED,
		&ptr_indexBuffer,
		NULL);


	// lock i_buffer and load the indices into it
	ptr_indexBuffer->Lock(0, 0, (void**)&pVoid, 0);
	memcpy(pVoid, indices, sizeof(indices));
	ptr_indexBuffer->Unlock();

	ptr_dxDevice->SetFVF(drawFVF);

	// select the vertex and index buffers to use
	ptr_dxDevice->SetStreamSource(0, vertexBuffer, 0, sizeof(CUSTOMVERTEX));
	ptr_dxDevice->SetIndices(ptr_indexBuffer);

	// draw the cube
	ptr_dxDevice->DrawIndexedPrimitive(D3DPT_TRIANGLELIST, 0, 0, 8, 0, 12);
}

/*
	3D grid
*/
void CreateGrid(LPDIRECT3DDEVICE9 &ptr_dxDevice)
{
	line_vertex lines[] =
	{
		//x
		{-0.5, 0, 0, 0xffff0000},
		{ 0.5, 0, 0, 0xffff0000},

		{ 0, 0, 0,  0xffff0000},

		//y
		{ 0, -0.5, 0, 0xff0000ff},
		{ 0,  0.5, 0, 0xff0000ff},

		{ 0, 0, 0, 0xff0000ff},

		//z
		{ 0, 0, -0.5, 0xffffffff},
		{ 0, 0,  0.5, 0xffffffff}
	};

	// in the render function
	ptr_dxDevice->SetFVF(drawFVF);
	ptr_dxDevice->DrawPrimitiveUP(D3DPT_LINESTRIP, 7, lines, sizeof(line_vertex));
}

void CreateSphere(LPDIRECT3DDEVICE9 &ptr_dxDevic)
{
	//LPD3DXMESH mesh = NULL;
//	LPD3DXBUFFER buff = NULL;
	//1
	//HRESULT s1 = D3DXCreateSphere(ptr_dxDevic, 1, 16, 32, &ptr_mesh, &ptr_buff);
	//ptr_dxDevic->DrawIndexedPrimitive(D3DPT_TRIANGLEFAN, 1, 0, 20, 0, 10);

http://www.directxtutorial.com/LessonList.aspx?listid=9
}