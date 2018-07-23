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

namespace DotManager.Utils.OS
{
    public class Supported
    {
        public static bool Check()
        {
            // I'd like to use a switch/case here, but this solution works for me
            //
            // Windows is only supported when choco is installed
            if (OS.Check.IsWindows())
            {
                int exitCode = Command.Exec("choco", "--help");
                switch(exitCode)
                {
                    case 0:
                        return true;
                    case 127:
                        throw new ApplicationException("choco is not installed!");
                    default:
                        throw new ApplicationException("choco returned an unknown error code");
                }
            }
            // Linux only has support for a few distros with specific package managers
            else if (OS.Check.IsLinux())
            {
                string distroName = OS.Linux.GetDistroName();
                if (OS.Linux.SupportedDistros.ContainsKey(distroName))
                {
                    if(OS.Linux.SupportedDistros[distroName] == "WIP")
                        Console.WriteLine($"NOTE: Support for {distroName} is still WIP!");
                    return true;
                }
            }
            // Unsupported OS
            return false;
        }

    }
}
