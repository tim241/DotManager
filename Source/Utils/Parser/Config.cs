using System;
using System.IO;
using DotManager;
using DotManager.Utils;

namespace DotManager.Utils.Parser
{
    public class Config
    {
        // Read config file
        public static void Read(string fileName)
        {
            if(File.Exists(fileName))
                Utils.Parser.Formats.YAML.Read.Config(fileName);
            else
                throw new ApplicationException("config file doesn't exist!");
        }
        // Create config file
        public static void Create(string fileName, string linuxDistro, string linuxDependencies,
                                  string linuxFilesDest, string[] linuxFiles,
                                  string winDependencies, string winFilesDest, 
                                  string[] winFiles)
        {
            Utils.Parser.Formats.YAML.Create.Config(fileName, linuxDistro, 
                linuxDependencies, linuxFilesDest, linuxFiles, 
                winDependencies, winFilesDest, winFiles);
        }
        // Create example config file
        public static void CreateExample()
        {
            Create("example.yaml",
                    "arch", "base-devel clang", "$HOME", new string[] { "Source/Utils/Env/Path.cs", "Source/Utils/OS/Check.cs", "Source/Utils/OS/Linux.cs" },
                    "steam origin", @"\Users\%USERNAME%", new string[] { "Source/" });
        }
    }
}