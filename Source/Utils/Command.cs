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
using System.ComponentModel;
using System.Diagnostics;

namespace DotManager.Utils
{
    public class Command
    {
        public static string Output;
        public static int Exec(string command, string arguments = null, bool waitForExit = true)
        {
            // Verify that command isn't null
            if (string.IsNullOrEmpty(command))
                throw new ArgumentException("command can't be empty or null!");
            // Create process and configure it
            Process cmdProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    UseShellExecute = false,
                    FileName = command,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };
            // Try to execute the program, if it fails with a Win32Exception, return 127
            // Because the program *likely* doesn't exist
            try
            {
                cmdProcess.Start();
            }
            catch (Win32Exception)
            {
                return 127;
            }

            if (waitForExit)
            {
                Output = cmdProcess.StandardOutput.ReadToEnd();
                cmdProcess.WaitForExit();
                return cmdProcess.ExitCode;
            }

            return 0;
        }
    }
}