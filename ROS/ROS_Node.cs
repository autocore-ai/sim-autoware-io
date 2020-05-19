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
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

namespace AutoCore.Sim.Autoware.IO
{
    public class ROS_Node : MonoBehaviour
    {
        static ROS_Node instance;
        public static ROS_Node Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameObject(typeof(ROS_Node).Name).AddComponent<ROS_Node>();
                }
                return instance;
            }
        }
        static ROS_Config config;
        public static ROS_Config Config
        {
            set => config = value;
            get
            {
                if (config == null)
                {
                    config = ROS_Config.GetConfig();
                }
                return config;
            }
        }
        public bool ROS2 { get; private set; }
        public string BinaryPath
        {
            get
            {
                if (ROS2)
                {
                    return Path.GetFullPath(Application.streamingAssetsPath + "/ROS2");
                }
                else
                {
                    return Path.GetFullPath(Application.streamingAssetsPath + "/../Plugins/x86_64/");
                }
            }
        }

        private void Awake()
        {
            instance = this;
            ROS2 = ROS_Config.ROS2;
            if (ROS2)
            {
                ROS2_NativePluin.RegisterDebugFunctions();
                InitROS2(Config.ros_node);
            }
            else
            {
                ROS1_NativePluin.RegisterDebugFunctions();
                InitROS1(Config.ros_node, Config.ros_master_uri, Config.ros_ip);
            }
        }

        private void Update()
        {
            if (ROS_OK)
            {
                ROS_SpinOnce();
            }
        }

        private void OnDestroy() => ROS_ShutDown();

        public static bool CheckRos1Master(IPAddress remoteIP, ushort port, float timeout = 1) => ROS1_NativePluin.ros_master_check(remoteIP.ToString(), port, timeout);

        public void InitROS2(string name, params string[] param)
        {
            if (!ROS2_NativePluin.ros_ok())
            {
                ROS2_NativePluin.ros_init(name);
            }
        }

        public void InitROS1(string name, string master, string hostname, params string[] param)
        {
            if (!ROS1_NativePluin.ros_ok())
            {
                var argv = new List<string> { $"__master:={master}", $"__hostname:={hostname}" };
                if (param.Length > 0)
                {
                    argv.AddRange(param);
                }
                ROS1_NativePluin.ros_init(argv.Count, argv.ToArray(), name);
            }
        }

        private void ROS_SpinOnce()
        {
            if (ROS2)
            {
                ROS2_NativePluin.ros_spinOnce();
            }
            else
            {
                ROS1_NativePluin.ros_spinOnce();
            }
        }

        public void ROS_ShutDown()
        {
            if (ROS2)
            {
                ROS2_NativePluin.ros_shutdown();
            }
            else
            {
                ROS1_NativePluin.ros_shutdown();
            }
            Config = null;
        }

        public bool ROS_OK
        {
            get
            {
                if (ROS2)
                {
                    return ROS2_NativePluin.ros_ok();
                }
                else
                {
                    return ROS1_NativePluin.ros_ok();
                }
            }
        }

        public void Advertise_tf2_msgs_TFMessage(string topic, uint queue_size, bool latch = false)
        {
            if (ROS2)
            {
                ROS2_NativePluin.advertise_tf2_msgs_TFMessage(topic, queue_size, latch);
            }
            else
            {
                ROS1_NativePluin.advertise_tf2_msgs_TFMessage(topic, queue_size, latch);
            }
        }


        public void Advertise_geometry_msgs_PoseStamped(string topic, uint queue_size, bool latch = false)
        {
            if (ROS2)
            {
                ROS2_NativePluin.advertise_geometry_msgs_PoseStamped(topic, queue_size, latch);
            }
            else
            {
                ROS1_NativePluin.advertise_geometry_msgs_PoseStamped(topic, queue_size, latch);
            }
        }

        public void Advertise_geometry_msgs_TwistStamped(string topic, uint queue_size, bool latch = false)
        {
            if (ROS2)
            {
                ROS2_NativePluin.advertise_geometry_msgs_TwistStamped(topic, queue_size, latch);
            }
            else
            {
                ROS1_NativePluin.advertise_geometry_msgs_TwistStamped(topic, queue_size, latch);
            }
        }

        internal void Advertise_sensor_msgs_CameraInfo(string topic, uint queue_size, bool latch = false)
        {
            if (ROS2)
            {
                ROS2_NativePluin.advertise_sensor_msgs_CameraInfo(topic, queue_size, latch);
            }
            else
            {
                ROS1_NativePluin.advertise_sensor_msgs_CameraInfo(topic, queue_size, latch);
            }
        }
        internal void Advertise_sensor_msgs_Image(string topic, uint queue_size, bool latch = false)
        {
            if (ROS2)
            {
                ROS2_NativePluin.advertise_sensor_msgs_Image(topic, queue_size, latch);
            }
            else
            {
                ROS1_NativePluin.advertise_sensor_msgs_Image(topic, queue_size, latch);
            }
        }
        internal void Advertise_sensor_msgs_PointCloud2(string topic, uint queue_size, bool latch = false)
        {
            if (ROS2)
            {
                ROS2_NativePluin.advertise_sensor_msgs_PointCloud2(topic, queue_size, latch);
            }
            else
            {
                ROS1_NativePluin.advertise_sensor_msgs_PointCloud2(topic, queue_size, latch);
            }
        }
        internal void Advertise_autoware_msgs_VehicleStatus(string topic, uint queue_size, bool latch = false)
        {
            if (ROS2)
            {
                ROS2_NativePluin.advertise_autoware_msgs_VehicleStatus(topic, queue_size, latch);
            }
            else
            {
                ROS1_NativePluin.advertise_autoware_msgs_VehicleStatus(topic, queue_size, latch);
            }
        }

        public void Subscribe_autoware_msgs_VehicleCmd(string topic, uint queue_size, IntPtr call_back)
        {
            if (ROS2)
            {
                ROS2_NativePluin.subscribe_autoware_msgs_VehicleCmd(topic, queue_size, call_back);
            }
            else
            {
                ROS1_NativePluin.subscribe_autoware_msgs_VehicleCmd(topic, queue_size, call_back);
            }
        }

        internal void Publish_Transform(string topic, string frame, string child, ROS_MSG_geometry_msgs_Transform transform)
        {
            if (ROS2)
            {
                ROS2_NativePluin.publish_Transform(topic, frame, child, transform);
            }
            else
            {
                ROS1_NativePluin.publish_Transform(topic, frame, child, transform);
            }
        }

        public void Publish_Pose(string topic, string frame, Transform transform)
        {
            if (ROS2)
            {
                ROS2_NativePluin.publish_Pose(topic, frame, transform);
            }
            else
            {
                ROS1_NativePluin.publish_Pose(topic, frame, transform);
            }
        }

        public void Publish_Twist(string topic, string frame, Rigidbody rigidbody)
        {
            if (ROS2)
            {
                ROS2_NativePluin.publish_Twist(topic, frame, rigidbody);
            }
            else
            {
                ROS1_NativePluin.publish_Twist(topic, frame, rigidbody);
            }
        }

        public void Publish_CameraInfo(string topic, string frame_id, uint width, uint height, float fxy)
        {
            if (ROS2)
            {
                ROS2_NativePluin.publish_CameraInfo(topic, frame_id, width, height, fxy);
            }
            else
            {
                ROS1_NativePluin.publish_CameraInfo(topic, frame_id, width, height, fxy);
            }
        }

        public unsafe void Publish_Image(string topic, string frame_id, uint seq, uint width, uint height, byte* data)
        {
            if (ROS2)
            {
                ROS2_NativePluin.publish_Image(topic, frame_id, seq, width, height, data);
            }
            else
            {
                ROS1_NativePluin.publish_Image(topic, frame_id, seq, width, height, data);
            }
        }

        public unsafe void Publish_PointCloud2(string topic, string frame_id, uint length, byte* data)
        {
            if (ROS2)
            {
                ROS2_NativePluin.publish_PointCloud2(topic, frame_id, length, data);
            }
            else
            {
                ROS1_NativePluin.publish_PointCloud2(topic, frame_id, length, data);
            }
        }

        public void Publish_VehicleStatus(
            string topic,
            string frame_id,
            string tm,
            int drivemode,
            int steeringmode,
            int gearshift,
            double speed,
            int drivepedal,
            int brakepedal,
            double angle,
            int lamp,
            int light)
        {
            if (ROS2)
            {
                ROS2_NativePluin.publish_VehicleStatus(topic, frame_id, tm, drivemode, steeringmode, gearshift, speed, drivepedal, brakepedal, angle, lamp, light);
            }
            else
            {
                ROS1_NativePluin.publish_VehicleStatus(topic, frame_id, tm, drivemode, steeringmode, gearshift, speed, drivepedal, brakepedal, angle, lamp, light);
            }
        }
    }
}