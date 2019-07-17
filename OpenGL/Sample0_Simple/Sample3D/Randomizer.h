#pragma once

#include <iostream>
#include <time.h>

class Randomizer
{
public:
	Randomizer() {}
	~Randomizer() { /*cout << "ГСЦ свое дело сделал и может ууходить." << endl;*/ }

	//Random INT
	int GetRandomInt(int min, int max)
	{
		srand(time(0));
		return min + rand() % max;
	}

	//Random FLOAT
	float GetRandomFloat(float min, float max)
	{
		srand(time(NULL));
		return static_cast <float> (rand()) / (static_cast <float> (RAND_MAX / max));
	}

	//Random DOUBLE
	double GetRandomDouble(double min, double max)
	{
		srand(time(0));
		return static_cast <double> (rand()) / (static_cast <double> (RAND_MAX / max));
	}
};

