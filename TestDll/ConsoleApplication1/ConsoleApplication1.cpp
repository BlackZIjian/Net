// ConsoleApplication1.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"

using namespace std;


	extern "C" __declspec(dllexport) double Add(double a, double b)
	{
		return a + b;
	}


