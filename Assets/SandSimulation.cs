using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SandSimulation : MonoBehaviour
{
    // Start is called before the first frame update

    public ComputeShader shader;
    public RawImage image;
    ComputeBuffer buffer;
    Texture2D texture;
    public Camera cam;

    public Canvas canvas;

    const int gridx = 198;
    const int gridy = 108;

    Cell[,] grid = new Cell[gridx, gridy];
    Cell[,] grid_update = new Cell[gridx, gridy];
    struct Cell
    {
        public short type;
        public Vector2 vel;
        public Color colour;
        public bool updated;
    }

    void Start()
    {
        cam = Camera.main;
        texture = new Texture2D(gridx, gridy);
        texture.filterMode = FilterMode.Point;

        set_sand();

        //for (int r = 0; r < rows; r++)
        //{
        //    for (int c = 0; c < cols; c++)
        //    {
        //        texture.SetPixel(r, c, new Color(0, 0, 0));
        //    }
        //}

        //texture.SetPixel(0, 50, new Color(1, 0, 0));
        //texture.Apply();

        image.texture = texture;



    }

    void set_sand()
    {
        Cell sandCell = new Cell();
        sandCell.type = 1;
        sandCell.vel = new Vector2(0, 0);
        sandCell.colour = new Color(0.8f, 0.7f, 0.6f);


        Cell blank = new Cell();
        blank.colour = new Color(0, 0, 0);
        blank.type = 0;
        blank.vel = new Vector2(0, 0);

        for (int r = 0; r < gridx; r++)
        {
            for (int c = 0; c < gridy; c++)
            {
                grid[r, c] = blank;
            }
        }
    }

    void swap(int r1, int c1, int r2, int c2)
    {

        if(grid[r1, c1].updated == false && grid[r2, c2].updated == false)
        {
            grid[r1, c1].updated = true;

            if(grid[r2, c2].type != 0)
            {
                grid[r2, c2].updated = true;
            }
            Cell temp = grid[r2, c2];
            grid[r2, c2] = grid[r1, c1];
            grid[r1, c1] = temp;
        }

        //Color temp_colour = colour_grid[r1, c1];
        //colour_grid[r1, c1] = colour_grid[r2, c2];
        //colour_grid[r2, c2] = temp_colour;

        //texture.SetPixel(r1, c1, );
        //texture.SetPixel(r2, c2, colour_grid[r2, c2]);

    }


    void update_grid()
    {

        for (int r = gridx - 1; r >= 0; r--)
        {
            for (int c = gridy - 1; c >= 0; c--)
            {
                texture.SetPixel(r, c, grid[r, c].colour);
                grid[r, c].updated = false;
            }

        }



        //for (int x = gridx - 1; x >= 0; x--)
        //{
        //    for (int y = gridy - 1; y >= 0; y--)
        //    {
        //        if (y - 1 >= 0 && grid[x, y - 1].type == 0)
        //        {
        //            swap(x, y, x, y - 1);
        //        } else if(y - 1 >= 0 && x - 1>= 0 && grid[x - 1, y - 1].type == 0)
        //        {
        //            swap(x, y, x - 1, y - 1);
        //        }
        //        else if (y - 1 >= 0 && x + 1 < gridx && grid[x + 1, y - 1].type == 0)
        //        {
        //            swap(x, y, x + 1, y - 1);
        //        }
        //    }
        //}


        for (int x = 0; x < gridx; x++)
        {
            for (int y = 0; y < gridy; y++)
            {

                if (grid[x, y].type == 1)
                {
                    if (y - 1 >= 0 && (grid[x, y - 1].type == 0 || grid[x, y - 1].type == 2))
                    {
                        swap(x, y, x, y - 1);
                    }
                    else if (y - 1 >= 0 && x - 1 >= 0 && (grid[x - 1, y - 1].type == 0 || grid[x - 1, y - 1].type == 2))
                    {
                        swap(x, y, x - 1, y - 1);
                    }
                    else if (y - 1 >= 0 && x + 1 < gridx && (grid[x + 1, y - 1].type == 0 || grid[x + 1, y - 1].type == 2))
                    {
                        swap(x, y, x + 1, y - 1);
                    }
                } 
                else if(grid[x,y].type == 2)
                {
                    if (y - 1 >= 0 && grid[x, y - 1].type == 0)
                    {
                        swap(x, y, x, y - 1);
                    }
                    else if (y - 1 >= 0 && x - 1 >= 0 && grid[x - 1, y - 1].type == 0)
                    {
                        swap(x, y, x - 1, y - 1);
                    }
                    else if (y - 1 >= 0 && x + 1 < gridx && grid[x + 1, y - 1].type == 0)
                    {
                        swap(x, y, x + 1, y - 1);
                    }
                    else if(x - 1 >= 0 && grid[x - 1, y].type == 0)
                    {
                        swap(x, y, x - 1, y);
                    }
                    else if (x + 1 < gridx && grid[x + 1, y].type == 0)
                    {
                        swap(x, y, x + 1, y);
                    }
                }
            }
        }
        //for (int r =  - 1; r >= 0; r--)
        //{
        //    for(int c = cols - 1; c >= 0; c--)
        //    {

        //        //Empty Buttom
        //        if(r - 1 >= 0  && grid[r -1, c].type == 0)
        //        {
        //            swap(r, c, r - 1, c);
        //        }
        //        else if (r - 1 >= 0 && c + 1 < cols && grid[r - 1, c + 1].type == 0)
        //        {
        //            swap(r, c, r - 1, c + 1);
        //        }
        //        else if (r - 1 >= 0 && c - 1 > 0 && grid[r - 1, c - 1].type == 0)
        //        {
        //            swap(r, c, r - 1, c - 1);
        //        }

        //    }

        //}

        texture.Apply();
        //image.material.mainTexture = texture;

    }

    // Update is called once per frame
    void Update()
    {
        update_grid();

        texture.SetPixel(0, 0, new Color(1, 0, 0));
        texture.Apply();

        Cell sandCell = new Cell();
        sandCell.type = 1;
        sandCell.vel = new Vector2(0, 0);
        sandCell.colour = new Color(0.8f, 0.7f, 0.6f);


        Cell waterCell = new Cell();
        waterCell.type = 2;
        waterCell.vel = new Vector2(0, 0);
        waterCell.colour = new Color(0.2f, 0.6f, 1f);


        if (Input.GetMouseButton(0))
        {

            Vector2 mousepos = Input.mousePosition / new Vector2(Screen.width, Screen.height) * new Vector2(gridx, gridy);

            if (0 <= (int)(mousepos.x) && (int)(mousepos.x) < gridx && 0 <= (int)(mousepos.y) && (int)(mousepos.y) < gridy)
            {
                grid[(int)mousepos.x, (int)mousepos.y] = sandCell;
            }
        }



        if (Input.GetMouseButton(1))
        {

            Vector2 mousepos = Input.mousePosition / new Vector2(Screen.width, Screen.height) * new Vector2(gridx, gridy);

            if (0 <= (int)(mousepos.x) && (int)(mousepos.x) < gridx && 0 <= (int)(mousepos.y) && (int)(mousepos.y) < gridy)
            {
                grid[(int)mousepos.x, (int)mousepos.y] = waterCell;
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            set_sand();
        }
    }
}
