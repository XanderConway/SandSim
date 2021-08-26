using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCell : Cell
{
    float spread_chance = 0.5f;
    Color true_color;
    private int glow_timer;

    //How long the particle will live
    int life_time;
    private int life_timer;
    bool smoke;


    //private static float[] spread_chances = { 1, 0.4f, 0.5f, 0.2f, 0.8f, 0.6f, 0.2f, 0.1f, 0.7f, 0.6f, 0, 0.2f, 1 };
    private static int spread_index = 0;

    public FireCell(Color col, float spread_chance, int life_time, bool smoke = true)
    {

        this.true_color = col;
        this.col = true_color;
        this.spread_chance = spread_chance;
        this.life_time = life_time;
        this.id = 5;
        this.weight = 1000;
        this.smoke = smoke;
    }

    //For flowing, once a cell is flowing in a direction, it shouldn't try to move back to the space it once occupied
    // 0 =uncommited
    // 1 = commited right
    // 2 = commited left

    //Move the references space in accordance to the cells movement characteristics
    public override void move(short x, short y)
    {
        if (this.updated)
        {
            return;
        }

        glow();

        if (time_to_die())
        {
            //if(Grid.check(x, y - 1, 0))
            //{
            //    this.life_time += 2;
            //    Grid.swap(x, y, x, y - 1);
            //}
            if(this.smoke)
            {
                Grid.grid[x, y] = new SmokeCell(new Color(0.6f, 0.6f, 0.6f), 1, new Vector2(0, 0));
            }
            return;
        }

        //set_on_fire(x - 1, y);
        //set_on_fire(x + 1, y);

        if(this.smoke)
        {
            set_on_fire(x, y - 1);
            set_on_fire(x, y + 1);

            set_on_fire(x - 1, y - 1);
            //set_on_fire(x - 1, y + 1);

            set_on_fire(x + 1, y - 1);

            //flicker(x + 1, y);
            //flicker(x - 1, y);
            //flicker(x, y - 1);
        }
        //set_on_fire(x + 1, y + 1);

    }

    private void glow()
    {
        glow_timer += 1;

        while(glow_timer > 10)
        {
            glow_timer = -10;
        }

        float brightness = (Mathf.Abs(glow_timer) - 5) * 0.05f ;

        this.col.r =  this.true_color.r +  brightness;
        this.col.g = this.true_color.g + brightness;
        this.col.b = this.true_color.b +  brightness;
    }

    private bool time_to_die()
    {
        this.life_timer += 1;

        if(this.life_timer > this.life_time)
        {
            return true;
        }

        return false;
    }

    //Used to compute how much the fire should spread
    private float burn_function()
    {
        FireCell.spread_index += 1;
        return (Mathf.Sin(FireCell.spread_index) + 1) * 5f;
    }


    //Doesn't work
    //private void flicker(int x, int y)
    //{
    //    if(Grid.in_bound(x,y) && Grid.grid[x, y].id == 0)
    //    {
    //        Grid.grid[x, y] = new FireCell(this.true_color, 3, 1, false);
    //    }
    //}

    private bool set_on_fire(int x, int y)
    {
        //if (FireCell.spread_index >= FireCell.spread_chances.Length)
        //{
        //    spread_index = 0;
        //}

        if (this.spread_chance > burn_function())
        {
            if (Grid.in_bound(x, y) && Grid.grid[x, y].flammability > 0)
            {

                float brightness = Random.Range(0, 0.2f);
                FireCell fire_cell = new FireCell(this.true_color, 3, this.life_time);
                fire_cell.updated = true;
                fire_cell.glow_timer += this.glow_timer + 7;

                Grid.grid[x, y] = fire_cell;
                return true;
            }
        }
 
        return false;
    }

}
