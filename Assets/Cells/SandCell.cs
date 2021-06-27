using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandCell : Cell
{
    Color dirt;
    Color grass = new Color(0.2f, 0.8f, 0.4f);
    public SandCell() : base() { }
    public SandCell(Color col, int type) : base(col, type) { }

    public SandCell(Color col, int type, Vector2 vel) : base(col, type, vel) 
    {
        dirt = col;
    }

    //Move the references space in accordance to the cells movement characteristics
    public override void move(ref Grid grid, int x, int y)
    {

        if(this.updated)
        {
            return;
        }

        if(!grid.check_any(x, y - 1) || !grid.check_any(x, y - 2))
        {
            this.col = grass;
        } else
        {
            this.col = dirt;
        }
         
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
                else if (grid.swap(newx, newy, newx + 1, newy + ysign))
                {
                    newy += ysign;
                    newx += 1;
                }
                else if (grid.swap(newx, newy, newx - 1, newy + ysign))
                {
                    newy += ysign;
                    newx -= 1;
                }
                else
                {
                    grid.pass_velocity(newx, newy + ysign, grid.grid[newx, newy].vel * Vector2.up);
                    grid.grid[newx, newy].vel.y = 0;
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

                    grid.pass_velocity(newx + xsign, newy, grid.grid[newx, newy].vel * Vector2.right);
                    grid.grid[newx, newy].vel.x = 0;
                    break;
                }
            }
        }

        grid.grid[newx, newy].updated = true;
    }

}

