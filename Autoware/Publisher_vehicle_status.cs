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

using AutoCore.Sim.Vehicle.Control;
using UnityEngine;

namespace AutoCore.Sim.Autoware.IO
{
    [RequireComponent(typeof(IVehicle))]
    public class Publisher_vehicle_status : ROS_Publisher
    {
        IVehicle vehicle;
        IVehicle Vehiche
        {
            get
            {
                if (vehicle == null)
                {
                    vehicle = GetComponent<IVehicle>();
                }
                return vehicle;
            }
        }
        private void Awake()
        {
            topic = "vehicle_status";
            frame = "base_link";
        }
        protected override void OnROSAdvertise() => ROS_Node.Instance.Advertise_autoware_msgs_VehicleStatus(Topic, 1);
        protected override void OnPublish()
        {
            ROS_Node.Instance.Publish_VehicleStatus(
                Topic, Frame, ROS_Node.Config.ros_node,
               0, 0, 0, Vehiche.Speed,
               0, 0, -Vehiche.Angle, 0, 0);
        }
    }
}
