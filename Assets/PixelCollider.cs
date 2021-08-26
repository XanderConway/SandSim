using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelCollider : MonoBehaviour
{


    //One Node per pixel
    public class Node
    {
        Vector2 pos;
        bool active;
        int vertexIndex = -1;

        public Node(Vector2 _pos, bool _active)
        {
            pos = _pos;
            active = _active;
        }
    }
    
    public void generateCollider(Vector2 _pos, float size)
    {
        Node[,] table = new Node[Grid.width, Grid.height];

        for(int x = 0; x < Grid.width; x++ )
        {
            for(int y = 0; y < Grid.height; y++)
            {
                float x_pos = _pos.x - size * Grid.width / 2;
                float y_pos = _pos.y - size * Grid.height / 2;
                table[x, y] = new Node(new Vector2(1, 2), Grid.grid[x,y].solid);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
