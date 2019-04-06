using Microsoft.CodeAnalysis.CSharp;

namespace Executor.Core
{
    public static class RokaProgramExecutor
    {
        public static void Execute(string program)
        {
            var sourceLanguage = CSharpCompilation.Create("RokaToGo");
        }
    }
}
