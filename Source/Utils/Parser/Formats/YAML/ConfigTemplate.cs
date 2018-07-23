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

using System.Collections.Generic;

namespace DotManager.Utils.Parser.Formats.YAML
{
    public class ConfigTemplate
    {
        public List<Linux> linux { get; set; }

        public List<Windows> windows { get; set; }
        public class Windows
        {
            // Get dependencies
            public string Dependencies { get; set; }
            // Get files destination
            public string Filesdest { get; set; }
            // Get files
            public string[] Files { get; set; }
        }
        public class Linux
        {
            // Get distro name
            public string Distro { get; set; }
            // Get depedencies for distro
            public string Dependencies { get; set; }
            // Get file(s) destination
            public string Filesdest { get; set; }
            // Get files
            public string[] Files { get; set; }
        }
    }

}
