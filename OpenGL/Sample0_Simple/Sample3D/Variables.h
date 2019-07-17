#pragma once
#include <list>
#include "Enemy.h"

 GLfloat xRotated, yRotated, zRotated;

GlobalCoordinator glCoorndinator;

GLfloat X, Y, Z;

//длинна капли дождя
GLfloat RainLenth = 0.05f;
//отображать дождь или нет
bool setRain = 0;

list<Enemy> EnemiesList;