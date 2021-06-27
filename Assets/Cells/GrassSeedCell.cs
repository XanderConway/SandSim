using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSeedCell : Cell
{
    bool planted = false;
    Color grass_color = new Color(0.2f, 1f, 0.2f);
    int height;
    float time = 0;
    public GrassSeedCell(Color col, int type, Vector2 vel) : base(col, type, vel)
    {
        this.destroy_on_contact = true;
        this.height = Random.Range(3, 20);
    }

    public GrassSeedCell(Color col, int type, Vector2 vel, int height) : base(col, type, vel)
    {
        this.destroy_on_contact = true;
        this.height = height;
        this.planted = true;
    }

    public override void move(ref Grid grid, int x, int y)
    {

        if (this.updated)
        {
            return;
        }

        if (!planted)
        {
            int xVel = (int)grid.grid[x, y].vel.x;
            int yVel = (int)grid.grid[x, y].vel.y;

            //The direction
            int ysign = (int)Mathf.Sign(yVel);
            int xsign = (int)Mathf.Sign(xVel);

            int newx = x;
            int newy = y;


            if (ysign != 0)
            {
                for (int movey = 0; movey < Mathf.Abs(yVel); movey++)
                {
                    if (grid.swap(newx, newy, newx, newy + ysign))
                    {
                        newy += ysign;
                    }
                    else
                    {
                        grid.grid[newx, newy].vel.y = 0;

                        if (grid.check(x, y + 1, new HashSet<int> { 1 }))
                        {
                            this.col = grass_color;
                            planted = true;
                        }

                        break;
                    }
                }
            }

            if (xsign != 0)
            {
                for (int movex = 0; movex < Mathf.Abs(xVel); movex += 1)
                {
                    if (grid.swap(newx, newy, newx + xsign, newy))
                    {
                        newx += xsign;
                    }
                    else
                    {
                        grid.grid[newx, newy].vel.x = 0;
                        break;
                    }
                }
            }
        }
        else
        {
            if (this.height > 0 && grid.check(x, y - 1, new HashSet<int> { 0 }))
            {
                time += Time.deltaTime;
                if (time > 0.3f)
                {
                    grid.grid[x, y - 1] = new GrassSeedCell(grass_color, this.type, new Vector2(0, 0), this.height - 1);
                    time = 0;
                }
            }
        }
    }
}
