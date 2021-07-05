using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCell : Cell
{
    Color bark = new Color(0.4f, 0.2f, 0.1f);
    Vector2 grow_dir;
    bool planted = false;

    float grow_timer = 0;

    //How many cells have been moved previously (used to align with velocity)
    Vector2 grow_history;

    //trunk thickness x10
    int width = 30;

    //branch length
    int height = 50;

    public TreeCell(Vector2 vel, int height, int width, Vector2 grow_dir, Vector2 grow_history, bool planted)
    {
        this.height = height;
        this.col = bark;
        this.vel = vel;
        this.planted = planted;
        this.id = 3;
        this.weight = 3;
        this.grow_history = grow_history;
        this.grow_dir = grow_dir;
        this.width = width;
        this.flammability = 20;
    }

    public override void move(short x, short y)
    {

        if (Grid.grid[x,y].updated)
        {
            return;
        }


        if (!this.planted)
        {

            if (Grid.check(x, y + 1, 1))
            {
                this.planted = true;
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
                    }
                    else
                    {
                        break;
                    }
                }
            }

            Grid.grid[newx, newy].updated = true;
        } else
        {

            //Branching
            //if(this.height > 30 && this.width > 1)
            //{

            //}

            //Upward Growth
            if (this.height > 1)
            {


                int next_x = x;
                int next_y = y;

                if (grow_dir.x == 0)
                {
                    next_x = x;
                    next_y = y - (int)Mathf.Sign(grow_dir.y);
                }
                else if (grow_dir.y == 0)
                {
                    next_y = y;
                    next_x = x - (int)Mathf.Sign(grow_dir.x);
                }
                else
                {

                    float slope = Mathf.Abs(grow_dir.y / grow_dir.x);
                    float history_ratio = Mathf.Abs(grow_history.y / grow_history.x);

                    if (history_ratio < slope)
                    {
                        next_x = x;
                        next_y = y - (int)Mathf.Sign(grow_dir.y);
                    }
                    else
                    {
                        next_y = y;
                        next_x = x - (int)Mathf.Sign(grow_dir.x);
                    }
                }

                if (Grid.in_bound(next_x, next_y) && (!Grid.check_any(next_x, next_y) || Grid.check(next_x, next_y, 3)))
                {
                    this.grow_history.x += Mathf.Abs(next_x - x);
                    this.grow_history.y += Mathf.Abs(next_y - y);

                    Grid.grid[next_x, next_y] = new TreeCell(new Vector2(0, 0), this.height - 1, this.width, this.grow_dir, this.grow_history, true);
                    this.height = 0;
                }

            }

            //Outwards expansion
            if (this.width > 0)
            {
                if (Grid.in_bound(x + 1, y) && !Grid.check_any(x + 1, y))
                {
                    Grid.grid[x + 1, y] = new TreeCell(new Vector2(0, 0), 0, this.width - 1, new Vector2(0, 0), new Vector2(1, 1), true);
                }

                if (Grid.in_bound(x - 1, y) && !Grid.check_any(x - 1, y))
                {
                    Grid.grid[x - 1, y] = new TreeCell(new Vector2(0, 0), 0, this.width - 1, new Vector2(0, 0), new Vector2(1, 1), true);
                }
            }

        }
    }
}
