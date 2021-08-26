using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cell
{

    public Color col;
    public int id;
    public Vector2 vel;
    public bool updated;
    public bool destroy_on_contact = false;
    public int weight;
    public byte flammability = 0;
    public bool solid = true;

    //TODO why not make the grid reference a property?

    // Start is called before the first frame update
    public Cell()
    {
        this.id = 0;
        this.col = new Color(0, 0, 0);
    }
    public Cell(Color col, int type)
    {
        this.col = col;
        this.id = type;
        this.vel = new Vector2(0, 0);
    }

    public Cell(Color col, int type, Vector2 vel)
    {
        this.col = col;
        this.id = type;
        this.vel = vel;
    }

    //Move the references space in accordance to the cells movement characteristics
    public abstract void move(short x, short y);

}

