

#include "stdafx.h"
#include <iostream>
//#include <stdlib.h>

//opengl
#include <windows.h>
#include "DLLs/glut.h"
 
//helpers
#include "RGBColourFilter.h"
#include "XYZCoordinator.h"

//список
#include <list>
#include <vector>

//enemy
#include "Enemy.h"
//randomizer
#include "Randomizer.h"


using namespace std;

void SetSolidCone(int base, float height, int slices, int stacks, RGB_colour_filter colour);
void SetWireCone(int base, float height, int slices, int stacks, RGB_colour_filter colour);
void SetSolidCube(double size, RGB_colour_filter colour);
void SetWireCube(double size, RGB_colour_filter colour);
void SetSolidSphere(GLdouble radius, RGB_colour_filter colour);
void MoveCoordinator(float x, float y, float z);
void SetPlane();
void SetRain();


GLfloat xRotated, yRotated, zRotated;

GlobalCoordinator glCoorndinator;

GLfloat X, Y, Z;

//массив капель дождя. пока их три
vector<XYZCoordinate> myRainDots;
//длинна капли дождя
GLfloat RainLenth = 0.5f;
//отображать дождь или нет
bool setRain = 0;
//sddd
list<Enemy> EnemiesList;

void Initialize()
{
	glClearColor(0, 0, 0, 0);
}

