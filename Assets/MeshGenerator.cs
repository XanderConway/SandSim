using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{

    Mesh mesh;

    Vector3[] verts;
    int[] triangles;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        verts = new Vector3[]
        {
            new Vector3( 0, 0, 1),
            new Vector3(0, 10, 1),
            new Vector3(10, 0, 1),
            new Vector3(10, 10, 1)
        };

        triangles = new int[]
        {
            0, 1 , 2,
            1, 3, 2

        };
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = verts;
        mesh.triangles = triangles;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
