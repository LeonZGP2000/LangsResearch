#include "time.h"
#include  <iostream>

using namespace::std;

class Pointers{
	
	public:
		void PointerStart()
		{
			int a = 10;
			int b = 0;
			int * pointer;
			
			pointer = a;
			
			b = *pointer;
			
			cout << a <<endl;
			cout << b <<endl; 
			cout << pointer <<endl; 					
		}
};
