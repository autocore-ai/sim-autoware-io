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

using System.Runtime.InteropServices;
using UnityEngine;

namespace AutoCore.Sim.Autoware.IO
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ROS_MSG_geometry_msgs_Quaternion
    {
        public double x;
        public double y;
        public double z;
        public double w;
        public static implicit operator ROS_MSG_geometry_msgs_Quaternion(Quaternion a) => new ROS_MSG_geometry_msgs_Quaternion { x = -a.z, y = a.x, z = -a.y, w = a.w };
        public static implicit operator Quaternion(ROS_MSG_geometry_msgs_Quaternion a) => new Quaternion((float)a.y, -(float)a.z, -(float)a.x, (float)a.w);
    }
}