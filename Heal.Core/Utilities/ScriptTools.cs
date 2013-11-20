using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;

namespace Heal.Core.Utilities
{
    public class ScriptTools
    {
        private static Dictionary<int, Assembly> m_assembly = new Dictionary<int, Assembly>();
        private static Dictionary<int, Type> m_types = new Dictionary<int, Type>();
        private static Assembly Compile(string vSource)
        {
            CodeDomProvider vCodeCompiler = new CSharpCodeProvider();
            CompilerParameters vCompilerParameters = new CompilerParameters();
            vCompilerParameters.GenerateExecutable = false;
            vCompilerParameters.GenerateInMemory = true;
            vCompilerParameters.ReferencedAssemblies.Add("mscorlib.dll");
            vCompilerParameters.ReferencedAssemblies.Add("System.dll");
            vCompilerParameters.ReferencedAssemblies.Add("System.Core.dll");
            vCompilerParameters.ReferencedAssemblies.Add(AppDomain.CurrentDomain.SetupInformation.ApplicationBase +
                                                          "Heal.Core.dll");
            CompilerResults vCompilerResults =
                vCodeCompiler.CompileAssemblyFromSource(vCompilerParameters, vSource);
            Assembly assembly = vCompilerResults.CompiledAssembly;

            foreach (var item in assembly.GetTypes())
            {
                m_types.Add( item.Name.GetHashCode(), item );
            }
            return assembly;
        }

        private static Assembly GetAssembly(string vSource)
        {
            Assembly assembly;
            m_assembly.TryGetValue( vSource.GetHashCode(), out assembly );
            if(assembly == null)
            {
                assembly = Compile( vSource );
                m_assembly.Add( vSource.GetHashCode(), assembly );
            }
            return assembly;
        }

        public static object RunScript(string vSource, object[] vParams)
        {
            throw new NotImplementedException();
        }

    }
}
