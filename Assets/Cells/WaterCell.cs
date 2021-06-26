using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCell : Cell
{
    int flowspeed = 8;
    public WaterCell() : base() { }
    public WaterCell(Color col, int type) : base(col, type) { }

    public WaterCell(Color col, int type, Vector2 vel) : base(col, type, vel) { }

    //For flowing, once a cell is flowing in a direction, it shouldn't try to move back to the space it once occupied
    // 0 =uncommited
    // 1 = commited right
    // 2 = commited left
    int commited = 0;

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

        int flowspeed = 20;

        if (ysign != 0)
        {
            for (int movey = 0; movey < yVel ; movey++)
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
                    if(commited == 0)
                    {
                        if (grid.swap(newx, newy, newx - 1, newy))
                        {
                            newx -= 1;
                            commited = 1;
                        } 
                        else if (grid.swap(newx, newy, newx + 1, newy))
                        {
                            newx += 1;
                            commited = 2;
                        } else
                        {
                            grid.grid[newx, newy].vel.y = 0;
                            break;
                        }
                    } 
                    else if(commited == 1)
                    {
                        if (grid.swap(newx, newy, newx - 1, newy))
                        {
                            newx -= 1;
                        } 
                        else
                        {
                            commited = 0;
                            grid.grid[newx, newy].vel.y = 0;
                            break;
                        }
                    }
                    else if (commited == 2)
                    {
                        if (grid.swap(newx, newy, newx + 1, newy))
                        {
                            newx += 1;
                        }
                        else
                        {
                            commited = 0;
                            grid.grid[newx, newy].vel.y = 0;
                            break;
                        }
                    }

                    flowspeed -= 1;
                }
                //else if(grid.swap(newx, newy, newx - 1, newy))
                //{
                //    newx -= 1;
                //}
                //else if (grid.swap(newx, newy, newx + 1, newy))
                //{
                //    newx += 1;
                //}
                //else
                //{
                //    grid.grid[newx, newy].vel.y = 0;
                //    break;
                //}
            }

        }

        //if (xsign != 0)
        //{
        //    for (int movex = 0; movex < Mathf.Abs(xVel); movex += 1)
        //    {
        //        if (grid.swap(newx, newy, newx + xsign, newy))
        //        {
        //            newx += xsign;


        //            //friction
        //            if (grid.check(newx, newy + 1, new List<int> { 1 }))
        //            {

        //                //grid[newx, newy].vel.x -= 0.05f * xsign;
        //                //if (Mathf.Abs(grid[newx, newy].vel.x) < 0)
        //                //{
        //                //    grid[newx, newy].vel.x = 0;
        //                //}
        //            }
        //        }
        //        else
        //        {

        //            //grid.pass_velocity(newx + xsign, newy, grid.grid[newx, newy].vel * Vector2.right);
        //            grid.grid[newx, newy].vel.x = 0;
        //            break;
        //        }
        //    }
        //}

        grid.grid[newx, newy].updated = true;
    }

}

