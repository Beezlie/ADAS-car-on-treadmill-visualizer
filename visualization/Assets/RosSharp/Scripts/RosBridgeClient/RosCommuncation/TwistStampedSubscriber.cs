using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class TwistStampedSubscriber : Subscriber<Messages.Geometry.TwistStamped>
    {
        public Vector3 linearVel;
        public Vector3 angularVel;

        public void Awake()
        {
            base.Start();
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void ReceiveMessage(Messages.Geometry.TwistStamped message)
        {
            linearVel = GetLinear(message).Ros2Unity();
            angularVel = GetAngular(message).Ros2Unity();
        }

        private Vector3 GetLinear(Messages.Geometry.TwistStamped message)
        {
            return new Vector3(
                message.twist.linear.x,
                message.twist.linear.y,
                message.twist.linear.z);
        }

        private Vector3 GetAngular(Messages.Geometry.TwistStamped message)
        {
            return new Vector3(
                message.twist.angular.x,
                message.twist.angular.y,
                message.twist.angular.z);
        }
    }
}
