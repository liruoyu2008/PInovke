// See https://aka.ms/new-console-template for more information
using System.Runtime.InteropServices;
using System.Text;

Console.WriteLine("Hello, World!");

// 整数传递,值传递
var i = Dll.TestBase(1);
Console.WriteLine(i);

// bool传递
var b = Dll.TestBool(true);
Console.WriteLine(b);

// 指针传递
int i2 = 2;
var iPtr2 = Dll.TestBasePtr(ref i2);
Console.WriteLine(i2);
Console.WriteLine(iPtr2);
Console.WriteLine(Marshal.ReadInt32(iPtr2));

// 数组传递，本质是CLR自动帮我们传递了数组元素的首地址过去
// Note: 并不是传递数组对象的地址，因为数组对象还包括元数据如数组长度等
int[] arr = new int[3] { 1, 2, 3 };
var arrPtr = Dll.TestArr(arr);
Console.WriteLine($"{arr[0]}  {arr[1]}  {arr[2]}");
Console.WriteLine(arrPtr);
Console.WriteLine(Marshal.ReadInt32(arrPtr));
Console.WriteLine(Marshal.ReadInt32(arrPtr, 4));
Console.WriteLine(Marshal.ReadInt32(arrPtr, 8));
var arr2 = new int[3];
Marshal.Copy(arrPtr, arr2, 0, 3);
Console.WriteLine($"{arr2[0]}  {arr2[1]}  {arr2[2]}");

// 引用传递
// 引用的本质还是指针，所以对C#来说，引用传递和指针传递没什么不同
// 只是对C/C++编译器来说，引用省略了“解指针”的步骤，可以直接访问
int i3 = 3;
var iPtr3 = Dll.TestBaseRef(ref i3);
Console.WriteLine(i3);
Console.WriteLine(iPtr3);
Console.WriteLine(Marshal.ReadInt32(iPtr3));

// 字符
var ch = 'x';
var ch2 = Dll.TestChar(Convert.ToByte(ch));
Console.WriteLine(Convert.ToChar(ch2));
var ch3 = '美';
var ch4 = Dll.TestWChar(ch3);
Console.WriteLine(ch4);

// 字符串
var str = "五十六个民族";
var str2 = Dll.TestStr(str);
Console.WriteLine(Marshal.PtrToStringUni(str2));
var strb = new StringBuilder("五十六个民族");
var strb2 = Dll.TestStrB(strb);
Console.WriteLine(strb);

// 结构体
var stu = new Student() { Name = Marshal.StringToHGlobalUni("小花"), Age = 18 };
var stu2 = Dll.TestStruct(stu);
Console.WriteLine($"{Marshal.PtrToStringUni(stu.Name)} {stu.Age}");
Console.WriteLine($"{Marshal.PtrToStringUni(stu2.Name)} {stu2.Age}");


Console.WriteLine("Bye!");


[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct Student
{
    public IntPtr Name;

    public int Age;
}

public static class Dll
{
    [DllImport("PInvokeCPP", CallingConvention = CallingConvention.Cdecl)]
    public static extern int TestBase(int x);

    [DllImport("PInvokeCPP", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool TestBool([MarshalAs(UnmanagedType.Bool)] bool x);

    [DllImport("PInvokeCPP", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr TestBasePtr(ref int x);

    [DllImport("PInvokeCPP", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr TestArr(int[] x);

    [DllImport("PInvokeCPP", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr TestBaseRef(ref int x);

    [DllImport("PInvokeCPP", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    public static extern byte TestChar(byte ch);

    [DllImport("PInvokeCPP", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    public static extern char TestWChar(char ch);

    [DllImport("PInvokeCPP", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    public static extern IntPtr TestStr(string ch);

    [DllImport("PInvokeCPP", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    public static extern IntPtr TestStrB(StringBuilder ch);

    [DllImport("PInvokeCPP", CallingConvention = CallingConvention.Cdecl)]
    public static extern Student TestStruct(Student x);
}
