using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManage3 : MonoBehaviour
{
    const int gridx = 198 * 2;
    const int gridy = 108 * 2;
    Grid grid;

    // Start is called before the first frame update
    Texture2D texture;

    void Start()
    {
        texture = new Texture2D(gridx, gridy);
        texture.filterMode = FilterMode.Point;
        grid = new Grid(gridx, gridy, texture);

        Renderer rend = gameObject.GetComponentInChildren<MeshRenderer>();
        //rend.material.EnableKeyword("_MAINTEX");
        rend.material.SetTexture("_MainTex", texture);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void move_grid()
    {

        if (update_from_left)
        {
            for (int r = 0; r < gridx; r++)
            {
                for (int c = 0; c < gridy; c++)
                {

                    grid[r, c].vel += new Vector2(0, 0.4f);
                    move_cell(r, c);
                }
            }
        }
        else
        {
            for (int r = gridx - 1; r >= 0; r--)
            {
                for (int c = gridy - 1; c >= 0; c--)
                {

                    grid[r, c].vel += new Vector2(0, 0.4f);
                    move_cell(r, c);
                }
            }
        }

        update_from_left = !update_from_left;

        //reset
        for (int x = 0; x < gridx; x++)
        {
            for (int y = 0; y < gridy; y++)
            {
                grid[x, y].updated = false;
            }
        }

        update_colour();
        texture.SetPixels(col_grid);
        texture.Apply();
    }

    void move_cell(int r, int c)
    {
        if (grid[r, c].updated)
        {
            return;
        }

        int type = grid[r, c].type;

        if (type == 1)
        {
            sand_move(r, c);
        }

        if (type == 2)
        {
            water_move(r, c);
        }
    }



}
