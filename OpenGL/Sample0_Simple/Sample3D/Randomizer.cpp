#include "stdafx.h"
#include <stdlib.h>
#include "Randomizer.h"
#include <time.h>

using namespace std;

//Randomizer::Randomizer()
//{
//}
//
//
//Randomizer::~Randomizer()
//{
//}

int GetRandomInt(int min, int max)
{
	srand(time(0));
	return min + rand() % max;
}

float Randomizer::GetRandomFloat(float min, float max)
{
	srand(time(0));
	return static_cast <float> (rand()) / (static_cast <float> (RAND_MAX / max));
}

double Randomizer::GetRandomDouble(double min, double max)
{
	srand(time(0));
	return static_cast <double> (rand()) / (static_cast <double> (RAND_MAX / max));
}