void Draw()
{	
	//установка цвета для объекта
	RGB_colour_filter ColourFilter;

	glMatrixMode(GL_MODELVIEW);
	glClear(GL_COLOR_BUFFER_BIT);

	glLoadIdentity();
	glTranslatef(0.0, 0.0, -10.5);

	glRotatef(xRotated, 1.0, 0.0, 0.0);
	glRotatef(yRotated, 0.0, 1.0, 0.0);
	glRotatef(zRotated, 0.0, 0.0, 1.0);
	


	//glBegin(GL_QUADS);        // Draw The Cube Using quads
	//glColor3f(0.0f, 1.0f, 0.0f);    // Color Blue
	//glVertex3f(1.0f, 1.0f, -1.0f);    // Top Right Of The Quad (Top)
	//glVertex3f(-1.0f, 1.0f, -1.0f);    // Top Left Of The Quad (Top)
	//glVertex3f(-1.0f, 1.0f, 1.0f);    // Bottom Left Of The Quad (Top)
	//glVertex3f(1.0f, 1.0f, 1.0f);    // Bottom Right Of The Quad (Top)
	//glColor3f(1.0f, 0.5f, 0.0f);    // Color Orange
	//glVertex3f(1.0f, -1.0f, 1.0f);    // Top Right Of The Quad (Bottom)
	//glVertex3f(-1.0f, -1.0f, 1.0f);    // Top Left Of The Quad (Bottom)
	//glVertex3f(-1.0f, -1.0f, -1.0f);    // Bottom Left Of The Quad (Bottom)
	//glVertex3f(1.0f, -1.0f, -1.0f);    // Bottom Right Of The Quad (Bottom)
	//glColor3f(1.0f, 0.0f, 0.0f);    // Color Red    
	//glVertex3f(1.0f, 1.0f, 1.0f);    // Top Right Of The Quad (Front)
	//glVertex3f(-1.0f, 1.0f, 1.0f);    // Top Left Of The Quad (Front)
	//glVertex3f(-1.0f, -1.0f, 1.0f);    // Bottom Left Of The Quad (Front)
	//glVertex3f(1.0f, -1.0f, 1.0f);    // Bottom Right Of The Quad (Front)
	//glColor3f(1.0f, 1.0f, 0.0f);    // Color Yellow
	//glVertex3f(1.0f, -1.0f, -1.0f);    // Top Right Of The Quad (Back)
	//glVertex3f(-1.0f, -1.0f, -1.0f);    // Top Left Of The Quad (Back)
	//glVertex3f(-1.0f, 1.0f, -1.0f);    // Bottom Left Of The Quad (Back)
	//glVertex3f(1.0f, 1.0f, -1.0f);    // Bottom Right Of The Quad (Back)
	//glColor3f(0.0f, 0.0f, 1.0f);    // Color Blue
	//glVertex3f(-1.0f, 1.0f, 1.0f);    // Top Right Of The Quad (Left)
	//glVertex3f(-1.0f, 1.0f, -1.0f);    // Top Left Of The Quad (Left)
	//glVertex3f(-1.0f, -1.0f, -1.0f);    // Bottom Left Of The Quad (Left)
	//glVertex3f(-1.0f, -1.0f, 1.0f);    // Bottom Right Of The Quad (Left)
	//glColor3f(1.0f, 0.0f, 1.0f);    // Color Violet
	//glVertex3f(1.0f, 1.0f, -1.0f);    // Top Right Of The Quad (Right)
	//glVertex3f(1.0f, 1.0f, 1.0f);    // Top Left Of The Quad (Right)
	//glVertex3f(1.0f, -1.0f, 1.0f);    // Bottom Left Of The Quad (Right)
	//glVertex3f(1.0f, -1.0f, -1.0f);    // Bottom Right Of The Quad (Right)

	/////////////////////////////	//Отрисовка квадрата 
	//////glColor3f(1.0, 1.0, 1.0); //Выбираем белый цвет
	////////glBegin(GL_POLYGON);
	//////glVertex3f(1.25, 1.25, 0.0); //Координаты квадрата
	//////glVertex3f(1.75, 1.25, 0.0);
	//////glVertex3f(1.75, 1.75, 0.0);
	//////glVertex3f(1.25, 1.75, 0.0);

	//////glColor3f(1.0f, 0.0f, 0.0f);
	//////glVertex3f(1.25, 1.25, -1.5); //Координаты квадрата
	//////glVertex3f(1.75, 1.25, -1.5);
	//////glVertex3f(1.75, 1.75, -1.5);
	//////glVertex3f(1.25, 1.75, -1.5);

	//glEnd();            // End Drawing The Cube
/*
	//glutSwapBuffers();
	int base = 1;
	float height = 1.5;
	int slices = 50;
	int stacks = 50;

	//конус №  1
	MoveCoordinator(0.0, 0.0, -2.5);
	ColourFilter.R = 1.0f;
	ColourFilter.G = 0.1f;
	ColourFilter.B = 0.1f;
	SetSolidCone(base, height, slices, stacks, ColourFilter);

	//конус № 2
	MoveCoordinator(0.0, 0.0, -2.5);
	ColourFilter.R = 1.0f;
	ColourFilter.G = 1.0f;
	ColourFilter.B = 0.0f;
	SetWireCone(base, height, slices, stacks, ColourFilter);
	
	//конус № 3
	MoveCoordinator(0.0, 0.0, -2.5);
	ColourFilter.R = 0.1f;
	ColourFilter.G = 0.6f;
	ColourFilter.B = 1.0f;
	SetWireCone(base, height, slices, stacks, ColourFilter);
	*/

	glBegin(GL_LINES);

		SetPlane();

		//set rain
		//отобразить дождь, если необходимо
		if (setRain)
		{
			glColor3f(1.0, 0.0, 0.0);
			for (int i = 0; i < myRainDots.size(); i++)
			{
				XYZCoordinate c = myRainDots[i];
				//cout << "start [" << i<< "]: "<< c.X << ";" << c.Y << ";" <<c.Z << endl;

				//обновим высоту капли с учетом скорости падения
				c.Y -= 0.05f;

				if (myRainDots[i].Y < -3)
					myRainDots[i].Y = 5;

				myRainDots[i] = c;
				
				glVertex3f(c.X, c.Y, c.Z);
				glVertex3f(c.X, c.Y- RainLenth, c.Z);

				//cout << "start [" << i << "]: " << c.X << ";" << c.Y << ";" << c.Z << endl;

				//debug
				if (myRainDots[i].Y < -3)
				{
					cout << i << ":" << myRainDots[i].Y << endl;
					myRainDots[i].Y = 5;
				}
			}
		}
	glEnd();

	MoveCoordinator(X, Y, Z);

	ColourFilter.R = 0.1f;
	ColourFilter.G = 0.6f;
	ColourFilter.B = 0.4f;
	SetSolidCube(1, ColourFilter);

	/*
	//отобразим противников
	list <Enemy>::iterator c1_Iter;
	
	//цвет оппонентов - красный
	ColourFilter.R = 0.8f;
	ColourFilter.G = 0.1f;
	ColourFilter.B = 0.1f;

	for (c1_Iter = EnemiesList.begin(); c1_Iter != EnemiesList.end(); c1_Iter++)
	{
		//solidscheree //!!!!!!

		//установить в координаты

		//исправить ГСЦ

		//MoveCoordinator(c1_Iter.);
		//SetSolidSphere(0.09, ColourFilter);
	}
	*/



	glFlush();	
}

