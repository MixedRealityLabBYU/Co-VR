# GuiManager

Manages the GUI of the co-location setup. It is responsible for the following tasks:
- Managing the input system for each co-location step
- Keeping track of which step the user is currently in
- Displaying the correct GUI for the current step
- Setting correct values for the GUI elements
- Managing the host/guest interaction
- Managing the multiplayer session

## Properties

### Phase
*public enum Phase*
<br>The different phases of the co-location setup. The possible phases are:
- MAIN_MENU
- WALL_DRAWING
- ORIGIN_SET
- MAKE_ROOM_PROMPT
- JOIN_ROOM_PROMPT
- ORIGIN_TELEPORT
- FINISHED

### currentPhase
*public Phase currentPhase*
<br>The current phase of the co-location setup. The default value is MAIN_MENU.

### MainMenu
*public GameObject MainMenu*<br>
The canvas for the main menu. Contains the buttons to start the co-location setup as host or guest, as well as the button to quit the application.

### HostMenu
*public GameObject HostMenu*<br>
The canvases for the host menu. Goes through the wall creation and origin setting steps.

### GuardianInstructionMenu
*public GameObject GuardianInstructionMenu*<br>
The canvas for the guardian instruction menu. Contains the instructions for drawing the wall.

### GuardianConfirmation
*public GameObject GuardianConfirmation*<br>
The canvas for the guardian confirmation menu. Contains the button to confirm the wall the host has created.

### OriginMenu
*public GameObject OriginMenu*<br>
The canvas for the origin menu. Contains the instructions for setting the origin.

### SetRoomPrompt
*public GameObject SetRoomPrompt*<br>
The canvas for the set room prompt. Contains the input field for the room name.

### WallCreation
*public GameObject WallCreation*<br>
The game object for the wall creation. Contains the [WallDrawer](WallDrawer.md) script for drawing the wall.

### OriginSetting
*public GameObject OriginSetting*<br>
The game object for the origin setting. Contains the [Origin](Origin.md) script for setting the origin.

### SetRoomNameInput
*public TMP_InputField SetRoomNameInput*<br>
The input field for the room name. Used by Normcore to set the room name.

### GuestMenu
*public GameObject GuestMenu*<br>
The canvas for the guest menu. Contains the button to join a room.

### JoinRoomPrompt
*public GameObject JoinRoomPrompt*<br>
The canvas for the join room prompt. Contains the input field for the room name.

### TeleportingPrompt
*public GameObject TeleportingPrompt*<br>
The canvas for the teleporting prompt. Contains the instructions for teleporting to the origin.

### JoinRoomNameInput
*public TMP_InputField JoinRoomNameInput*<br>
The input field for the room name. Used by Normcore to join a room.

### realtime
*public Realtime realtime*<br>
The Normcore Realtime instance for the multiplayer session.

### defaultGuardian
*public GameObject defaultGuardian*<br>
The guardian prefab for the wall. Used by the [Guardian](Guardian.md) script.

### networkGuardian
*private GameObject networkGuardian*<br>
The guardian object that is network instantiated for the wall. Set at runtime.

### inGameMenu
*public GameObject inGameMenu*<br>
The canvas for the in-game menu. Contains buttons to recalibrate the origin rotation and to return to the main menu.

### sceneWrapper
*public GameObject sceneWrapper*<br>
The game object that contains the scene. All non-co-location objects should be children of this object.

### ground
*public GameObject ground*<br>
The game object for the ground during co-location setup.

### headLine
*public GameObject headLine*<br>
A red line that is attached to the main camera. Used to help calibrate the origin rotation.

### passthroughToggle
*public TogglePassthrough passthroughToggle*<br>
The game object that contains the [TogglePassthrough](TogglePassthrough.md) script for toggling passthrough mode.

### leftHand
*public GameObject leftHand*<br>
The left hand anchor from the CoVRCameraRig.

### rightHand
*public GameObject rightHand*<br>
The right hand anchor from the CoVRCameraRig.

### role
*private string role*<br>
The role of the player. Either "host" or "guest", and is set at runtime.

<br>

## Methods

### Update
*private void Update()*<br>
Updates the GUI and co-location GameObjects based on the current phase.

### RegressPhase
*public void RegressPhase()*<br>
Regresses the current phase by one step.

### ProgressPhase
*public void ProgressPhase()*<br>
Progresses the current phase by one step.

### Recalibrate
*public void Recalibrate()*<br>
Sets the current phase to the origin teleporting phase.

### QuitToMenu
*public void QuitToMenu()*<br>
Sets the current phase to the main menu phase, quits the room, and clears the wall.

### SetPhase
*public void SetPhase(Phase settingPhase)*<br>
Sets the current phase to the given phase.

### HostButton
*public void HostButton()*<br>
Sets the role to "Host" and sets the current phase to the wall drawing phase.

### GuestButton
*public void GuestButton()*<br>
Sets the role to "Guest" and sets the current phase to the join room prompt phase.

### HostRoomButton
*public void HostRoomButton()*<br>
Joins the room with the name in the set room name input field, and sets the current phase to the finished phase.

### JoinRoomButton
*public void JoinRoomButton()*<br>
Joins the room with the name in the join room name input field, and sets the current phase to the origin teleporting phase.

### JoinRoom
*public void JoinRoom(string name)*<br>
Joins the room with the given name.

### QuitRoom
*public void QuitRoom()*<br>
Quits the room.

### OnJoin
*public void OnJoin(Realtime realtime)*<br>
Called when the player joins a room. If the player is the host, the guardian is instantiated and the host is made the owner of the scene wrapper. If the player is a guest, the default guardian is disabled and the origin is enabled for network transforms.

### SpawnTorch
*public void SpawnTorch()*<br>
Spawns a torch GameObject if the player is the host.
