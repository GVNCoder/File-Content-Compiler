using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using CommandLine;

namespace FileContentCompiler
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<RunOptions>(args)
                .WithParsed(RunFileContentCompiler);
        }

        private static void RunFileContentCompiler(RunOptions options)
        {
            const string pathGroupName = "path";

            // read all source file content into string
            var sourceFileContentLines = File.ReadAllLines(options.SourceFilePath);
            var stringBuilder = new StringBuilder();

            // compile file contents
            for (var i = 0; i < sourceFileContentLines.Length; i++)
            {
                var line = sourceFileContentLines[i];
                var insertMatch = Regex.Match(line, $"^#include\\s\\\"(?'{pathGroupName}'.*?)\\\"");
                var matchPath = insertMatch.Success
                    ? insertMatch.Groups[pathGroupName].Value
                    : string.Empty;

                // validate file path
                if (string.IsNullOrWhiteSpace(matchPath) || File.Exists(Path.Combine(Environment.CurrentDirectory, matchPath)) == false)
                {
                    stringBuilder.AppendLine(line);
                }
                else
                {
                    var contentToInsert = File.ReadAllLines(matchPath);
                    foreach (var content in contentToInsert)
                    {
                        stringBuilder.AppendLine(content);
                    }
                }
            }

            // save compiled content into file
            File.WriteAllText(options.DestinationFilePath, stringBuilder.ToString(), Encoding.UTF8);
            Console.WriteLine("Done!");
        }
    }
}
