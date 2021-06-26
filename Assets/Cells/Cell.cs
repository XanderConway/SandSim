using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cell
{

    public Color col;
    public int type;
    public Vector2 vel;
    public bool updated;
    public bool destroy_on_contact = false;

    // Start is called before the first frame update
    public Cell()
    {
        this.type = 0;
        this.col = new Color(0, 0, 0);
    }
    public Cell(Color col, int type)
    {
        this.col = col;
        this.type = type;
        this.vel = new Vector2(0, 0);
    }

    public Cell(Color col, int type, Vector2 vel)
    {
        this.col = col;
        this.type = type;
        this.vel = vel;
    }

    //Move the references space in accordance to the cells movement characteristics
    public abstract void move(ref Grid grid, int x, int y);

}
