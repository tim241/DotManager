/*
 DotManager - https://github.com/tim241/DotManager

 Copyright (C) 2018 Tim Wanders <timwanders241@gmail.com>

 This program is free software: you can redistribute it and/or modify
 it under the terms of the GNU General Public License as published by
 the Free Software Foundation, either version 3 of the License, or
 (at your option) any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.IO;
using System.Diagnostics;
using DotManager;
using DotManager.Utils;
using DotManager.Utils.Parser;
using System.Collections.Generic;

namespace DotManager
{

    class Program
    {
        // Get current executable filename
        //
        static string ExecutableFileName = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);
        // Parse arguments
        //
        static void ParseArguments(string[] args)
        {
            string configFile = null;
            bool install = false;
            foreach (string argument in args)
            {
                if(argument.StartsWith("--config="))
                    configFile = argument.Replace("--config=", null);
                switch(argument)
                {
                    case "--create-example":
                        Config.CreateExample();
                        break;
                    case "--help":
                        Program.Usage();
                        break;
                    case "--install":
                        install = true;
                        break;
                    default:
                        break;
                }
            }
            if(!string.IsNullOrEmpty(configFile))
            {
                Config.Read(configFile);
                Utils.Settings.Verify.Config();
                if(!install)
                {
                    // Show information about config
                    Utils.Print.Info.Settings();
                }
            }
        }
        // Display usage
        //
        static void Usage()
        {
            Console.Write(ExecutableFileName + " --help\n" +
                          ExecutableFileName + " --config=[FILE]\n" +
                          ExecutableFileName + " --create-example\n" +
                          ExecutableFileName + " --dry-run\n");
        }
        // Main
        //
        static void Main(string[] args)
        {
            // Verify that OS is supported
            if (!Utils.OS.Supported.Check())
                throw new ApplicationException("Unsupported OS");
            // Verify that arguments are given,
            // if not, show usage
            if (args.Length == 0)
            {
                Program.Usage();
                Environment.Exit(1);
            }
            else
            {
                // Parse the arguments
                Program.ParseArguments(args);
            }
        }
    }
}
