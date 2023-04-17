# TeleportPlayer

This script is used to teleport a player to a teleportation anchor. It can be used to teleport the player to a teleportation anchor in the scene or to a teleportation anchor in another scene. It was created by Unity and is included in the [VR Development Pathway](https://learn.unity.com/pathway/vr-development) on Unity Learn.

## Properties

### Anchor
*public TeleportationAnchor anchor*
<br>The anchor that the player will be teleported to.

### Provider
*public TeleportationProvider provider*
<br>The teleportation provider that will be used to teleport the player.

<br>

## Methods

### Teleport
*public void Teleport()*
<br>Teleport the player to the anchor. Sets the player's position to the anchor's position and rotation to the anchor's rotation.

### CreateRequest
*public void CreateRequest()*<br>
Create a teleportation request for the player. The request will be sent to the provider and the player will be teleported to the anchor when the request is accepted.
