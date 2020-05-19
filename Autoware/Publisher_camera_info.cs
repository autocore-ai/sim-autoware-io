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

namespace AutoCore.Sim.Autoware.IO
{
    [RequireComponent(typeof(Camera))]
    public class Publisher_camera_info : ROS_Publisher
    {
        new Camera camera;
        Camera Camera
        {
            get
            {
                if (camera == null)
                {
                    camera = GetComponent<Camera>();
                }
                return camera;
            }
        }
        public RenderTexture renderTexture;
        public RenderTexture RenderTexture
        {
            get
            {
                if (renderTexture == null)
                {
                    renderTexture = Camera.targetTexture;
                }
                return renderTexture;
            }
        }

        private void Awake()
        {
            frame = frame ?? name;
            topic = $"{name}/camera_info";
        }

        protected override void OnROSAdvertise()
        {
            ROS_Node.Instance.Advertise_sensor_msgs_CameraInfo(Topic, 1, true);
            ROS_Node.Instance.Publish_CameraInfo(Topic, name, (uint)RenderTexture.width, (uint)RenderTexture.height, RenderTexture.height / Mathf.Tan(Mathf.Deg2Rad * Camera.fieldOfView / 2) / 2);
        }
    }
}