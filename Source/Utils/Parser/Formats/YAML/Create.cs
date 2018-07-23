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
using System.Collections.Generic;
using System.IO;
using YamlDotNet;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DotManager.Utils.Parser.Formats.YAML
{

    public class Create
    {
        public static void Config(string fileName, string linuxDistro, string linuxDependencies,
                                  string linuxFilesDest, string[] linuxFiles,
                                  string winDependencies, string winFilesDest, 
                                  string[] winFiles)
        {
            // Create new Config
            ConfigTemplate cfg = new ConfigTemplate()
            {
                linux = new List<ConfigTemplate.Linux> {
                    new ConfigTemplate.Linux() {
                        Distro = linuxDistro,
                        Dependencies = linuxDependencies,
                        Filesdest = linuxFilesDest,
                        Files = linuxFiles
                    }
                },
                windows = new List<ConfigTemplate.Windows> {
                    new ConfigTemplate.Windows() {
                        Dependencies = winDependencies,
                        Filesdest = winFilesDest,
                        Files = winFiles
                    }
                }
            };
            // Get YAML from config
            Serializer sb = new SerializerBuilder().
                    WithNamingConvention(new CamelCaseNamingConvention())
                    .Build();
            string configText = sb.Serialize(cfg);
            // Write YAML to file
            File.WriteAllText(fileName, configText);

        }
    }
}