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
    public abstract class ROS_Publisher : ROS_Behaviour
    {
        public string frame = string.Empty;
        public virtual string Frame
        {
            get
            {
                if (string.IsNullOrEmpty(frame))
                {
                    if (transform.parent)
                    {
                        frame = transform.parent.name;
                    }
                    else
                    {
                        frame = "world";
                    }
                }
                return frame;
            }
        }
        protected bool OK { get; private set; }
        public uint hz = 0;
        float nextTimeStep;
        protected void Start()
        {
            if (ROS_Node.Instance.ROS_OK)
            {
                OnROSAdvertise();
                OK = true;
            }
        }
        protected void Update()
        {
            if (OK)
            {
                if (hz == 0)
                {
                    if (ROS_Node.Instance.ROS_OK)
                    {
                        OnPublish();
                    }
                }
                else if(Time.time > nextTimeStep)
                {
                    nextTimeStep += 1f / hz;
                    if (ROS_Node.Instance.ROS_OK)
                    {
                        OnPublish();
                    }
                }
            }
        }
        protected void OnDestroy() => OK = false;
        protected abstract void OnROSAdvertise();
        protected virtual void OnPublish() { }
    }
}