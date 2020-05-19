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

using System.IO;
using UnityEngine;

namespace AutoCore.Sim.Autoware.IO
{
    public class Publisher_points_map : ExternRosNode
    {
        const string exeName = "points_map_loader.exe";
        public string path = "";
        private void Awake()
        {
            arguments = "noupdate";
            path = Path.Combine(Application.streamingAssetsPath, path);
            foreach (var item in Directory.GetFiles(path, "*.pcd", SearchOption.AllDirectories))
            {
                arguments += " ";
                arguments += Path.GetFullPath(item);
            }
            fileName = GetFullPath(exeName);
        }
    }
}