using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCell : Cell
{
    int flowspeed = 8;
    public WaterCell() : base() { }
    public WaterCell(Color col, int type) : base(col, type) { }

    public WaterCell(Color col, int type, Vector2 vel) : base(col, type, vel) { }

    //Move the references space in accordance to the cells movement characteristics
    public override void move(ref Grid grid, int x, int y)
    {

        if (this.updated)
        {
            return;
        }

        int xVel = (int)grid.grid[x, y].vel.x;
        int yVel = (int)grid.grid[x, y].vel.y;

        //The direction
        int ysign = (int)Mathf.Sign(yVel);
        int xsign = (int)Mathf.Sign(xVel);

        int newx = x;
        int newy = y;

        bool dir_chosen = false;
        bool go_right = false;

        for (int i = 0; i < flowspeed; i++)
        {

            if (grid.swap(newx, newy, newx, newy + ysign))
            {
                newy += ysign;
                yVel -= 1;
            }
            else if (grid.swap(newx, newy, newx + 1, newy + ysign))
            {
                newy += ysign;
                newx += 1;

                go_right = false;

                yVel -= 1;
                xVel -= 1;

            }
            else if (grid.swap(newx, newy, newx - 1, newy + ysign))
            {
                newy += ysign;
                newx -= 1;

                go_right = true;

                yVel -= 1;
                xVel += 1;
            }
            else if (grid.swap(newx, newy, newx - 1, newy))
            {
                newx -= 1;
                xVel += 1;
            }
            else if (grid.swap(newx, newy, newx + 1, newy))
            {
                newx += 1;
                xVel -= 1;
            }
        }

        grid.grid[newx, newy].updated = true;
    }

}

