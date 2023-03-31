# Co-VR
Co-Located Virtual Reality, or Co-VR, centers around creating VR experiences that map players in the same physical and virtual space. Players can interact and see each other in the same virtual world while only being feet apart. We created this Unity package and associated documentation to help other Unity VR developers create Co-Located Experiences in Virtual Reality. We hope this tool will help add more exciting and groundbreaking VR experiences to the community.

## Installation

> Follow these directions carefully, as an incorrect setup may result in unexpected behavior or an unusable co-location setup.

### Import the Co-VR Package

### Create Necessary GameObjects

Several GameObjects are necessary for co-location to work. We will first go through the GameObjects not involving proprietary software or components, then discuss GameObjects involving proprietary software.

#### Non-Proprietary GameObjects

##### Wall Creation

Since Meta Quest does not allow for transmission of the native Guardian boundary to other headsets (and also has a hard limit of 15 m x 15 m area), this package opts for disabling the native Guardian boundary and creating a new one using a procedural mesh system. 

##### Origin

##### Passthrough Toggle

##### Guardian Prefab

##### GUI Manager

##### Parent Object

##### Ground & Ground Collider (Optional)

This is a simple ground object that 

##### Teleport Anchors (Optional)



#### Proprietary GameObjects

##### Realtime + VR Player (Normcore)

##### CoVRCameraRig (Oculus Integration)

This is a modified version of the OVRCameraRig prefab that ships with the Oculus Integration package on the Unity Asset Store. 

## Troubleshooting

