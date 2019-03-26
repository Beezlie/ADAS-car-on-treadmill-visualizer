using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;

public class CarPositionSubscriber : MonoBehaviour {

    public int numCars = 5;
    public string url = "ws://129.97.69.100:9090";

    private int numCarPrefabs = 4;
    private List<GameObject> cars;
    private List<GameObject> rosInterfaces;
    private float wheelOffset = 1f;       // offset to apply to y-coord of car so bottom of wheels touch track
    private float waypointScaling = 20f;    //real xMax = 2, yMax = 1, so scale to match virtual track 

    // Track if car is no longer visible
    private List<int> numFramesInactive;
    private List<Vector3> prevPos;

    private Vector3 initGoal = new Vector3(0, 0, 0);

    private void Awake()
    {
        // Create car objects and connector for each car
        cars = new List<GameObject>();
        rosInterfaces = new List<GameObject>();
        numFramesInactive = new List<int>();
        prevPos = new List<Vector3>();

        for (int i = 0; i < numCars; i++)
        {
            GameObject car = Instantiate(Resources.Load("Prefabs/Car_" + (i % numCarPrefabs))) as GameObject;
            BoxCollider boxCollider = car.AddComponent(typeof(BoxCollider)) as BoxCollider;
            car.name = "Car_" + i;
            car.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            car.transform.position = new Vector3(0f, wheelOffset, 0f);
            cars.Add(car);
            prevPos.Add(car.transform.position);
            numFramesInactive.Add(0);

            GameObject rosInterface = new GameObject();
            rosInterface.name = "RosInterface" + i;
            rosInterface.transform.position = new Vector3(0f, 0f, 0f);

            RosConnector rosConnector = rosInterface.AddComponent(typeof(RosConnector)) as RosConnector;
            rosConnector.Protocol = RosConnector.Protocols.WebSocketSharp;
            rosConnector.Timeout = 10;
            rosConnector.RosBridgeServerUrl = url;
            rosConnector.Awake();

            rosInterfaces.Add(rosInterface);
        }
    }

    private void Start () {
        // Create car position subscriber for each car
        for (int i = 0; i < numCars; i++)
        {
            GameObject rosInterface = rosInterfaces[i];
            PoseStampedSubscriber poseStampedSubscriber = rosInterface.AddComponent(typeof(PoseStampedSubscriber)) as PoseStampedSubscriber;
            poseStampedSubscriber.TimeStep = 0.1f;
            poseStampedSubscriber.Topic = "/car/" + i + "/pose";
            poseStampedSubscriber.PublishedTransform = rosInterface.transform;
            poseStampedSubscriber.Awake();
        }
    }

    void Update()
    {
        for (int i = 0; i < numCars; i++)
        {
            GameObject car = cars[i];
            GameObject rosInterface = rosInterfaces[i];
            RosConnector rosConnector = rosInterface.GetComponent<RosConnector>();

            // Update the positions of cars in visualization
            PoseStampedSubscriber poseStampedSubscriber = rosInterface.GetComponent<PoseStampedSubscriber>();
            float x = poseStampedSubscriber.position.x * waypointScaling + 24;      //TODO - fix the 24
            float z = poseStampedSubscriber.position.z * waypointScaling;
            float y = wheelOffset;
            Vector3 carPos = new Vector3(x, y, z);
            car.transform.position = carPos;
            car.transform.rotation = poseStampedSubscriber.rotation;

            numFramesInactive[i] = prevPos[i] == poseStampedSubscriber.position ? numFramesInactive[i] + 1 : 0;
            prevPos[i] = poseStampedSubscriber.position;

            // Remove car from visualization if it is not detected by camera
            bool isCarActive = numFramesInactive[i] > 50 ? false : true;
            car.SetActive(isCarActive);
        }
    }
}
