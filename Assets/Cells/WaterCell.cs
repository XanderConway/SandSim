using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCell : Cell
{
    int flowspeed = 8;
    private Color water_color;
    private Color splash_color = new Color(1, 1, 1);
    public WaterCell() : base() { }
    public WaterCell(Color col, int weight) : base(col, 2) 
    {
        this.water_color = col;
        this.weight = weight;
    }

    public WaterCell(Color col, int weight, Vector2 vel) : base(col, 2, vel) 
    {
        this.water_color = col;
        this.weight = weight;
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

        if(!Grid.check_any(x, y - 1))
        {
            this.col = this.splash_color;
        } else
        {
            this.col = this.water_color;
        }

        int xVel = (int)Grid.grid[x, y].vel.x;
        int yVel = (int)Grid.grid[x, y].vel.y;

        //The direction
        int ysign = (int)Mathf.Sign(yVel);
        int xsign = (int)Mathf.Sign(xVel);

        int newx = x;
        int newy = y;

        int flowspeed = 20;
        
        //Did the cell actually get anywhere?

        if (ysign != 0)
        {

            //fall while you still can fall or potentially flow to fall later
            for (int movey = 0; movey < yVel && flowspeed > 0; movey++)
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

                    if(commited == 0)
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
                        } else
                        {
                            Grid.grid[newx, newy].vel.y = 0;
                            flowspeed = 0;
                            break;
                        }
                    } 
                    else if(commited == 1)
                    {
                        if (Grid.swap(newx, newy, newx - 1, newy))
                        {
                            newx -= 1;
                        } 
                        else
                        {
                            commited = 0;
                            Grid.grid[newx, newy].vel.y = 0;
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
                            Grid.grid[newx, newy].vel.y = 0;
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

                    //grid.pass_velocity(newx + xsign, newy, grid.grid[newx, newy].vel * Vector2.right);
                    Grid.grid[newx, newy].vel.x = 0;
                    break;
                }
            }
        }

        Grid.grid[newx, newy].updated = true;
    }
}

