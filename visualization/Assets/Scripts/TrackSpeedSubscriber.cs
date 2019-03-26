using UnityEngine;
using RosSharp.RosBridgeClient;

public class TrackSpeedSubscriber : MonoBehaviour {

    public string url = "ws://129.97.69.100:9090";

    private RosConnector rosConnector;
    private TwistStampedSubscriber trackVelSubscriber;

    public float GetVelocity()
    {
        return trackVelSubscriber.linearVel.z;
    }

    private void Awake()
    {
        // Initialize ROS connector
        rosConnector = gameObject.AddComponent(typeof(RosConnector)) as RosConnector;
        rosConnector.Protocol = RosConnector.Protocols.WebSocketSharp;
        rosConnector.Timeout = 10;
        rosConnector.RosBridgeServerUrl = url;
        rosConnector.Awake();
    }

    private void Start () {
        // Initialize treadmill velocity subscriber
        trackVelSubscriber = gameObject.AddComponent(typeof(TwistStampedSubscriber)) as TwistStampedSubscriber;
        trackVelSubscriber.TimeStep = 0.1f;
        trackVelSubscriber.Topic = "/treadmill/velocity";
        trackVelSubscriber.Awake();
    }
}
