using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{

    public Color col;
    public int type;
    public Vector2 vel;
    public bool updated;

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
    public void move(ref Grid grid, int x, int y)
    {

    }

}

