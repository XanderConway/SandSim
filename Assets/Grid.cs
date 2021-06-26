using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    public int gridx;
    public int gridy;

    public Cell[,] grid;
    public Color[] col_grid;

    public Grid(int gridx, int gridy, bool fill = true)
    {
        this.gridx = gridx;
        this.gridy = gridy;

        this.grid = new Cell[gridx, gridy];
        this.col_grid = new Color[gridx * gridy];

        if(fill)
        {
            for (int x = 0; x < gridx; x++)
            {
                for (int y = 0; y < gridy; y++)
                {
                    grid[x, y] = new BlankCell();
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


    //The order matters for flattening
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

    public bool swap(int x1, int y1, int x2, int y2, HashSet<int> types)
    {
        if (in_bound(x1, y1) && in_bound(x2, y2))
        {
            if (types.Contains(grid[x2, y2].type))
            {
                grid[x1, y1].updated = true;
                Cell temp = grid[x1, y1];
                grid[x1, y1] = grid[x2, y2];
                grid[x2, y2] = temp;

                if(grid[x2, y2].type != 0)
                {
                    check_destory_on_contact(x1, y1);
                }

                if(grid[x1, y1].type != 0)
                {
                    check_destory_on_contact(x2, y2);
                }
                return true;
            }
        }

        return false;
    }

    void check_destory_on_contact(int x, int y)
    {
        if(grid[x, y].destroy_on_contact)
        {
            grid[x, y] = new BlankCell();
        }
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

    public bool check(int x, int y, List<int> states)
    {
        if (in_bound(x, y) && states.Contains(grid[x, y].type))
        {
            return true;
        }

        return false;
    }

    public bool check_any(int x, int y)
    {
        if (in_bound(x, y) && grid[x,y].type != 0)
        {
            return true;
        }

        return false;
    }
}
