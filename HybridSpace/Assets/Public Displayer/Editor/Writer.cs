﻿using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;

namespace PublicDisplayer
{
    public abstract class Writer
    {
        /// <summary>
        /// Overwrites a .cs file in your Directory.
        /// </summary>
        /// <param name="directives">Specify using directives for the file to function properly.</param>
        protected static void OverwriteFile(string fileName, IEnumerable<string> body, params string[] directives)
        {
            OverwriteFile(fileName, body, null, directives);
        }

        /// <summary>
        /// Overwrites a .cs file in your Directory.
        /// </summary>
        /// <param name="authoringFileType">Here you should add the authoring file that requested the overwrite. <br/>
        /// Example: <c>OverwriteFile( "MyFile" , "MyBody" , typeof( thisFile ) );</c>
        /// </param>
        /// <param name="directives">Specify using directives for the file to function properly.</param>
        protected static void OverwriteFile(string fileName, IEnumerable<string> body, Type authoringFileType, params string[] directives)
        {
            OverwriteFile(fileName, body, authoringFileType, false, directives);
        }

        /// <summary>
        /// Overwrites or adds a .cs file in your Directory.
        /// </summary>
        /// <param name="directives">Specify using directives for the file to function properly.</param>
        protected static void OverwriteOrAddFile(string fileName, IEnumerable<string> body, params string[] directives)
        {
            OverwriteOrAddFile(fileName, body, null, directives);
        }

        /// <summary>
        /// Overwrites or adds a .cs file in your Directory.
        /// </summary>
        /// <param name="authoringFileType">Here you should add the authoring file that requested the overwrite. <br/>
        /// Example: <c>OverwriteFile( "MyFile" , "MyBody" , typeof( thisFile ) );</c>
        /// </param>
        /// <param name="directives">Specify using directives for the file to function properly.</param>
        protected static void OverwriteOrAddFile(string fileName, IEnumerable<string> body, Type authoringFileType, params string[] directives)
        {
            OverwriteFile(fileName, body, authoringFileType, true, directives);
        }

        private static void OverwriteFile(string fileName, IEnumerable<string> body, Type authoringFileType, bool addIfMissing, params string[] directives)
        {
            /*
            if(!PublicDisplayerSettings.GetSettings(out PublicDisplayerSettings settings))
            {
                UnityEngine.Debug.LogWarning("Unable to retrieve settings, using default settings instead.");
            }

            string fileExtension;
            using(var provider = CodeDomProvider.CreateProvider("C#"))
            {
                fileExtension = provider.FileExtension;
            }

            string filePath = $"{settings.Path}/{fileName}.{fileExtension}.";

            var asset = AssetDatabase.LoadAssetAtPath(filePath, Type.GetType(fileName));
            if(!asset && !addIfMissing)
                return;
                */

            string fileExtension;
            using(var provider = CodeDomProvider.CreateProvider("C#"))
            {
                fileExtension = provider.FileExtension;
            }

            //string filePath = $"{settings.Path}/{fileName}.{fileExtension}.";
            string filePath = AssetDatabase.FindAssets(fileName, new[] { "Assets/Public Displayer" })
                .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
                .FirstOrDefault(name => name.Substring(name.LastIndexOf('/') + 1) == $"{fileName}.{fileExtension}");

            var asset = AssetDatabase.LoadAssetAtPath(filePath, Type.GetType(fileName));
            if(!asset && !addIfMissing)
                return;

            // Overwrite the file
            using(StreamWriter file = new StreamWriter(filePath))
            {
                IndentLevel = 0;

                // Add comment from authoring file
                string authoringFileName = authoringFileType.Name;
                if(!string.IsNullOrWhiteSpace(authoringFileName))
                {
                    file.WriteLine($"{indent}// This file is dynamically created and overwritten. Changes should be made in {authoringFileName}.{fileExtension}.");
                }

                // Add directives
                if(directives.Length > 0)
                {
                    foreach(string directive in directives)
                        file.WriteLine($"{indent}using {directive};");
                    file.WriteLine();
                }

                file.WriteLine($"{indent}namespace UnityEngine");
                file.WriteLine($"{indent}{{");
                IndentLevel++;

                // Write the body
                file.WriteLine($"{ToClass(fileName)}");
                file.WriteLine($"{indent}{{");
                IndentLevel++;
                foreach(var line in body)
                    file.WriteLine($"{indent}{line}");
                IndentLevel--;
                file.WriteLine($"{indent}}}");

                IndentLevel--;
                file.WriteLine($"{indent}}}");
            }
        }

        private static int indentLevel;
        protected static string indent;
        protected static int IndentLevel
        {
            get => indentLevel;
            set
            {
                indentLevel = Math.Max(0, value);
                indent = new string('\t', indentLevel);
            }
        }

        protected static string ToVariable(string identifier, object value)
        {
            using(var provider = new CSharpCodeProvider())
            {
                Type type = value.GetType();

                var typeRef = new CodeTypeReference(type);
                string typeName = provider.GetTypeOutput(typeRef);

                if(type == typeof(string))
                    value = $"@\"{value.ToString().Replace("\"", "\"\"")}\"";

                return $"{indent}public const {typeName} {identifier} = {value};";
            }
        }

        protected static string ToClass(string identifier) => $"{indent}public static class {identifier}";
    }
}
