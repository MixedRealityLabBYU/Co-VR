# Co-VR
Co-Located Virtual Reality, or Co-VR, centers around creating VR experiences that map players in the same physical and virtual space. Players can interact and see each other in the same virtual world while only being feet apart. We created this Unity package and associated documentation to help other Unity VR developers create Co-Located Experiences in Virtual Reality. We hope this tool will help add more exciting and groundbreaking VR experiences to the community.

# Installation

> Follow these directions carefully, as an incorrect setup may result in unexpected behavior or an unusable co-location setup.

## Import the Co-VR Package

> Note: DO NOT import the Co-VR package until making sure you have ALL the dependency packages imported properly. Co-location will not work otherwise.

The following list details packages that Co-VR is dependent upon. All packages can be found on the Unity Asset Store or are imported into a project by default: 

| Name                                | Version  |
|-------------------------------------|----------|
| com.normalvr.normcore               | 2.4.1    |
| com.unity.xr.oculus                 | 3.2.2    |
| com.unity.xr.openxr                 | 1.5.3    |
| com.unity.textmeshpro               | 3.0.6    |
| com.unity.ugui                      | 1.0.0    |
| com.unity.xr.interaction.toolkit    | 2.0.4    |
| com.unity.xr.management             | 4.2.1    |
| com.unity.test-framework            | 1.1.31   |
| com.unity.timeline                  | 1.6.4    |
| Oculus Integration                  | 50.0     |

