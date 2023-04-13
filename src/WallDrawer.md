# WallDrawer

## Properties

### wall
*Gameobject wall*<br>
Virtual "walls" or boundaries drawn by the player.

### XRRig
*GameObject XRRig*

The rig required for VR interaction with wall drawing.

### snapDistance
*float snapDistance*

Determines the maximum distance in which the final drawn wall will snap to the original drawn corner.

### wallHeight
*float wallHeight*

Determines the height of the walls, starting at y == 0 and going until y == wallheight

### vertices
*List\<Vector3\> vertices*
A list of Vector3 points that represent the local positions of the vertices of the wall mesh.

### triangles
*List\<int\> triangles*
A list of integers corresponding to indices in the `vertices` array. Each triangle that appears in the wall mesh has three entries in the vertices array that correspond to the three points of the triangle in the mesh.

### drawing
*bool drawing*
A simple boolean to measure whether or not walls are still being drawn.

### finished
*bool finished*
A simple boolean to measure whether or not walls have been completely drawn.
