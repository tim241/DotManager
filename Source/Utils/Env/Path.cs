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

namespace DotManager.Utils.Env
{
    public class Path
    {
        // Return path with variables extracted
        // If there are no variables in path, return given path.
        // Supports both Windows & Linux variables
        public static string Get(string path)
        {
            string returnPath = null, slash = null;
            // Use \ as slash for Windows
            // Use / as slash for Linux
            if(OS.Check.IsWindows())
                slash = @"\";
            else if(OS.Check.IsLinux())
                slash = @"/";
            // Split given path by slash and loop over each item
            foreach (string folder in path.Split(slash))
            {
                if (folder.StartsWith(@"$") ||
                    (folder.StartsWith(@"%") && folder.EndsWith(@"%")))
                {
                    // Get variable from system
                    // Also verify that variable isn't empty
                    string variable = Environment.GetEnvironmentVariable(
                        folder.Replace(@"$", null)
                        .Replace(@"%", null));
                    if (!string.IsNullOrEmpty(variable))
                        returnPath += variable;
                    else
                        returnPath += folder;
                }
                else
                    returnPath += folder;
                // Append slash
                returnPath += slash;
            }
            return returnPath;
        }
    }
}