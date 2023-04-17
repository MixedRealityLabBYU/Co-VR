using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class WallDrawer : MonoBehaviour
{
    public GameObject wall;
    public GameObject XRRig;
    public float snapDistance = 0.1f;
    public float wallHeight = 3;

    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private bool drawing = false;
    private bool finished = false;


    // Start is called before the first frame update
    void Start()
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();

        if (finished) return;

        if (drawing && vertices.Count > 3 && triangles.Count > 5) // Update line to show current device position
        {
            Vector3 firstPoint = vertices.ElementAt(0);
            Vector3 endPoint = XRRig.transform.TransformPoint(OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch));
            bool lastPoint = Mathf.Abs(endPoint.x - firstPoint.x) <= snapDistance && Mathf.Abs(endPoint.z - firstPoint.z) <= snapDistance;
            if (lastPoint && vertices.Count > 6)
            {
                endPoint = firstPoint;
            }

            vertices.RemoveAt(vertices.Count - 1);
            vertices.RemoveAt(vertices.Count - 1);
            endPoint.y = 0;
            vertices.Add(endPoint);
            endPoint.y = wallHeight;
            vertices.Add(endPoint);

            UpdateGuardian();
        }
    }

    public void AddCorner()
    {
        if (finished)
        {
            return;
        }
        Vector3 pointToAdd = XRRig.transform.TransformPoint(OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch));
        if (!drawing) {
            drawing = true;
            // Add first point
            pointToAdd.y = 0;
            vertices.Add(pointToAdd);

            pointToAdd.y = wallHeight;
            vertices.Add(pointToAdd);
        }
        Vector3 firstPoint = vertices.ElementAt(0);
        bool lastPoint = Mathf.Abs(pointToAdd.x - firstPoint.x) <= snapDistance && Mathf.Abs(pointToAdd.z - firstPoint.z) <= snapDistance;
        if (lastPoint && vertices.Count > 6)
        {
            triangles.Add(vertices.Count - 3);
            triangles.Add(vertices.Count - 2);
            triangles.Add(0);
            triangles.Add(0);
            triangles.Add(vertices.Count - 2);
            triangles.Add(1);

            UpdateGuardian();

            drawing = false;
            finished = true;
            
            return;
        }

        pointToAdd.y = 0;
        vertices.Add(pointToAdd);
        triangles.Add(vertices.Count - 3);
        triangles.Add(vertices.Count - 2);
        triangles.Add(vertices.Count - 1);

        pointToAdd.y = wallHeight;
        vertices.Add(pointToAdd);
        triangles.Add(vertices.Count - 2);
        triangles.Add(vertices.Count - 3);
        triangles.Add(vertices.Count - 1);

        UpdateGuardian();
        
    }

    public void RemoveLastCorner()
    {
        if (finished) return;
        if (vertices.Count < 1 || triangles.Count < 1)
        {
            // No corners have been made yet
            return;
        }
        else if (vertices.Count < 5 || triangles.Count < 7)
        {
            // Only one corner has been placed, so delete remaining corner PLUS one that is constantly being refreshed
            ClearWalls();
            drawing = false;
            return;
        }

        // Remove last corner's vertices and triangles integers
        triangles.RemoveRange(triangles.Count - 6, 6);
        vertices.RemoveRange(vertices.Count - 2, 2);

        UpdateGuardian();
    }

    public void ClearWalls()
    {
        triangles.Clear();
        vertices.Clear();
        UpdateGuardian();
        drawing = false;
        finished = false;
    }

    public List<int> GetTriangles() 
    {
        return triangles;
    }

    public List<Vector3> GetVertices() 
    {
        return vertices;
    }

    public bool GetFinished()
    {
        return finished;
    }

    public void UpdateGuardian()
    {
        wall.GetComponent<Guardian>().SetVertices(vertices);
        wall.GetComponent<Guardian>().SetTriangles(triangles);
        wall.GetComponent<Guardian>().RegenerateMesh();
    }
}