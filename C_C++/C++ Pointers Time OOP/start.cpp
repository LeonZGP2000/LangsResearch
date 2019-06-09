
#include <iostream>
#include "Human_WorkOut.cpp"
#include "Time.cpp"
#include "Pointers.cpp"
//****************************************************************************
void Start_HumanSample()
{
	Human_WorkOut* h = new Human_WorkOut();
	h->WorkOut_Human();
}
//****************************************************************************
void Start_TimeSample()
{
	Time* t = new Time();
	t->Time_Go();
}
//****************************************************************************
void Start_PointerSample()
{
	Pointers* p = new Pointers();
	p->PointerStart();
}
//****************************************************************************

int main(int argc, char *argv[])
{
	//Human sample
	//Start_HumanSample();
	
	//Time sample
	//Start_TimeSample();
	
	//Pointers
	Start_PointerSample();
	
	return 0;
}



