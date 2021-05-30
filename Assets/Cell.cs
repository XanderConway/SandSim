using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{

    Color col;
    public int type;
    public Vector2 vel;
    bool updated;

    // Start is called before the first frame update
    public Cell(Color col, int type)
    {
        this.col = col;
        this.type = type;
    }

}

