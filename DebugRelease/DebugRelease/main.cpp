#include <iostream>

using namespace std;

int magic(int x)
{
	return 10.0F * sin(x);
}

int main()
{
	int sum = 0;
	for (int i = 0; i<10; i++)
	{
		sum += magic(i);
	}
	cout << "sum: " << sum << endl;
	return 0;
}

