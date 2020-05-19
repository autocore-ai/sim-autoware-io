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

namespace AutoCore.Sim.Autoware.IO
{
    public class Publisher_tf : ROS_Publisher
    {
        public string child = "child_frame";
        private void Awake() => topic = "tf";
        protected override void OnROSAdvertise() => ROS_Node.Instance.Advertise_tf2_msgs_TFMessage(Topic, 1);
        protected override void OnPublish() => ROS_Node.Instance.Publish_Transform(Topic, Frame, child, new ROS_MSG_geometry_msgs_Transform(transform));
    }
}