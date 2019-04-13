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
        }

        private static void ShowErrors(EmitResult result)
        {
            var compilationErrors =
                result.Diagnostics
                    .Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error)
                    .ToList();

            foreach(var error in compilationErrors)
            {
                Console.WriteLine($"{error.GetMessage()}");
            }
        }
    }
}
