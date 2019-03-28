# ADAS-car-on-treadmill-visualizer
Visualization tool for the ADAS-on-a-treadmill system used by the University of Waterloo Real-time Embedded Systems Lab

![](demo.gif)

## Visualizer Integration
The visualizer is completely built into Unity Game Engine. Steps to run visualizer: 
1. Install [Unity](https://unity3d.com/get-unity/download/archive), currently tested on version 2018.2.18. Should work on later versions.
2. Open project folder "visualization" in Unity 
3. Separate from Unity, once the ADAS system is running, run the [rosbridge server](http://wiki.ros.org/rosbridge_suite/Tutorials/RunningRosbridge) in a separate terminal: `roslaunch rosbridge_server rosbridge_websocket.launch`
4. Open scene "Visualization Environment" in Unity and click play. Unity scene subscribes to various RC car ROS topics using a Unity library called [ROS Sharp](https://github.com/siemens/ros-sharp). This communication system allows Unity to obtain car information every frame. 
