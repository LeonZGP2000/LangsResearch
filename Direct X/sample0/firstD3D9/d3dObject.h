
// описание формата вершины для объекта.
#define D3DFVF_MYVERTEX (D3DFVF_XYZ | D3DFVF_DIFFUSE)

struct MyVertex
{
  float x, y, z;
  DWORD color;
};

bool CreateObject();
void DrawObject();
void DeleteObject();