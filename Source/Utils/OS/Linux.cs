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

namespace DotManager.Utils.OS
{
    public class Linux
    {
        // All *release files that are usable and tested.
        private static readonly string[] ReleaseFiles = {
                "/etc/os-release"
        };
        // Get distro name
        public static string GetDistroName()
        {
            // Verify that current OS is linux
            if (!OS.Check.IsLinux())
                throw new ApplicationException("Cannot get distro name on non-linux OS!");

            int exitCode = Command.Exec("lsb_release", "-i");
            bool fileFound = false;
            bool fileValid = false;
            switch (exitCode)
            {
                case 127:
                    foreach (string ReleaseFile in ReleaseFiles)
                    {
                        if (File.Exists(ReleaseFile))
                        {
                            fileFound = true;
                            foreach (string line in File.ReadAllLines(ReleaseFile))
                            {
                                if (line.Contains("ID="))
                                    return line.Trim(' ').Split("=")[1].Trim();
                            }
                        }
                    }
                    if (!fileFound)
                        throw new ApplicationException("can't get distro name, please install lsb_release");
                    if (!fileValid)
                        throw new ApplicationException("os release file invalid!");
                    break;
                case 0:
                    return Command.Output.Split("ID:")[1].ToLower().Trim();
                default:
                    throw new ApplicationException("Invalid error code returned by lsb_release");
            }
            return null;
        }
        // Supported distros
        // full = completely supported
        // WIP  = Work In Progress
        // 
        public static readonly Dictionary<string, string> SupportedDistros = new Dictionary<string, string>
        {
            ["arch"] = "full"
        };
        // Distro package managers
        public static readonly Dictionary<string, string> PackageManager = new Dictionary<string, string>
        {
            ["arch"] = "pacman --needed -Sy"
        };
        // Service managers
        public static readonly Dictionary<string, string> ServiceManager = new Dictionary<string, string>
        {
            ["arch"] = "systemctl enable"
        };
    }

}
