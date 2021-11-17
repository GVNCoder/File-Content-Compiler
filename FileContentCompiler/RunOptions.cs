using CommandLine;

namespace FileContentCompiler
{
    public class RunOptions
    {
        //[Option('b'
        //    , "base"
        //    , Required = true
        //    , HelpText = "Base path for building relative paths.")]
        //public string BasePath { get; set; }

        [Option('s'
            , "source"
            , Required = true
            , HelpText = "Source file for compilation.")]
        public string SourceFilePath { get; set; }

        [Option('d'
            , "destination"
            , Required = true
            , HelpText = "The resulting file.")]
        public string DestinationFilePath { get; set; }
    }
}