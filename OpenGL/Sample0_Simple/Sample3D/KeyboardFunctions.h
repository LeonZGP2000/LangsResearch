#pragma once
#include "stdafx.h"

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