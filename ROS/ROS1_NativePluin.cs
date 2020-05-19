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
using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace AutoCore.Sim.Autoware.IO
{
    public unsafe class ROS1_NativePluin
    {
        #region Native Functions
        #region Log
        const string NativePluginName = "autoware_sim_ros1";
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void UnmanagedFunctionDelegate(IntPtr strPtr);
        [DllImport(NativePluginName)]
        static extern void SetFunctionDebugLog(IntPtr fp);
        [DllImport(NativePluginName)]
        static extern void SetFunctionDebugLogWarning(IntPtr fp);
        [DllImport(NativePluginName)]
        static extern void SetFunctionDebugLogError(IntPtr fp);
        #endregion
        [DllImport(NativePluginName)]
        public static extern bool ros_master_check([MarshalAs(UnmanagedType.LPStr)]string host, int port, double timeout);
        [DllImport(NativePluginName)]
        public static extern void ros_init(int argc, [MarshalAs(UnmanagedType.LPArray)]string[] argv, [MarshalAs(UnmanagedType.LPStr)] string name);
        [DllImport(NativePluginName)]
        public static extern void ros_shutdown();
        [DllImport(NativePluginName)]
        public static extern void ros_spinOnce();
        [DllImport(NativePluginName)]
        public static extern bool ros_ok();
        #region Advertise
        [DllImport(NativePluginName)]
        public static extern int advertise_std_msgs_String([MarshalAs(UnmanagedType.LPStr)]string topic, uint queue_size, bool latch = false);
        [DllImport(NativePluginName)]
        public static extern int advertise_sensor_msgs_CameraInfo([MarshalAs(UnmanagedType.LPStr)]string topic, uint queue_size, bool latch = false);
        [DllImport(NativePluginName)]
        public static extern int advertise_sensor_msgs_Image([MarshalAs(UnmanagedType.LPStr)]string topic, uint queue_size, bool latch = false);
        [DllImport(NativePluginName)]
        public static extern int advertise_sensor_msgs_PointCloud2([MarshalAs(UnmanagedType.LPStr)]string topic, uint queue_size, bool latch = false);
        [DllImport(NativePluginName)]
        public static extern int advertise_tf2_msgs_TFMessage([MarshalAs(UnmanagedType.LPStr)]string topic, uint queue_size, bool latch = false);
        [DllImport(NativePluginName)]
        public static extern int advertise_geometry_msgs_PoseStamped([MarshalAs(UnmanagedType.LPStr)]string topic, uint queue_size, bool latch = false);
        [DllImport(NativePluginName)]
        public static extern int advertise_geometry_msgs_TwistStamped([MarshalAs(UnmanagedType.LPStr)]string topic, uint queue_size, bool latch = false);
        [DllImport(NativePluginName)]
        public static extern int advertise_autoware_msgs_VehicleStatus([MarshalAs(UnmanagedType.LPStr)]string topic, uint queue_size, bool latch = false);
        #endregion
        #region Publish
        [DllImport(NativePluginName)]
        public static extern void publish_std_msgs_String([MarshalAs(UnmanagedType.LPStr)]string topic, [MarshalAs(UnmanagedType.LPStr)]string msg);
        [DllImport(NativePluginName)]
        public static extern void publish_Image([MarshalAs(UnmanagedType.LPStr)]string topic, [MarshalAs(UnmanagedType.LPStr)]string frame_id, uint seq, uint width, uint height, byte* data);
        [DllImport(NativePluginName)]
        public static extern void publish_PointCloud2([MarshalAs(UnmanagedType.LPStr)]string topic, [MarshalAs(UnmanagedType.LPStr)]string frame_id, uint length, byte* data);
        [DllImport(NativePluginName)]
        public static extern void publish_CameraInfo([MarshalAs(UnmanagedType.LPStr)]string topic, [MarshalAs(UnmanagedType.LPStr)]string frame_id, uint width, uint height, float fxy);
        [DllImport(NativePluginName)]
        public static extern void publish_Transform([MarshalAs(UnmanagedType.LPStr)]string topic, [MarshalAs(UnmanagedType.LPStr)]string frame_id, [MarshalAs(UnmanagedType.LPStr)]string child_frame_id, ROS_MSG_geometry_msgs_Transform transform);
        [DllImport(NativePluginName)]
        public static extern void publish_Pose([MarshalAs(UnmanagedType.LPStr)]string topic, [MarshalAs(UnmanagedType.LPStr)]string frame_id, ROS_MSG_geometry_msgs_Transform pose);
        [DllImport(NativePluginName)]
        public static extern void publish_Twist([MarshalAs(UnmanagedType.LPStr)]string topic, [MarshalAs(UnmanagedType.LPStr)]string frame_id, ROS_MSG_geometry_msgs_Twist twist);
        [DllImport(NativePluginName)]
        public static extern void publish_VehicleStatus(
            [MarshalAs(UnmanagedType.LPStr)]string topic,
            [MarshalAs(UnmanagedType.LPStr)]string frame_id,
            [MarshalAs(UnmanagedType.LPStr)]string tm,
            int drivemode,
            int steeringmode,
            int gearshift,
            double speed,
            int drivepedal,
            int brakepedal,
            double angle,
            int lamp,
            int light);
        #endregion
        #region Subscribe
        [DllImport(NativePluginName)]
        public static extern void subscribe_std_msgs_String([MarshalAs(UnmanagedType.LPStr)]string topic, uint queue_size, IntPtr call_back);
        [DllImport(NativePluginName)]
        public static extern void subscribe_autoware_msgs_VehicleCmd([MarshalAs(UnmanagedType.LPStr)]string topic, uint queue_size, IntPtr call_back);
        #endregion
        #endregion
        #region Log
        [MonoPInvokeCallback(typeof(UnmanagedFunctionDelegate))]
        static void Log(IntPtr strPtr) => Debug.Log($"{NativePluginName}:{Marshal.PtrToStringAnsi(strPtr)}");
        [MonoPInvokeCallback(typeof(UnmanagedFunctionDelegate))]
        static void LogWarning(IntPtr strPtr) => Debug.LogWarning($"{NativePluginName}:{Marshal.PtrToStringAnsi(strPtr)}");
        [MonoPInvokeCallback(typeof(UnmanagedFunctionDelegate))]
        static void LogError(IntPtr strPtr) => Debug.LogError($"{NativePluginName}:{Marshal.PtrToStringAnsi(strPtr)}");
        public static void RegisterDebugFunctions()
        {
            UnmanagedFunctionDelegate delegate_log = Log;
            UnmanagedFunctionDelegate delegate_log_warning = LogWarning;
            UnmanagedFunctionDelegate delegate_log_error = LogError;
            SetFunctionDebugLog(Marshal.GetFunctionPointerForDelegate(delegate_log));
            SetFunctionDebugLogWarning(Marshal.GetFunctionPointerForDelegate(delegate_log_warning));
            SetFunctionDebugLogError(Marshal.GetFunctionPointerForDelegate(delegate_log_error));
        }
        #endregion
    }
}