// pch.cpp: 与预编译标头对应的源文件

#include "pch.h"
#include <iostream>
#include <locale>

// 当使用预编译的头时，需要使用此源文件，编译才能成功。

struct Student
{
	const wchar_t* Name;
	int Age;
};

using namespace std;

extern "C"
{
	__declspec(dllexport) int __cdecl TestBase(int a)
	{
		return a + 1;
	}

	__declspec(dllexport) bool __cdecl TestBool(bool a)
	{
		return false;
	}

	__declspec(dllexport) int* __cdecl TestBasePtr(int* a)
	{
		*a = *a + 1;
		return a;
	}

	__declspec(dllexport) int* __cdecl TestArr(int* a)
	{
		*(a + 1) = 99;
		a[2] = 999;
		return a;
	}

	__declspec(dllexport) int& __cdecl TestBaseRef(int& a)
	{
		a = a + 1;
		int& b = a;
		return b;
	}

	__declspec(dllexport) char __cdecl TestChar(char a)
	{
		cout << "get char: " << a << endl;
		return a;
	}

	__declspec(dllexport) wchar_t __cdecl TestWChar(wchar_t a)
	{
		// set locale
		locale::global(locale(""));
		wcout.imbue(locale());

		wcout << "get wchar: " << a << endl;
		return a;
	}

	__declspec(dllexport) wchar_t* __cdecl TestStr(wchar_t* a)
	{
		// set locale
		locale::global(locale(""));
		wcout.imbue(locale());

		wcout << "get wchar string: " << a << endl;
		return a;
	}

	__declspec(dllexport) wchar_t* __cdecl TestStrB(wchar_t* a)
	{
		// set locale
		locale::global(locale(""));
		wcout.imbue(locale());

		wcout << "get wchar string: " << a << endl;
		a[4] = L'星';
		a[5] = L'座';
		wcout << "changed to: " << a << endl;
		return a;
	}

	__declspec(dllexport) Student __cdecl TestStruct(Student stu)
	{
		stu.Age = 99;
		return stu;
	}
}

