using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour {

    private GameObject trackData;
    private GameObject road;      
    private float roadLength;     

    private void Awake()
    {
        //Get and store a reference to the main track object and track data
        trackData = GameObject.Find("TrackData");
        road = GameObject.Find("Road_1");
        roadLength = road.gameObject.GetComponent<MeshRenderer>().bounds.size.z;
    }

    void Update () {
        //Move object with same velocity as the track
        float trackVel = trackData.gameObject.GetComponent<TrackSpeedSubscriber>().GetVelocity();
        float offset = Time.deltaTime * trackVel * 20;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - offset);

        //Check if the difference along the z axis between the main Camera and the position of the object this is attached to is greater than groundHorizontalLength.
        if (transform.position.z < -roadLength)
        {
            //If true, this means this object is no longer visible and we can safely move it forward to be re-used.
            RepositionBackground();
        }
    }

    //Moves the object this script is attached to right in order to create our looping background effect.
    private void RepositionBackground()
    {
        //This is how far to the right we will move our background object, in this case, twice its length. This will position it directly to the right of the currently visible background object.
        Vector3 groundOffSet = new Vector3(0, 0, roadLength * 5f);

        //Move this object from it's position offscreen, behind the player, to the new position off-camera in front of the player.
        transform.position = transform.position + groundOffSet;
    }
}
