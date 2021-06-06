using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    public int gridx;
    public int gridy;

    public Cell[,] grid;
    public Color[] col_grid;

    private Texture2D texture;

    public Grid(int gridx, int gridy, Texture2D text, bool fill = true)
    {
        this.gridx = gridx;
        this.gridy = gridy;

        this.grid = new Cell[gridx, gridy];
        this.col_grid = new Color[gridx * gridy];

        this.texture = text;

        if(fill)
        {
            texture = new Texture2D(gridx, gridy);
            texture.filterMode = FilterMode.Point;

            Cell blankCell = new Cell();
            for (int x = 0; x < gridx; x++)
            {
                for (int y = 0; y < gridy; y++)
                {
                    grid[x, y] = blankCell;
                }
            }
        }
    }

    public void update_colour()
    {
        for (int x = 0; x < gridx; x++)
        {
            for (int y = 0; y < gridy; y++)
            {
                col_grid[y * gridx + x] = grid[x, y].col;
            }
        }
    }

    public bool in_bound(int x, int y)
    {
        return 0 <= x && x < gridx && 0 <= y && y < gridy;
    }

    public bool swap(int x1, int y1, int x2, int y2)
    {
        if (in_bound(x1, y1) && in_bound(x2, y2))
        {

            if (grid[x2, y2].type == 0)
            {
                grid[x1, y1].updated = true;
                Cell temp = grid[x1, y1];
                grid[x1, y1] = grid[x2, y2];
                grid[x2, y2] = temp;
                return true;
            }
        }

        return false;
    }

    public bool pass_velocity(int x, int y, Vector2 vel)
    {
        if (in_bound(x, y))
        {
            grid[x, y].vel += vel;
            return true;
        }

        return false;
    }
}
