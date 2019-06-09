#include <iostream>
#include "time.h"
using namespace::std;

/*
	OOP
*/

class Human
{
	public:
		string name;
		string famName;
		string midName;
		int age;
		
		Human()		{			
		}
		
		Human(int _age, std::string _name, std::string _famName, std::string _midName, std::string _nationality)
		{
			age = _age;
			name = _name;
			famName = _famName;
			midName = _midName;
			nationality= _nationality;
		}
		
		std::string GetInfo(Human* h)
		{
			return "[H->GetInfo]: Name:" + h->name + " FamName:" + h->famName; 
		}
		
		//�����������
		virtual void  Go(){
			cout<< "Human goes" <<endl;
		}
		
	private:
		string nationality;		
		
	

};

/*
	������������
*/

class Men : public  Human
{
	public:
	Men(std::string _wearColour) : Human()
	{
		name = "default";
		
		wearColour = _wearColour;
	}
	
	//��������������� �� ����������
	void Go() override {
		cout<< "Men goes" <<endl;
	}
	

	//���������� ������
	void Print()
	{
		cout<< "[Pint] default message" <<endl;
	}
	
	void Print(int value)
	{
		cout<< "[Print]"<<value <<endl;
	}
	private:
		std::string wearColour;
};


