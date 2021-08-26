using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTest : MonoBehaviour
{
    // Start is called before the first frame update

    EdgeCollider2D edgeCollider;
    void Start()
    {
        edgeCollider = GetComponent<EdgeCollider2D>();
        List<Vector2> points = new List<Vector2> { new Vector2(0, 0), new Vector2(10, 0) };

        Vector2[] init_points = { new Vector2(0, 0), new Vector2(0, 0) };
        edgeCollider.points = init_points;

    }

    // Update is called once per frame
    void Update()
    {
        List<Vector2> points = new List<Vector2> { new Vector2(Random.Range(-10, 0), 5), new Vector2(7, 5) };
        edgeCollider.SetPoints(points);
    }
}