> Note: [Normcore SDK](https://normcore.io/) is a package owned by [Normal VR](https://www.normalvr.com/) that allows for multiplayer functionality. Due to the limited time and resources of our project, we opted to use Normcore to implement multiplayer functionality. However, Normcore is not required for co-location to work. If you wish to use a different multiplayer SDK, you will need to modify the code in the Co-VR package to work with your SDK.

Once the dependencies have been imported, do the following:

1. Search for Co-VR on the Unity Asset Store and add it to your assets.
2. In an open project in Unity Editor, open *Package Manager* by selecting **Window > Package Manager**.
3. On the **Packages: My Assets** tab at the top of the Package Manager, select the Co-VR package and click **Download** in the bottom right.
4. After the download is complete, click the **Import** button that appears next to the **Download** button.
5. When a pop-up window appears, click the **All** button and then the **Import** button in the pop-up window.

Once the package is imported into the project, you will need to move some files around.

- Move `CoVRCameraRig.prefab` and `Guardian.prefab` into the `Assets/Resources` folder.

Next, you will create the GameObjects necessary for co-location to work in your project.

------

## Create Necessary GameObjects

Several GameObjects are necessary for co-location to work. We will first go through the GameObjects not involving proprietary software or components, then discuss GameObjects involving proprietary software.

> Note: Component settings marked with an asterisk (*) are recommended settings. These settings are not required for co-location to work, but are recommended for the best experience. However, they need to be specified in order for the GameObject to work properly.

### Non-Proprietary GameObjects

#### Wall Creation

Since Meta Quest does not allow for transmission of the native Guardian boundary to other headsets (and also has a hard limit of 15 m x 15 m area), this package opts for disabling the native Guardian boundary and creating a new one using a procedural mesh system. 
> Note: the native Guardian boundary on the Meta Quest does not have to be disabled if the desired play area is less than 15 m x 15 m.

**Components:**
- [Wall Drawer (Script)](src/WallDrawer.md)
    - Wall: Guardian.prefab
    - XR Rig: CoVRCameraRig.prefab
    - Snap Distance: 0.1*
    - Wall Height: 3*
- [On Button Press (Script)](src/OnButtonPress.md)
    - Action: gripButton [RightHand XR Controller]*
    - On Press: Wall Creation (Wall Drawer) > WallDrawer.AddCorner
- [On Button Press (Script)](src/OnButtonPress.md)
    - Action: secondaryButton [RightHand XR Controller]*
    - On Press: Wall Creation (Wall Drawer) > WallDrawer.RemoveLastCorner

<img src="images/WallCreationInspector.png" alt="Wall Creation Component Settings" width="600">

#### Origin

The Origin is the point of reference that each headset uses to orient their virtual worlds in the correct physical location and rotation. The host sets the position and rotation of the origin, and the guests then use that to align their virtual worlds to match the host's.

**Components:**
- [Origin (Script)](src/Origin.md)
    - XR Camera: Center Eye Anchor (Transform) - found in CoVRCameraRig
- Teleportation Anchor
    - Interaction Manager: XR Interaction Manager (XR Interaction Manager) - found in CoVRCameraRig
    - Match Orientation: Target Up and Forward
    - Teleporation Provider: CoVRCameraRig (Teleportation Provider) - found in CoVRCameraRig
- [On Button Press (Script)](src/OnButtonPress.md)
    - Action: thumbstickClicked [RightHand XR Controller]*
    - On Press: Origin > Origin.SetOrigin
- Realtime Transform (Script) - Normcore package - DISABLED
    - Sync: Position true, Rotation true, Scale true
- Realtime View (Script) - Normcore package - DISABLED
    - Components: Origin (Realtime Transform)

<img src="images/OriginInspector.png" alt="Origin Component Settings" width="600">

#### Passthrough Toggle

The Passthrough Toggle is a simple GameObject that allows the user to toggle the passthrough camera on and off. This is useful for when the user needs to see their surroundings without taking off the headset.

**Components:**
- Toggle Passthrough (Script)
    - XR Rig: CoVRCameraRig.prefab
    - Main Camera: CenterEyeAnchor (Camera) - found in CoVRCameraRig

<img src="images/PassthroughToggleInspector.png" alt="Passthrough Toggle Component Settings" width="600">

#### Guardian Prefab

The Guardian prefab is used to transmit the created wall data to the guests. It updates when the host joins a Normcore session.

**Components:**
- Mesh Filter
    - Mesh: None (Mesh) - set at runtime
- Mesh Renderer
    - Materials: None (Material) - set at runtime
- Guardian (Script)
    - Wall Color: #00E5FF*
    - Wall Material: None (Material)*
- Realtime View (Script) - Normcore package
    - Components: Guardian (Guardian)

<img src="images/GuardianInspector.png" alt="Guardian Component Settings" width="600">

#### GUI Manager

The GUI Manager manages the user interface during co-location setup. It is responsible for displaying the correct UI elements at the correct times, including the Guardian wall creation UI, the origin setting UI, and the passthrough toggle UI.

**Components:**
- Gui Manager (Script)
    - Current Phase: MAIN_MENU
    - Main Menu: MainMenu (GameObject)*
    - Host Menu: HostMenu (GameObject)*
    - Guardian Instruction Menu: GuardianInstructions (GameObject)*
    - Guardian Confirmation: GuardianConfirmation (GameObject)*
    - Origin Menu: OriginMenu (GameObject)*
    - Set Room Prompt: HostRoomNamePrompt (GameObject)*
    - Wall Creation: Wall Creation (GameObject)
    - Origin Setting: Origin (GameObject)
    - Set Room Name Input: RoomNameInputField (TMP_InputField)*
    - Guest Menu: GuestMenu (GameObject)*
    - Join Room Prompt: JoinRoomNamePrompt (GameObject)*
    - Teleporting Prompt: TeleportingPrompt (GameObject)*
    - Join Room Name Input: RoomNameInputField (TMP_InputField)*
    - Realtime: Realtime + VR Player (Realtime) - found in Normcore package
    - Default Guardian: Guardian.prefab
    - In Game Menu: In-Game Menu Boom Arm (GameObject)*
    - Scene Wrapper: \<Parent object of all GameObjects in the scene not involved in co-location\>
    - Ground: Ground (GameObject)*
    - Head Line: Headline (GameObject)* - found in CoVRCameraRig
    - Passthrough Toggle: Passthrough Toggle (GameObject)
    - Left Hand: OculusTouchForQuest2LeftModel - found in Oculus Integration package
    - Right Hand: OculusTouchForQuest2RightModel - found in Oculus Integration package

<img src="images/GuiManagerInspector.png" alt="GUI Manager Component Settings" width="600">

#### Parent Object

Currently, in order for "teleporting" around the scene to work in co-location, all GameObjects that are not involved in co-location must be parented to a single GameObject. This GameObject is then teleported around the scene, and all other GameObjects follow it.

**Components:**
- Realtime Transform (Script) - Normcore package
    - Sync: Position true, Rotation true
- Realtime View (Script) - Normcore package
    - Components: Parent Object (Realtime Transform)

<img src="images/ParentObjectInspector.png" alt="Parent Object Component Settings" width="600">

#### Ground & Ground Collider (Optional)

The Ground and Ground Collider GameObjects are used to prevent objects with gravity from falling through the floor. The Ground GameObject is a simple cylinder, and the Ground Collider is an invisible mesh that spans the entire play area.

**Components:**

Ground
- Cylinder (Mesh Filter)
    - Mesh: Cylinder
- Mesh Renderer
    - Materials: GroundMat (Material)

Ground Collider
- Plane (Mesh Filter)
    - Mesh: Plane
- Mesh Collider
    - Mesh: Plane

<img src="images/GroundInspector.png" alt="Ground Component Settings" width="600">

<img src="images/GroundColliderInspector.png" alt="Ground Collider Component Settings" width="600">

#### Teleport Anchors (Optional)

The Teleport Anchors are used to teleport the Parent Object around the scene. They are placed where the parent object should teleport to. The Teleport Anchors are invisible, and are only used for teleportation.

**Components:**
None

### Proprietary GameObjects

#### Realtime + VR Player (Normcore)

This is a modified version of the Realtime + VR Player prefab that ships with the Normcore package on the Unity Asset Store. It creates a Normcore session and handles the connection between the host and the guests. It also creates the avatars for the host and the guests.

**Components:**
- Realtime (Script) - Normcore package
    - App Key: \<Your Normcore App Key\>
    - Join Room on Start: false
    - Room Name: \<Your Normcore Room Name\>
- Realtime Avatar Manager (Script)
    - Local Avatar Prefab: VR Player
    - Root: CoVRCameraRig (Transform)
    - Head: CenterEyeAnchor (Transform)
    - Left Hand: LeftHandAnchor (Transform)
    - Right Hand: RightHandAnchor (Transform)

<img src="images/RealtimeVRPlayerInspector.png" alt="Realtime + VR Player Component Settings" width="600">

#### CoVRCameraRig (Oculus Integration)

This is a modified version of the OVRCameraRig prefab that ships with the Oculus Integration package on the Unity Asset Store. It performs various functions regarding locomotion and visualization. It also handles the passthrough camera, allowing for passthrough to be turned on and off.

**Components:**

- OVR Camera Rig (Script) - Oculus Integration package
    - Use Per Eye Cameras: false
- OVR Manager (Script) - Oculus XR Plugin package
    - Tracking Origin Type: Floor Level
    - Quest Features [General] > Requires System Keyboard: true
    - Quest Features [General] > Tracked Keyboard Support: Required
    - Quest Features [General] > Passthrough Support: Supported
    - Quest Features [Experimental] > Experimental Features Enabled: true
    - Enable Passthrough: true
- OVR Passthrough Layer (Script) - Oculus Integration package
    - Placement: Underlay
- Teleport Player (Script)
    - Anchor: Origin (Teleportation Anchor)
    - Provider: CoVRCameraRig (Teleportation Provider)
- XR Origin
    - Origin Base GameObject: CoVRCameraRig (GameObject)
    - Camera Floor Offset Object (TrackingSpace) - found in CoVRCameraRig
    - Camera GameObject: CenterEyeAnchor (Camera) - found in CoVRCameraRig
    - Tracking Origin Mode: Floor
- Locomotion System
    - Timeout: 10*
    - XR Origin: CoVRCameraRig (XR Origin)
- Teleportation Provider
    - System: CoVRCameraRig (Locomotion System)
- Realtime View (Script) - Normcore package - DISABLED
    - Components: CoVRCameraRig (Realtime Avatar)
    - Child Views:
        - CenterEyeAnchor (Realtime View)
        - LeftHandAnchor (Realtime View)
        - RightHandAnchor (Realtime View)
        - LeftHandGrabber (Realtime View)
        - RightHandGrabber (Realtime View)
- Realtime Avatar (Script) - Normcore Package - DISABLED
    - Head: CenterEyeAnchor (Transform) - found in CoVRCameraRig
    - Left Hand: LeftHandAnchor (Transform) - found in CoVRCameraRig
    - Right Hand: RightHandAnchor (Transform) - found in CoVRCameraRig

<img src="images/CoVRCameraRigInspector1.png" alt="CoVRCameraRig Component Settings 1" width="600">

<img src="images/CoVRCameraRigInspector2.png" alt="CoVRCameraRig Component Settings 2" width="600">

<img src="images/CoVRCameraRigInspector3.png" alt="CoVRCameraRig Component Settings 3" width="600">



# Troubleshooting

