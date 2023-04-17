using System.Collections;
using System.Collections.Generic;
// using System;
using UnityEngine;
using Normal;
using Normal.Realtime;

public class Guardian : RealtimeComponent<NetworkData>
{
    
    protected string verticesJSON = "";
    protected string trianglesJSON = "";
    private Mesh wallMesh;
    public Color wallColor;
    public Material wallMaterial;

    private string verticesJSONprev = "";

    // Start is called before the first frame update
    void Start()
    {
        wallMesh = new Mesh { name = "Procedural Mesh" };
        if (wallColor == null) wallColor = new Color(0f, 1f, 1f, 0.3f);
        if(wallMaterial == null) wallMaterial = new Material(Shader.Find("Sprites/Default"));
        gameObject.GetComponent<MeshFilter>().mesh = wallMesh;
        gameObject.GetComponent<MeshRenderer>().material = wallMaterial;
        gameObject.GetComponent<MeshRenderer>().receiveShadows = false;
        gameObject.GetComponent<MeshRenderer>().sharedMaterial.SetColor("_Color", wallColor);

        
    }

    [System.Serializable]
    public class Vector3ListWrapper
    {
        public List<Vector3> list = new List<Vector3>();
        public Vector3ListWrapper(List<Vector3> list) { this.list = list; }
        public Vector3ListWrapper() {}
    }

    [System.Serializable]
    public class IntListWrapper
    {
        public List<int> list = new List<int>();
        public IntListWrapper(List<int> list) { this.list = list; }
        public IntListWrapper() {}
    }    

    // Update is called once per frame
    void Update()
    {
        verticesJSON = model.verticesJSON;
        trianglesJSON = model.trianglesJSON;
        if(verticesJSON != verticesJSONprev)
        {
            verticesJSONprev = verticesJSON;
            RegenerateMesh();
        }
    }

    public void RegenerateMesh() {


        if(verticesJSON == "" && trianglesJSON == "")
        {
            wallMesh.Clear();
            return;
        }

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        vertices = JsonUtility.FromJson<Vector3ListWrapper>(verticesJSON).list;
        triangles = JsonUtility.FromJson<IntListWrapper>(trianglesJSON).list;

        


        if(wallMesh.vertices.Length > vertices.Count)
        {
            wallMesh.triangles = triangles.ToArray();
            wallMesh.SetVertices(vertices);
        }
        else
        {
            wallMesh.SetVertices(vertices);
            wallMesh.triangles = triangles.ToArray();
        }

        wallMesh.RecalculateNormals();
        wallMesh.RecalculateBounds();
    }

    public void SetVertices(List<Vector3> newVertices) 
    {
       model.verticesJSON = (JsonUtility.ToJson(new Vector3ListWrapper(newVertices)));
    }

    public void SetTriangles(List<int> newTriangles)
    {
        model.trianglesJSON = (JsonUtility.ToJson(new IntListWrapper(newTriangles)));
    }
    
    public void SetVerticesJson(string newVerticesJson)
    {
        model.verticesJSON = (newVerticesJson);
    }

    public string GetVerticesJson()
    {
        return model.verticesJSON;
    }

    public void SetTrianglesJson(string newTrianglesJson)
    {
        model.trianglesJSON = (newTrianglesJson);
    }

    public string GetTrianglesJson()
    {
        return model.trianglesJSON;
    }
    
}
