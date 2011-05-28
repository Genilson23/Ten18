﻿using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using System.Security.Policy;
using System.Reflection.Emit;
using System.Reflection;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using Microsoft.CSharp;
using Ten18.Interop;
using System.IO;
using Mono.Cecil;
using Ten18.Build;


namespace Ten18.Interop
{
    partial class CppHeaderTemplate
    {
        public CppHeaderTemplate(TypeDefinition typeDef)
        {   
            mCppHeaderFile = Path.Combine(Paths.WorkingDir, typeDef.FullName.Replace(".", "\\") + ".Generated.h");
            if (!Directory.Exists(Path.GetDirectoryName(mCppHeaderFile)))
                Directory.CreateDirectory(Path.GetDirectoryName(mCppHeaderFile));
        }

        internal void Add(MethodGenerator methodGenerator) { mMethodGenerators.Add(methodGenerator); }

        public void Generate()
        {
            var code = TransformText();
            File.WriteAllText(mCppHeaderFile, code);

            Console.WriteLine("Updated: {0}", mCppHeaderFile);
        }

        private string mCppHeaderFile;
        private IList<MethodGenerator> mMethodGenerators = new List<MethodGenerator>();
    }
}
