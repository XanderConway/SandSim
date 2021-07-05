using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    public static int gridx;
    public static int gridy;

    public static Cell[,] grid;
    public static Color[] col_grid;

    public static void init_grid(int x, int y, bool fill = true)
    {
        gridx = x;
        gridy = y;

        grid = new Cell[gridx, gridy];
        col_grid = new Color[gridx * gridy];

        if(fill)
        {
            for (int r = 0; r < gridx; r++)
            {
                for (int c = 0; c < gridy; c++)
                {
                    grid[r, c] = new BlankCell();
                }
            }
        }
    }

    public static void update_colour()
    {
        for (int x = 0; x < gridx; x++)
        {
            for (int y = 0; y < gridy; y++)
            {
                col_grid[y * gridx + x] = grid[x, y].col;
            }
        }
    }

    public static bool in_bound(int x, int y)
    {
        return 0 <= x && x < gridx && 0 <= y && y < gridy;
    }


    //The order matters for flattening
    public static bool swap(int x1, int y1, int x2, int y2)
    {
        if (in_bound(x1, y1) && in_bound(x2, y2))
        {
            if (grid[x2, y2].weight < grid[x1, y1].weight)
            {
                Cell temp = grid[x1, y1];
                grid[x1, y1] = grid[x2, y2];
                grid[x2, y2] = temp;
                return true;
            }
        }

        return false;
    }


    //This Method kills framerate for some reason (passing HashSet is bad)
    public static bool swap(int x1, int y1, int x2, int y2, HashSet<int> types)
    {
        if (in_bound(x1, y1) && in_bound(x2, y2))
        {
            if (types.Contains(grid[x2, y2].id))
            {
                Cell temp = grid[x1, y1];
                grid[x1, y1] = grid[x2, y2];
                grid[x2, y2] = temp;

                return true;
            }
        }

        return false;
    }

    static void check_destory_on_contact(int x, int y)
    {
        if(grid[x, y].destroy_on_contact)
        {
            grid[x, y] = new BlankCell();
        }
    }

    public static bool pass_velocity(int x, int y, Vector2 vel)
    {
        if (in_bound(x, y))
        {
            grid[x, y].vel += vel;
            return true;
        }

        return false;
    }

    //This methods kills frame rate (don't pass Hash)
    public static bool check(int x, int y, int id)
    {
        if (in_bound(x, y) && grid[x,y].id == id)
        {
            return true;
        }

        return false;
    }


    //Returns true if something is in the specified location
    public static bool check_any(int x, int y)
    {
        if (in_bound(x, y) && grid[x,y].id != 0)
        {
            return true;
        }

        return false;
    }
}
