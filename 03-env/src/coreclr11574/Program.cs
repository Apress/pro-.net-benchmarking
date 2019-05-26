using System;

class Program
{
    static byte[] s_arr2;
    static byte[] s_arr3;

    static void Init()
    {
        s_arr2 = new byte[] {0x11, 0x12, 0x13};
        s_arr3 = new byte[] {0x21, 0x22, 0x33};
    }

    static void Main(string[] args)
    {
        Init();

        byte[] arr1 = new byte[] {2};
        byte[] arr2 = s_arr2;
        byte[] arr3 = s_arr3;

        int len = arr1.Length + arr2.Length + arr3.Length;
        int cur = 0;
        Console.WriteLine("1: cur = {0}", cur);
        cur += arr1.Length;
        Console.WriteLine("2: cur += {0}, now {1}", arr1.Length, cur);
        cur += arr2.Length;
        Console.WriteLine("3: cur += {0}, now {1}", arr2.Length, cur);
        cur += arr3.Length;
        Console.WriteLine("4: cur += {0}, now {1}", arr3.Length, cur);
        Console.WriteLine("5: len is {0}", len);
    }
}