using System;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;

namespace HugeMethod
{
    class Program
    {
        static void Main()
        {
            // Regular code generation routine
            var assemblyName = new AssemblyName {Name = "MyAssembly"};
            var assembly = AppDomain.CurrentDomain
                .DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
            var module = assembly.DefineDynamicModule("Module");
            var typeBuilder = module.DefineType("Type", TypeAttributes.Public);
            var methodBuilder = typeBuilder.DefineMethod(
                "Calc", MethodAttributes.Public | MethodAttributes.Static,
                typeof(int), new Type[0]);

            // Generate the target method
            var generator = methodBuilder.GetILGenerator();
            generator.Emit(OpCodes.Ldc_I4, 0); // Put 0 on stack
            for (var i = 1; i <= 1000000; i++)
            {
                generator.Emit(OpCodes.Ldc_I4, i); // Put i on stack
                generator.Emit(i % 2 == 0 // Apply '+' or '-' on two top stack values
                    ? OpCodes.Add
                    : OpCodes.Sub);
            }
            generator.Emit(OpCodes.Ret); // Return the top value from stack

            // Build the target type
            var type = typeBuilder.CreateType();
            // Lambda which call this method via reflection
            Func<int> calc = () => (int) type.InvokeMember("Calc",
                BindingFlags.InvokeMethod | BindingFlags.Public |
                BindingFlags.Static, null, null, null);

            // Measure duration of the 1st and 2nd calls
            var stopwatch1 = Stopwatch.StartNew();
            calc(); // 1st call (cold start)
            stopwatch1.Stop();
            var stopwatch2 = Stopwatch.StartNew();
            var result = calc(); // 2nd call (warmed state)
            stopwatch2.Stop();

            // Print results
            Console.WriteLine($"Result   : {result}");
            Console.WriteLine($"1st call : {stopwatch1.ElapsedMilliseconds} ms");
            Console.WriteLine($"2nd call : {stopwatch2.ElapsedMilliseconds} ms");
        }
    }
}