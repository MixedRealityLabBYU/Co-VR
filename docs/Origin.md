# Origin

The Origin script manages the Origin object in the scene. The Origin object is the point of reference that the headsets use to synchronize their virtual and physical spaces.

## Properties

### XRCamera
*public Transform XRCamera*
<br>The camera that will be used to determine the player's position and rotation.

### onNetwork
*private bool onNetwork*
<br>Whether the player is currently connected to the Normcore network.

### savedPosition
*public Transform savedPosition*
<br>The position and rotation of the origin before joining the network.

<br>

## Methods

### Update
*public void Update()*
<br>If the player is connected to the network, sets the transform of the origin to savedPosition. Otherwise, set savedPosition to the current transform.

### EnableNetworkTransform
*public void EnableNetworkTransform()*<br>
Enables the network transform component on the origin and requests Normcore ownership of the origin. Sets onNetwork to true.

### DisableNetworkTransform
*public void DisableNetworkTransform()*<br>
Disables the network transform component on the origin and releases Normcore ownership of the origin. Sets onNetwork to false.

### OnJoin
*public void OnJoin()*<br>
Called by a Unity Event from the GUI Manager when a player joins a Normcore room. Calls EnableNetworkTransform.

### SetOrigin
*public void SetOrigin()*<br>
Sets the transform of the origin to the current transform of the camera.
