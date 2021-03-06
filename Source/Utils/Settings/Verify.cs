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

namespace DotManager.Utils.Settings
{
    public class Verify
    {
        // Verify that each file/folder in specified array exists
        //
        private static bool checkFilesInArray(string[] array)
        {
            foreach (string item in array)
            {
                // Sanity check
                if (!string.IsNullOrEmpty(item))
                {
                    if (!File.Exists(Env.Path.Get(item))
                        && !Directory.Exists(Env.Path.Get(item)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        // Verify array
        //
        private static bool checkArray(string[] array)
        {
            if (array != null)
            {
                if (!checkFilesInArray(array))
                    return false;
            }
            return true;
        }
        // Verify that directory exists when array isn't emtpy
        //
        private static bool checkDirectory(string[] array, string directory)
        {
            if (array != null)
            {
                string fileDest = Env.Path.Get(directory);
                if (!string.IsNullOrEmpty(fileDest))
                {
                    if (Directory.Exists(fileDest))
                    {
                        return true;
                    }
                }
            }
            // Return true when array is null
            // Because array is allowed to be empty
            else
                return true;
            return false;
        }
        public static void Config()
        {
            // Verify that the Linux distro is supported
            if (OS.Check.IsLinux() &&
                Settings.Linux.Distro != null)
            {
                if (!OS.Linux.SupportedDistros.ContainsKey(Settings.Linux.Distro))
                {
                    throw new ApplicationException("distro defined in config file is invalid!");
                }
            }
            if (!checkArray(Settings.Linux.Files) ||
                !checkArray(Settings.Windows.Files))
            {

                throw new ApplicationException("files defined in config file don't exist!");
            }
            if (Settings.Linux.Files != null || Settings.Windows.Files != null)
            {
                if (!(OS.Check.IsLinux() && checkDirectory(Settings.Linux.Files, Settings.Linux.FilesDest)) &&
                    !(OS.Check.IsWindows() && checkDirectory(Settings.Windows.Files, Settings.Windows.FilesDest)))
                {
                    throw new ApplicationException("filedest defined in config file is invalid");
                }
            }
        }
    }
}