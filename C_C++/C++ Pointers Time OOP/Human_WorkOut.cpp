
#include <iostream>
//#include "stdafx.h"
#include "Human.cpp"
#include <typeinfo>

using namespace::std;

class Human_WorkOut
{
public:
/*
Запустить метод Go() на объекте
*/
void ShowObjectInfo(Human* h){
	h->Go();
}

void WorkOut_Human()
{
		//localty:
	setlocale(LC_ALL, "rus");
	
	//params:
	Human* h;
	Men* m;
	
	
	h = new Human();
	h->age = 150;
	h->name = "Иван";
	h->famName= "Кот";
	ShowObjectInfo(h);
	cout<< h->GetInfo(h) << endl;
	
	//наследование
	m = new Men("green");
	ShowObjectInfo(m);
	cout<< "man's name: (наследование)" << m->name << endl;
	
	//вызвать метод Го  из базового класса:
	cout<< "Вызовем метод Go() из базового класса"<< endl;
	std::string type1 =  typeid(m).name();
	std::string type2 =  typeid(((Human*)m)).name();
	
	cout<< "type1: " << type1 <<endl;
	cout<< "type2: " << type2 <<endl;
	
	//1: ((Human*)m)->Go() ;	 //Men goes
	/*2:Human* hh = (Human*)m;
	hh->Go() ;*/ //Men goes
	
	//[Error] invalid conversion from 'Human*' to 'Men*' [-fpermissive]
	/*Men* hh = new Human();
	hh->Go() ;*/
	
	//УРА!-это оно
	m->Human::Go();
	
	cout<< "Перекрытие метода"<< endl;
	m->Print();
	m->Print(27 * 1);
	
//	pause();
//	return 0;
}

};
