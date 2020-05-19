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

using AOT;
using AutoCore.Sim.Vehicle.Control;
using System.Runtime.InteropServices;
using UnityEngine;

namespace AutoCore.Sim.Autoware.IO
{
    [RequireComponent(typeof(IVehicle))]
    public class Subscriber_vehicle_cmd : ROS_Subscriber
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
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void VoidFuncPtrVehicleCmd(int steer,
                                            int accel,
                                            int brake,
                                            int lamp_l,
                                            int lamp_r,
                                            int gear,
                                            int mode,
                                            ROS_MSG_geometry_msgs_Twist twist_cmd,
                                            ROS_MSG_autoware_msgs_ControlCommand ctrl_cmd,
                                            int emergency);
        [MonoPInvokeCallback(typeof(VoidFuncPtrVehicleCmd))]
        private void OnVehicleCmd(int steer,
                                        int accel,
                                        int brake,
                                        int lamp_l,
                                        int lamp_r,
                                        int gear,
                                        int mode,
                                        ROS_MSG_geometry_msgs_Twist twist_cmd,
                                        ROS_MSG_autoware_msgs_ControlCommand ctrl_cmd,
                                        int emergency)
        {
            Vehiche.LinearVelocity = (float)ctrl_cmd.linear_velocity;
            Vehiche.LinearAcceleration = (float)ctrl_cmd.linear_acceleration;
            Vehiche.SteeringAngle = -(float)ctrl_cmd.steering_angle;
        }
        VoidFuncPtrVehicleCmd callback;
        private void Awake() => topic = "vehicle_cmd";
        protected override void OnSubscribe()
        {
            callback = OnVehicleCmd;
            ROS_Node.Instance.Subscribe_autoware_msgs_VehicleCmd(Topic, 10, Marshal.GetFunctionPointerForDelegate(callback));
        }
    }
}