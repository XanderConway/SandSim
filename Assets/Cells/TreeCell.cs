using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCell : Cell
{
    Color bark = new Color(0.5f, 0.3f, 0.2f);
    Vector2 grow_dir;

    //How many cells have been moved previously (used to align with velocity)
    Vector2 grow_history;

    //trunk thickness x10
    int width = 30;

    //branch length
    int height = 50;

    public TreeCell(Vector2 grow_vel, Vector2 grow_history, int height, int width)
    {
        this.grow_history = grow_history;
        this.grow_dir = grow_vel;
        this.width = width;
        this.height = height;
        this.col = bark;
    }

    public override void move(ref Grid grid, int x, int y)
    {


        //if (height < 1)
        //{
        //    return;
        //}

        //int xsign = (int)Mathf.Sign(grow_dir.x);
        //int ysign = (int)Mathf.Sign(grow_dir.y);

        //if (xsign != 0 || ysign != 0)
        //{
        //    // Will this cell grow in the x or y direction ?
        //    bool grow_x = true;

        //    float true_ratio = ysign == 0 ? grow_dir.x + 1 : grow_dir.x / grow_dir.y;
        //    float history_ratio = grow_history.y == 0 ? grow_history.x + 1 : grow_history.x / grow_history.y;

        //    if (Mathf.Abs(true_ratio) > Mathf.Abs(history_ratio))
        //    {
        //        grow_history.y += 1;
        //        grow_x = false;
        //    }
        //    else
        //    {
        //        grow_history.x += 1;
        //        grow_history.y = 0;
        //    }

        //    if (grow_x)
        //    {
        //        if (grid.check(x + xsign, y, new HashSet<int> { 0 }))
        //        {
        //            grid.grid[x + xsign, y] = new TreeCell(grow_dir, grow_history, height - 1, width -= 1);
        //        }
        //    } else
        //    {
        //        if (grid.check(x, y + ysign, new HashSet<int> { 0 }))
        //        {
        //            grid.grid[x, y + ysign] = new TreeCell(grow_dir, grow_history, height - 1, width -= 1);
        //        }
        //    }
        //}
    }
}
