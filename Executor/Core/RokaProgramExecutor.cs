using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text.RegularExpressions;

namespace Executor.Core
{
    public static class RokaProgramExecutor
    {
        public static void Main()
        {
            Execute("");
        }
        public static void Execute(string program)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(program);
            var assemblyPath = Path.GetDirectoryName(typeof(object).Assembly.Location);
            var references = new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).GetTypeInfo().Assembly.Location),
                MetadataReference.CreateFromFile(typeof(HashSet<int>).GetTypeInfo().Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Regex).GetTypeInfo().Assembly.Location),
                MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.Private.CoreLib.dll")),
                MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.Console.dll")),
                MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.Runtime.dll"))
            };
            var compilation = CSharpCompilation.Create(
                "RokaToGo",
                new[] { syntaxTree },
                references,
                new CSharpCompilationOptions(OutputKind.ConsoleApplication)
            );

            using (var ms = new MemoryStream())
            {
                var result = compilation.Emit(ms);
                if (result.Success)
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    var assembly = AssemblyLoadContext.Default.LoadFromStream(ms);
                    var type = assembly.GetType("RokaProgramming.RokaProgram");
                    type.InvokeMember("Main", BindingFlags.Default | BindingFlags.InvokeMethod, null, null, null);
                }
                else
                {
                    ShowErrors(result);
                }
            }

            //            var tree = CSharpSyntaxTree.ParseText(@"
            //namespace Roka
            //{
            //public class MyClass
            //{
            //    public static void Main()
            //    {
            //        System.Console.WriteLine(""Hello World!"");
            //        System.Console.ReadLine();
            //    }   
            //}
            //}");
            //            var assemblyPath = Path.GetDirectoryName(typeof(object).Assembly.Location);

            //            var mscorlib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
            //            var compilation = CSharpCompilation.Create("MyCompilation",
            //                syntaxTrees: new[] { tree },
            //                references: 
            //                new[] {
            //                    mscorlib,
            //                    MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.Private.CoreLib.dll")),
            //                    MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.Console.dll")),
            //                    MetadataReference.CreateFromFile(Path.Combine(assemblyPath, "System.Runtime.dll"))
            //                });

            //            //Emit to stream
            //            var ms = new MemoryStream();
            //            var emitResult = compilation.Emit(ms);

            //            if(!emitResult.Success){
            //                ShowErrors(emitResult);
            //            }

            //            //Load into currently running assembly. Normally we'd probably
            //            //want to do this in an AppDomain
            //            var ourAssembly = Assembly.Load(ms.ToArray());
            //            var type = ourAssembly.GetType("Roka.MyClass");

            //            //Invokes our main method and writes "Hello World" :)
            //            type.InvokeMember("Main", BindingFlags.Default | BindingFlags.InvokeMethod, null, null, null);
        }

        private static void ShowErrors(EmitResult result)
        {
            var compilationErrors = result.Diagnostics.Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error).ToList();
            foreach(var error in compilationErrors)
            {
                Console.WriteLine($"{error.GetMessage()}");
            }
        }
    }
}
