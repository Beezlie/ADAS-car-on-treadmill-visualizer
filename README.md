# ADAS-car-on-treadmill-visualizer
Visualization tool designed for the ADAS-on-a-treadmill system used by the University of Waterloo Real-time Embedded Systems Lab.  The visualizer is built into Unity Game Engine and subscribes to ROS topics such as the treadmill speed and car positions using a library called [ROS Sharp](https://github.com/siemens/ros-sharp).  

## Instructions
Steps to run visualizer: 
1. Install [Unity](https://unity3d.com/get-unity/download/archive), currently tested on version 2018.2.18. Should work on later versions.
2. Open the project folder "visualization" in Unity 
3. Once the ADAS-on-a-treadmill system is running, run the [rosbridge server](http://wiki.ros.org/rosbridge_suite/Tutorials/RunningRosbridge) in a separate terminal:
<br>
<center>`roslaunch rosbridge_server rosbridge_websocket.launch` </center>
<br>
4. Open the scene "Visualization Environment" in Unity and click play. 

![](demo.gif)


