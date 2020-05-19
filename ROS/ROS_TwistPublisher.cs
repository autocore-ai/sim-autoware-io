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
    [RequireComponent(typeof(Rigidbody))]
    public abstract class ROS_TwistPublisher : ROS_Publisher
    {
        new Rigidbody rigidbody;
        public Rigidbody Rigidbody
        {
            get
            {
                if (rigidbody == null)
                {
                    rigidbody = GetComponent<Rigidbody>();
                }
                return rigidbody;
            }
        }
        protected override void OnROSAdvertise() => ROS_Node.Instance.Advertise_geometry_msgs_TwistStamped(Topic, 1);
        protected override void OnPublish() => ROS_Node.Instance.Publish_Twist(Topic, Frame, Rigidbody);
    }
}