/*
	Сетка
*/
void SetPlane()
{
	//отобразить сетку

	//X
	glColor3f(1.0f, 0.0f, 1.0f);
	glVertex3f(-3, 0.0f, -1.0f);
	glVertex3f(+3, 0.0f, -1.0f);

	glVertex3f(-1, -1.0f, -1.0f);
	glVertex3f(+1, -1.0f, -1.0f);

	glVertex3f(-1, 1.0f, -1.0f);
	glVertex3f(+1, 1.0f, -1.0f);

	//Y
	glColor3f(0.0f, 1.0f, 0.0f);
	glVertex3f(0.0, -2.0f, -1.0f);
	glVertex3f(0.0, 2.0f, -1.0f);

	glVertex3f(-1.0, -1.0f, -1.0f);
	glVertex3f(-1.0, 1.0f, -1.0f);
	
	glVertex3f(1.0, -1.0f, -1.0f);
	glVertex3f(1.0, 1.0f, -1.0f);

}

void SetSolidCone(int base, float height, int slices, int stacks, RGB_colour_filter colour)
{
	glColor3f(colour.R, colour.G, colour.B);
	glutSolidCone(base, height, slices, stacks);
}

void SetWireCone(int base, float height, int slices, int stacks, RGB_colour_filter colour)
{
	glColor3f(colour.R, colour.G, colour.B);
	glutWireCone(base, height, slices, stacks);
}

void SetWireCube(double size, RGB_colour_filter colour)
{
	glColor3f(colour.R, colour.G, colour.B);
	glutWireCube(size);
}

void SetSolidCube(double size, RGB_colour_filter colour)
{
	glColor3f(colour.R, colour.G, colour.B);
	glutSolidCube(size);
}

void SetSolidSphere(GLdouble radius, RGB_colour_filter colour)
{
	glColor3f(colour.R, colour.G, colour.B);
	glutSolidSphere(radius, 15, 15);
}

/*
	Передвинуть указатель для создания нвоого объекта в определнной точке
*/
void MoveCoordinator(float x, float y, float z)
{
	glTranslatef(x, y, z);

	glCoorndinator.X += x;
	glCoorndinator.Y += y;
	glCoorndinator.Z += z;

	//cout << "Координаты относительно 0/0/0: [" << glCoorndinator.X  <<  ";" << glCoorndinator.Y <<";" << glCoorndinator.Z << "]" <<endl;
}

/*
	Анимация 
*/
void animation(void)
{

	//yRotated += 0.01;
	//xRotated += 0.02;
	Draw();
	//Sleep(1000 / 30);
}

void reshape(int x, int y)
{
	if (y == 0 || x == 0) return;  //Nothing is visible then, so return
								   //Set a new projection matrix
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	//Angle of view:40 degrees
	//Near clipping plane distance: 0.5
	//Far clipping plane distance: 20.0

	gluPerspective(40.0, (GLdouble)x / (GLdouble)y, 0.5, 20.0);
	glMatrixMode(GL_MODELVIEW);
	glViewport(0, 0, x, y);  //Use the whole window for rendering
}

/*
	MOUSE
*/
void mause(int buton1, int buton2, int x, int y)
{
	if (buton1 == GLUT_LEFT_BUTTON)
	{
		
		//if (buton2 == 0)
		//	cout << "ЛКМ Нажато" << endl;
		//else if (buton2 == 1)
		//	cout << "ЛКМ Отжато" << endl;
		//else
		//	cout << "ЛКМ state :" << buton2 << endl;

		cout << "Нажата левая кнопка  мыши" << endl;
		//X = x;
		//Y = y;
		//Z += 0.001f;

		yRotated += 0.5;
		xRotated += 0.5;
	}
	else if (buton1 == GLUT_MIDDLE_BUTTON)
	{
		cout << "Нажата средняя кнопка  мыши" << endl;
	}
	else if (buton1 == GLUT_RIGHT_BUTTON)
	{
		cout << "Нажата правая кнопка  мыши" << endl;
		//Z -= 0.001f;
		yRotated -= 0.5;
		xRotated -= 0.5;
	}
	else
		cout << "Нажата НЕОПРЕДЕЛЕННА кнопка  мыши" << endl;

}

