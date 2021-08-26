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

        this.vel.y -= 0.8f;

        int xVel = (int)Grid.grid[x, y].vel.x;
        int yVel = (int)Grid.grid[x, y].vel.y;

        //The direction
        int ysign = (int)Mathf.Sign(yVel);
        int xsign = (int)Mathf.Sign(xVel);

        int newx = x;
        int newy = y;

        int flowspeed = 3;

        //Did the cell actually get anywhere?

        if (ysign != 0)
        {

            //fall while you still can fall or potentially flow to fall later
            for (int movey = 0; movey < Mathf.Abs(yVel) && flowspeed > 0; movey++)
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

                    //flowing shouldn't count as a fall
                    movey -= 1;

                    if (commited == 0)
                    {
                        if (Grid.swap(newx, newy, newx - 1, newy))
                        {
                            newx -= 1;
                            commited = 1;
                        }
                        else if (Grid.swap(newx, newy, newx + 1, newy))
                        {
                            newx += 1;
                            commited = 2;
                        }
                        else
                        {
                            //Grid.grid[newx, newy].vel.y = 0;
                            flowspeed = 0;
                            break;
                        }
                    }
                    else if (commited == 1)
                    {
                        if (Grid.swap(newx, newy, newx - 1, newy))
                        {
                            newx -= 1;
                        }
                        else
                        {
                            commited = 0;
                            //Grid.grid[newx, newy].vel.y = 0;
                            flowspeed = 0;
                            break;
                        }
                    }
                    else if (commited == 2)
                    {
                        if (Grid.swap(newx, newy, newx + 1, newy))
                        {
                            newx += 1;
                        }
                        else
                        {
                            commited = 0;
                            //Grid.grid[newx, newy].vel.y = 0;
                            flowspeed = 0;
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

        Grid.grid[newx, newy].updated = true;
    }
}

