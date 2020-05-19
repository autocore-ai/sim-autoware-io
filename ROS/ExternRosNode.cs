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

namespace AutoCore.Sim.Autoware.IO
{
    public class ExternRosNode : ExternProcess
    {
        protected string GetFullPath(string exeName) => Path.Combine(ROS_Node.Instance.BinaryPath, exeName);
        protected override void Start()
        {
            if (!ROS_Node.Instance.ROS2)
            {
                EnvironmentVariables.Add("ROS_MASTER_URI", ROS_Node.Config.ros_master_uri);
                EnvironmentVariables.Add("ROS_IP", ROS_Node.Config.ros_ip);
            }
            base.Start();
        }
    }
}