/*
	KEYBOARD
*/
void keyboardArrows(int key, int x, int y)
{
	if (key == 27)
		exit(0);
	else if (key == (unsigned char)GLUT_KEY_UP) //up
	{
		cout << "btn -> UP (" << Y << ")" << endl;
		//Z += 0.05f;
		if (Y < 3)
			Y += 0.05f;
	}
	else if (key == GLUT_KEY_DOWN) //down
	{
		cout << "btn -> DOWN(" << Y << ")" << endl;
		//Z -= 0.05f;
		if (Y > -3)
		Y -= 0.05f;
	}
	else if (key == GLUT_KEY_LEFT) //left	
	{ 
		cout << "btn -> LEFT" << endl;
		if (X > -5)
		X -= 0.05f;
	}
	else if (key == GLUT_KEY_RIGHT) //right
	{
		cout << "btn -> RIGHT" << endl;
		if (X < 5)
		X += 0.05f;
	}
}

void keyboard(unsigned char key, int x, int y)
{
	if (key == 27)
		exit(0);
	else if (key == 13)
	{ 
		//cout << "enter" << endl;
		if (setRain == true)
			setRain = false;
		else
			setRain = true;
	}
}
/*
	Generate opponents
*/
void GenerateEnemies(int max)
{
	EnemiesList.clear();

	Randomizer r;

	for (size_t i = 0; i < max; i++)
	{
		/*ИСПРАВИТЬ Генерацию случайных чисел - сейчас одинаковые данные постоянно*/
		Enemy e;
		e.X = r.GetRandomFloat(-3, 3);
		e.Y = r.GetRandomFloat(-3, 3);
		e.Z = r.GetRandomFloat(-3, 3);

		e.EnemyColour.R = r.GetRandomFloat(0, 1);
		e.EnemyColour.G = r.GetRandomFloat(0, 1);
		e.EnemyColour.B = r.GetRandomFloat(0, 1);

		//добавляем в коллекцию
		EnemiesList.push_back(e);
	}
}


/*
Установка по дождю
*/
void SetRain()
{
	// 3 капли, можно больше :)

	XYZCoordinate coord;

	myRainDots.reserve(3);

	coord.X = 1.0f;
	coord.Y = 5.0f;
	coord.Z = 3.0f;

	myRainDots.push_back(coord);

	coord.X = -1.0f;
	coord.Y = 5.0f;
	coord.Z = 3.0f;

	myRainDots.push_back(coord);

	coord.X = 3.0f;
	coord.Y = 5.0f;
	coord.Z = 2.0f;

	myRainDots.push_back(coord);

	coord.X = 0.0f;
	coord.Y = 4.0f;
	coord.Z = 1.5f;

	myRainDots.push_back(coord);
}

int main(int argc, char **argv)
{
	//работа с координаторами

	setlocale(LC_ALL, "rus");
	cout << "OpenGL" <<endl;//system("pause");

	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_SINGLE | GLUT_RGB);
	//GLUT_SINGLE
	glutInitWindowSize(800, 600);		//Указываем размер окна
	glutInitWindowPosition(100, 100);	//Позиция окна
	int id = glutCreateWindow("Polygon");	cout <<"id: " << id  << endl;	//Имя окна
	
	Initialize();						//Вызов функции Initialize
	glutSetCursor(GLUT_CURSOR_CROSSHAIR);

	//game mode
	//glutGameModeString("300x300:32"); // For full screen game mode
	//glutEnterGameMode();

	//генерация оппонентов
	GenerateEnemies(4);

	//RAIN !!!!!!
	SetRain();

	//мышка
	glutMouseFunc(mause);
	//клавиатура
	glutKeyboardFunc(keyboard);
	//клавиатура- move...
	glutSpecialFunc(keyboardArrows);
	glutReshapeFunc(reshape);
	glutDisplayFunc(Draw);				//Вызов функции отрисовки
										//Set the function for the animation.
	glutIdleFunc(animation);

	glutMainLoop();

    return 0;

}



