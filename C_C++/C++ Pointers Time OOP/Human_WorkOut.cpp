
#include <iostream>
//#include "stdafx.h"
#include "Human.cpp"
#include <typeinfo>

using namespace::std;

class Human_WorkOut
{
public:
/*
��������� ����� Go() �� �������
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
	h->name = "����";
	h->famName= "���";
	ShowObjectInfo(h);
	cout<< h->GetInfo(h) << endl;
	
	//������������
	m = new Men("green");
	ShowObjectInfo(m);
	cout<< "man's name: (������������)" << m->name << endl;
	
	//������� ����� ��  �� �������� ������:
	cout<< "������� ����� Go() �� �������� ������"<< endl;
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
	
	//���!-��� ���
	m->Human::Go();
	
	cout<< "���������� ������"<< endl;
	m->Print();
	m->Print(27 * 1);
	
//	pause();
//	return 0;
}

};
