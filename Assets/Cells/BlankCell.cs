using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlankCell : Cell
{
    public BlankCell()
    {
        solid = false;
        //this.flammability = 5;
    }
    public override void move(short x, short y)
    {

    }
}
