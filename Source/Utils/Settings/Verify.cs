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
        private static bool checkFilesInArray(string[] array)
        {
            foreach (string item in array)
            {
                if (!File.Exists(Env.Path.Get(item))
                    && !Directory.Exists(Env.Path.Get(item)))
                {
                    return false;
                }
            }
            return true;
        }
        // Verify array
        private static bool checkArray(string[] array)
        {
            if (array != null)
            {
                if (!checkFilesInArray(array))
                    return false;
            }
            return true;
        }
        public static void Config()
        {
            if (!checkArray(Settings.Linux.Files) ||
                !checkArray(Settings.Windows.Files))
            {

                throw new ApplicationException("files defined in config file don't exist!");
            }
        }
    }
}