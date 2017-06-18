﻿//-----------------------------------------------------------------------
// <copyright file="SourcesManagement.cs" company="Hud Dimesion">
//     Copyright (c) Hud Dimension. All rights reserved.
// </copyright>
//
// <disclaimer>
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// </disclaimer>
//
// <author>Alessio Langiu</author>
// <email>alessio.langiu@huddimension.co.uk</email>
//-----------------------------------------------------------------------
using System.Linq;
using System.Reflection;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace HudDimension.UnityTestBDD
{
    public static class SourcesManagement
    {
        public static string GetAbsoluteDLLPathFromMethod(MethodInfo methodInfo)
        {
            return methodInfo.DeclaringType.Assembly.Location;
        }

        public static void OpenSourceCode(MethodInfo methodInfo, IUnityInterfaceWrapper unityInterfaceWrapper)
        {
            ReaderParameters readerParameters = new ReaderParameters { ReadSymbols = true };
            AssemblyDefinition assemblyDefinition = AssemblyDefinition.ReadAssembly(GetAbsoluteDLLPathFromMethod(methodInfo), readerParameters);
            OpenSourceCode(methodInfo, unityInterfaceWrapper, assemblyDefinition);
        }

        public static void OpenMethodSourceCode(MethodInfo methodInfo, IUnityInterfaceWrapper unityInterfaceWrapper)
        {
            ReaderParameters readerParameters = new ReaderParameters { ReadSymbols = true };
            AssemblyDefinition assemblyDefinition = AssemblyDefinition.ReadAssembly(GetAbsoluteDLLPathFromMethod(methodInfo), readerParameters);
            OpenMethodSourceCode(methodInfo, unityInterfaceWrapper, assemblyDefinition);
        }

        public static void OpenMethodSourceCode(MethodInfo methodInfo, IUnityInterfaceWrapper unityInterfaceWrapper, AssemblyDefinition assemblyDefinition)
        {
            SequencePoint sp = GetSequencePointForMethod(methodInfo, assemblyDefinition);
            Document document = sp.Document;
            string documentUrl = document.Url;
            unityInterfaceWrapper.UnityEditorInternalInternalEditorUtilityOpenFileAtLineExternal(documentUrl, sp.StartLine);
        }

        private static SequencePoint GetSequencePointForMethod(MethodInfo methodInfo, AssemblyDefinition assemblyDefinition)
        {
            TypeDefinition componentTypeDefinition = null;
            foreach (TypeDefinition typeDefinition in assemblyDefinition.MainModule.Types)
            {
                if (typeDefinition.FullName.Equals(methodInfo.DeclaringType.FullName))
                {
                    componentTypeDefinition = typeDefinition;
                    break;
                }
            }

            string methodFullName = GetMethodFullName(methodInfo);
            MethodDefinition methodDef = null;
            foreach (MethodDefinition methodDefinition in componentTypeDefinition.Methods)
            {
                if (methodDefinition.FullName.Equals(methodFullName))
                {
                    methodDef = methodDefinition;
                    break;
                }
            }

            SequencePoint sp = methodDef.Body.Instructions
        .Where(instruction => instruction.SequencePoint != null)
        .Select(instruction => instruction.SequencePoint)
        .FirstOrDefault();
            return sp;
        }

        private static string GetMethodFullName(MethodInfo methodInfo)
        {
            string parameters = "(";
            foreach (ParameterInfo parameter in methodInfo.GetParameters())
            {
                if (!parameters.Equals("("))
                {
                    parameters += ",";
                }

                parameters += parameter.ParameterType.FullName;
            }

            parameters += ")";
            string methodFullName = methodInfo.ReturnType.FullName + " " + methodInfo.DeclaringType.FullName + "::" + methodInfo.Name + parameters;
            return methodFullName;
        }

        private static void OpenSourceCode(MethodInfo methodInfo, IUnityInterfaceWrapper unityInterfaceWrapper, AssemblyDefinition assemblyDefinition)
        {
            SequencePoint sp = GetSequencePointForMethod(methodInfo, assemblyDefinition);
            Document document = sp.Document;
            string documentUrl = document.Url;
            unityInterfaceWrapper.UnityEditorInternalInternalEditorUtilityOpenFileAtLineExternal(documentUrl, 0);
        }
    }
}