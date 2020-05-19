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

using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

namespace AutoCore.Sim.Autoware.IO
{
    public class ExternProcess : MonoBehaviour
    {
        public string fileName = "cmd.exe";
        public string arguments = "";
        public Dictionary<string, string> EnvironmentVariables { get; set; } = new Dictionary<string, string>();
        Process process;
        protected virtual void Start() => new Thread(Run).Start();
        private void Run()
        {
            process = new Process();
            process.StartInfo.FileName = fileName;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.CreateNoWindow = true;
            foreach (var item in EnvironmentVariables)
            {
                process.StartInfo.EnvironmentVariables.Add(item.Key, item.Value);
            }
            process.Start();
            process.WaitForExit();
            process.Close();
            process = null;
        }
        protected virtual void OnDestroy() => process?.Kill();
    }
}