# Guardian

Manages the creation of the network guardian object in co-location setup.

## Properties

### verticesJSON
*protected string verticesJSON*<br>
The JSON string for the vertices of the wall.

### trianglesJSON
*protected string trianglesJSON*<br>
The JSON string for the triangles of the wall.

### wallMesh
*private Mesh wallMesh*<br>
The mesh of the wall. Updated when the vertices or triangles are changed.

### wallColor
*public Color wallColor*<br>
The color of the wall. Defaults to cyan.

### wallMaterial
*public Material wallMaterial*<br>
The material of the wall. Defaults to the default sprite material.

### verticesJSONprev
*private string verticesJSONprev*<br>
A temp variable used to check if the vertices have changed.

<br>

## Methods

### Start
*private void Start()*<br>
Initializes the wall mesh and sets the wall color and material.

### Update
*private void Update()*<br>
Updates the wall mesh when the vertices or triangles are changed.

### RegenerateMesh
*public void RegenerateMesh()*<br>
Regenerates the wall mesh. Called when the vertices or triangles are changed. Clears the mesh if the vertices and triangles are empty.

### SetVertices
*public void SetVertices(List<Vector3> newVertices)*<br>
Sets the vertices of the wall.

### SetTriangles
*public void SetTriangles(List<int> newTriangles)*<br>
Sets the triangles of the wall.

### SetVerticesJson
*public void SetVerticesJson(string newVerticesJson)*<br>
Sets the vertices of the wall from a JSON string.

### GetVerticesJson
*public string GetVerticesJson()*<br>
Gets the vertices of the wall as a JSON string.

### SetTrianglesJson
*public void SetTrianglesJson(string newTrianglesJson)*<br>
Sets the triangles of the wall from a JSON string.

### GetTrianglesJson
*public string GetTrianglesJson()*<br>
Gets the triangles of the wall as a JSON string.
