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
using DotManager.Utils.Settings;
using YamlDotNet;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DotManager.Utils.Parser.Formats.YAML
{

    public class Read
    {
        public static void Config(string fileName)
        {
            StringReader sr = new StringReader(File.ReadAllText(fileName));
            Deserializer ds = new DeserializerBuilder().
                    WithNamingConvention(new CamelCaseNamingConvention())
                    .Build();
            try
            {
                ConfigTemplate config = ds.Deserialize<ConfigTemplate>(sr);
                if (config.linux != null)
                {
                    foreach (ConfigTemplate.Linux linuxConfig in config.linux)
                    {
                        Settings.Linux.Distro = linuxConfig.Distro;
                        Settings.Linux.Dependencies = linuxConfig.Dependencies;
                        Settings.Linux.FilesDest = linuxConfig.Filesdest;
                        Settings.Linux.Files = linuxConfig.Files;
                    }
                }
                if (config.windows != null)
                {
                    foreach (ConfigTemplate.Windows windowsConfig in config.windows)
                    {
                        Settings.Windows.Dependencies = windowsConfig.Dependencies;
                        Settings.Windows.FilesDest = windowsConfig.Filesdest;
                        Settings.Windows.Files = windowsConfig.Files;
                    }
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException($"Invalid config file! {e.ToString()}");
            }
        }
    }
}
