// ConsoleApplication1.cpp : ���� DLL Ӧ�ó���ĵ���������
//

#include "stdafx.h"

using namespace std;


	extern "C" __declspec(dllexport) double Add(double a, double b)
	{
		return a + b;
	}


