using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeCell : Cell
{
    int flowspeed = 8;
    public SmokeCell(Color col, int weight, Vector2 vel) : base(col, 2, vel) 
    {
        this.col = col;
        this.weight = weight;
        this.id = 7;
    }

    //For flowing, once a cell is flowing in a direction, it shouldn't try to move back to the space it once occupied
    // 0 =uncommited
    // 1 = commited right
    // 2 = commited left
    int commited = 0;

    //Move the references space in accordance to the cells movement characteristics
    public override void move(short x, short y)
    {

        if (this.updated)
        {
            return;
        }


        int xVel = (int)Grid.grid[x, y].vel.x;
        int yVel = (int)Grid.grid[x, y].vel.y;

        //The direction
        int ysign = (int)Mathf.Sign(yVel);
        int xsign = (int)Mathf.Sign(xVel);

        int newx = x;
        int newy = y;

        if (ysign != 0)
        {
            for (int movey = 0; movey < Mathf.Abs(yVel); movey++)
            {
                if (Grid.swap(newx, newy, newx, newy + ysign))
                {
                    newy += ysign;
                }
                else if (Grid.swap(newx, newy, newx + 1, newy + ysign))
                {
                    newy += ysign;
                    newx += 1;
                }
                else if (Grid.swap(newx, newy, newx - 1, newy + ysign))
                {
                    newy += ysign;
                    newx -= 1;
                }
                else
                {
                    //Grid.pass_velocity(newx, newy + ysign, Grid.grid[newx, newy].vel * Vector2.up);
                    //Grid.grid[newx, newy].vel.y = 0;
                    break;
                }
            }

        }

        if (xsign != 0)
        {
            for (int movex = 0; movex < Mathf.Abs(xVel); movex += 1)
            {
                if (Grid.swap(newx, newy, newx + xsign, newy))
                {
                    newx += xsign;


                    //friction
                    //if (grid.check(newx, newy + 1, new HashSet<int> { 1 }))
                    //{

                    //grid[newx, newy].vel.x -= 0.05f * xsign;
                    //if (Mathf.Abs(grid[newx, newy].vel.x) < 0)
                    //{
                    //    grid[newx, newy].vel.x = 0;
                    //}
                    //}
                }
                else
                {

                    Grid.pass_velocity(newx + xsign, newy, Grid.grid[newx, newy].vel * Vector2.right);
                    Grid.grid[newx, newy].vel.x = 0;
                    break;
                }
            }
        }

        Grid.grid[newx, newy].updated = true;
    }
}

