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

using AutoCore.Sim.Sensor;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace AutoCore.Sim.Autoware.IO
{
    [RequireComponent(typeof(Lidar))]
    public class Publisher_points_raw : ROS_Publisher
    {
        public Lidar lidar;
        public Lidar Lidar
        {
            get
            {
                if (lidar == null)
                {
                    lidar = GetComponent<Lidar>();
                }
                return lidar;
            }
        }
        private void Awake()
        {
            frame = frame ?? name;
            topic = "points_raw";
        }
        protected override void OnROSAdvertise()
        {
            ROS_Node.Instance.Advertise_sensor_msgs_PointCloud2(Topic, 1);
            Lidar.OnLidarData += OnLidarData;
        }

        private unsafe void OnLidarData(NativeArray<float4> source)
        {
            if (OK)
            {
                var data = new NativeArray<float4>(source, Allocator.TempJob);
                var rosData = new NativeArray<float4>(data.Length, Allocator.TempJob);
                new GetRosFormatData()
                {
                    data = data,
                    rosData = rosData
                }.Schedule(data.Length, 64).Complete();
                data.Dispose();
                fixed (float4* pdata = rosData.ToArray())
                {
                    ROS_Node.Instance.Publish_PointCloud2(topic, frame, (uint)rosData.Length, (byte*)pdata);
                }
                rosData.Dispose();
            }
        }

        [BurstCompile]
        struct GetRosFormatData : IJobParallelFor
        {
            [ReadOnly] public NativeArray<float4> data;
            [WriteOnly] public NativeArray<float4> rosData;
            public void Execute(int index) => rosData[index] = new float4(data[index].z, -data[index].x, data[index].y, data[index].w);
        }
    }
}