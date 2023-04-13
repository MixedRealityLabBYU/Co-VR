# WallDrawer



## Properties

### wall
*Gameobject wall*
<br>Virtual "walls" or boundaries drawn by the player.

### XRRig
*GameObject XRRig*
<br>The rig required for VR interaction with wall drawing.

### snapDistance
*float snapDistance*
<br>Determines the maximum distance in which the final drawn wall will snap to the original drawn corner.

### wallHeight
*float wallHeight*
<br>Determines the height of the walls, starting at y == 0 and going until y == wallheight

### vertices
*List\<Vector3\> vertices*
<br>A list of Vector3 points that represent the local positions of the vertices of the wall mesh.

### triangles
*List\<int\> triangles*
<br>A list of integers corresponding to indices in the `vertices` array. Each triangle that appears in the wall mesh has three entries in the vertices array that correspond to the three points of the triangle in the mesh.

### drawing
*bool drawing*
<br>A simple boolean to measure whether or not walls are still being drawn.

### finished
*bool finished*
<br>A simple boolean to measure whether or not walls have been completely drawn.

<br>

## Methods

### Start()
*void Start()*
<br>Creates a new Vector3 list and a new int list before the first frame and sets 'vertices' and 'triangles' to them repsectively.

### Update
*void Update()*
<br>Dynamically (once per frame) updates the wall mesh as the player draws it (according to the position of the player's drawing controller). If the player is not drawing a wall, the function will do nothing.

### AddCorner()
*public void AddCorner(Vector3 corner)*
<br>Checks whether or not the player is drawing a wall and adds a new corner to the wall mesh if so. If there are no exisiting corners, the function will add the first corner. If the player is not drawing a wall, the function will do nothing.

### RemoveLastCorner()
*public void RemoveLastCorner()*
<br>Checks how many corners have been placed and removes the last one. If the player is not drawing a wall, the function will do nothing.

### ClearWall()
*public void ClearWall()*
<br>Clears the wall mesh and resets the `vertices` and `triangles` lists.

### GetTriangles()
*public List\<int\> GetTriangles()*
<br>A simple helper function that returns the value of the `triangles` list.

### GetVertices()
*public List\<Vector3\> GetVertices()*
<br>A simple helper function that returns the value of the `vertices` list.

### GetFinished()
*public bool GetFinished()*
<br>A simple helper function that returns the value of the `finished` boolean.

### UpdateGuardian()
*public void UpdateGuardian()*
<br>Updates the wall mesh to match the current player-defined wall/mesh.
