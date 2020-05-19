#region License
/*
* Copyright 2018 AutoCore
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*     http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/
#endregion

using System;
using System.IO;
using UnityEngine;

namespace AutoCore.Sim.Autoware.IO
{
    [Serializable]
    public class ROS_Config
    {
        [SerializeField]
        public string ros_node = "autocore_simulator";
        [SerializeField]
        public string ros_master_uri = "http://localhost:11311/";
        [SerializeField]
        public string ros_ip = "127.0.0.1";
        const string config_file = "ROS_Config.json";
        static string ConfigFile => Path.Combine(Application.streamingAssetsPath, config_file);
        public static ROS_Config GetConfig()
        {
            ROS_Config config;
            if (File.Exists(ConfigFile))
            {
                config = JsonUtility.FromJson<ROS_Config>(File.ReadAllText(ConfigFile));
            }
            else
            {
                if (!Directory.Exists(Application.streamingAssetsPath))
                {
                    Directory.CreateDirectory(Application.streamingAssetsPath);
                }
                config = new ROS_Config();
                File.WriteAllText(ConfigFile, JsonUtility.ToJson(config));
            }
            return config;
        }
        public static bool ROS2
        {
            get
            {
                foreach (var item in Environment.GetCommandLineArgs())
                {
                    if (item.ToLower().Equals("-ros2"))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}