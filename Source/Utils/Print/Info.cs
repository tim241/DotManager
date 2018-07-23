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
using System.Collections.Generic;

namespace DotManager.Utils.Print
{
    public class Info
    {
        // Print platform if platform has a value
        //
        private static void printPlatform(string platform, string distroName)
        {
            if (!string.IsNullOrEmpty(platform))
                Console.WriteLine($"--> Platform: {platform}");
            if (!string.IsNullOrEmpty(distroName))
                Console.WriteLine($"-> distro: {distroName}");
        }
        // Loop over each item and print that to the console
        //
        private static void printLoop(List<string> list, string line, string headLine)
        {
            if (list.Count != 0)
                Console.WriteLine(headLine);
            foreach (string item in list)
            {
                Console.WriteLine($"{line} {item}");
            }
        }
        // Print the settings
        //
        private static void printSettings(string[] array, string dest, string deps, string platformName, string distroName = null)
        {
            // Verify that everything's filled
            if (!string.IsNullOrEmpty(dest) && !string.IsNullOrEmpty(deps) &&
                    array != null)
                printPlatform(platformName, distroName);
            // If all those things are emtpy
            // Make sure that distroName and deps are not null
            // then print the platform
            else if (!string.IsNullOrEmpty(distroName) &&
                    deps != null)
                printPlatform(platformName, distroName);
            // Else check if files and dest have a value
            else if (!string.IsNullOrEmpty(dest) &&
                    array != null)
                printPlatform(platformName, distroName);
            // Show dependencies if it has a value
            if (deps != null)
                Console.WriteLine($"-> Dependencies: {deps}");
            // Create 2 lists
            List<string> dirs = new List<string>();
            List<string> files = new List<string>();
            // Verify that the array isn't empty
            if (array != null)
            {
                // Loop over each item in the array
                foreach (string file in array)
                {
                    // Extract variables from the file path
                    string envFile = Env.Path.Get(file);
                    // Verify that envFile isn't empty
                    if (!string.IsNullOrEmpty(envFile))
                    {
                        // If envFile is a directory, add it to that list
                        // If envFile is a file, add it to the file list
                        if (Directory.Exists(envFile))
                            dirs.Add(envFile);
                        else
                            files.Add(envFile);
                    }
                }
            }
            // Loop over each array and print relevant information
            printLoop(dirs, "directory:", "--> Directories");
            printLoop(files, "file:", "--> Files");
            // Verify that dest isn't empty
            if (dest != null)
                Console.WriteLine($"--> Destination: {Env.Path.Get(dest)}");
        }
        // Print settings using printSettings
        public static void Settings()
        {
            printSettings(Utils.Settings.Linux.Files, Utils.Settings.Linux.FilesDest,
                Utils.Settings.Linux.Dependencies, "Linux", Utils.Settings.Linux.Distro);
            printSettings(Utils.Settings.Windows.Files, Utils.Settings.Windows.FilesDest,
                Utils.Settings.Windows.Dependencies, "Windows");
        }
    }
}
