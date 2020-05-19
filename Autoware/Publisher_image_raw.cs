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

using UnityEngine;
using UnityEngine.Rendering;

namespace AutoCore.Sim.Autoware.IO
{
    [RequireComponent(typeof(Publisher_camera_info))]
    public class Publisher_image_raw : ROS_Publisher
    {
        RenderTexture renderTexture;
        public RenderTexture RenderTexture
        {
            get
            {
                if (renderTexture == null)
                {
                    renderTexture = GetComponent<Publisher_camera_info>().RenderTexture;
                }
                return renderTexture;
            }
        }
        private void Awake()
        {
            frame = frame ?? name;
            topic = $"{name}/image_raw";
        }

        protected override void OnROSAdvertise() => ROS_Node.Instance.Advertise_sensor_msgs_Image(Topic, 1);
        protected override void OnPublish()
        {
            base.OnPublish();
            if (OK)
            {
                AsyncGPUReadback.Request(RenderTexture, 0, TextureFormat.RGB24, OnReadBack);
            }
        }
        private unsafe void OnReadBack(AsyncGPUReadbackRequest obj)
        {
            if (OK && !obj.hasError && obj.done)
            {
                fixed (byte* data = obj.GetData<byte>().ToArray())
                {
                    ROS_Node.Instance.Publish_Image(Topic, name, (uint)Time.frameCount, (uint)obj.width, (uint)obj.height, data);
                }
            }
        }
    }
}