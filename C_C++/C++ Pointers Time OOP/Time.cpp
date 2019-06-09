#include "time.h"
#include  <iostream>

using namespace::std;

class Time{
	
	public:
	void Time_Go()
	{
		cout << "Wellcome in time.h" <<endl;
		
		struct tm *tim;
 		time_t tt = time(NULL);
 		tim = localtime(&tt);
 	
 	    cout << "Today: " << tim->tm_mday << "." << tim->tm_mon << "." << tim->tm_year+1900 << endl;

	}
};
