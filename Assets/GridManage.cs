using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GridManage : MonoBehaviour
{

    const int gridx = 198 * 2;
    const int gridy = 108 * 2;
    Cell[,] grid = new Cell[gridx, gridy];
    Color[] col_grid = new Color[gridx * gridy];

    // Start is called before the first frame update
    Texture2D texture;

    struct Cell
    {
        public Color col;
        public int type;
        public Vector2 vel;
        public bool updated;
    }

    void Start()
    {
        print(SystemInfo.supportsComputeShaders);
        Renderer rend = gameObject.GetComponentInChildren<MeshRenderer>();
        //rend.material.EnableKeyword("_MAINTEX");
        init_grid();
        rend.material.SetTexture("_MainTex", texture);

    }
    private void Update()
    {
        mouse_control();
    }

    int frame = 0;

    // Update is called once per frame
    void FixedUpdate()
    {

        Cell sandCell = new Cell();
        sandCell.type = 1;
        sandCell.col = new Color(0.8f, 0.6f, 0.4f, 1);
        sandCell.vel = new Vector2(0, 0);

        grid[gridx / 2, 0] = sandCell;
        move_grid();
    }

    Vector2 mouse_pos_old = new Vector2(0, 0);
    Vector2 mouse_pos_new = new Vector2(0, 0);
    float time = 0;
    void mouse_control()
    {

        Vector2 mousepos = Input.mousePosition / new Vector2(Screen.width, Screen.height) * new Vector2(gridx, gridy);

        mouse_pos_old = mouse_pos_new;
        mouse_pos_new = mousepos;


        if (Input.GetMouseButton(0))
        {
            Vector2 mouse_vel = (mouse_pos_new - mouse_pos_old) / Time.deltaTime / new Vector2(Screen.width, Screen.height) * new Vector2(gridx, -gridy) / 70;
            print("Cell at " + mousepos);
            if (0 <= (int)(mousepos.x) && (int)(mousepos.x) < gridx && 0 <= (int)(mousepos.y) && (int)(mousepos.y) < gridy)
            {

                Cell sandCell = new Cell();
                sandCell.type = 1;
                float brightness = Random.Range(0, 0.3f);
                sandCell.col = new Color(212 / 255f + brightness, 175 / 255f + brightness, 55 / 255f + brightness, 1);


               sandCell.vel = mouse_vel;

                for(int i = 0; i < 10; i++)
                {

                    //forget random circle for now
                    float a = Random.Range(0, 1) * 2 * Mathf.PI;
                    float radius = 3 * Mathf.Sqrt(Random.Range(0, 1));
                    //int x = (int)(radius * Mathf.Cos(a));
                    //int y = (int)(radius * Mathf.Sin(a));
                    int rad = 3;
                    int x = rad - (int)Random.Range(0, rad * 2);
                    int y = rad - (int)Random.Range(0, rad * 2);

                    //gridy - 1 - (int)mousepos.y to invert mouse
                    int circle_posx = x + (int)mousepos.x;
                    int circle_posy = gridy - 1 - (int)mousepos.y + y;

                    if (in_bound(circle_posx, circle_posy) && grid[circle_posx, circle_posy].type != 1)
                    {
                        grid[circle_posx, circle_posy] = sandCell;
                    }
                }
            }
        }

    }


    void init_grid()
    {

        texture = new Texture2D(gridx, gridy);
        texture.filterMode = FilterMode.Point;

        Cell blankCell = new Cell();
        blankCell.type = 0;
        blankCell.col = new Color(0, 0, 0, 1);
        blankCell.vel = new Vector2(0, 0);

        Cell sandCell = new Cell();
        sandCell.type = 1;
        sandCell.col = new Color(0.8f, 0.6f, 0.4f, 1);
        sandCell.vel = new Vector2(0, 0);


        for (int x = 0; x < gridx; x++)
        {
            for (int y = 0; y < gridy; y++)
            {
                grid[x, y] = blankCell;
            }
        }

        //grid[0, 0] = sandCell;
        //grid[gridx / 2, gridy / 2] = sandCell;

        //update_colour();
        //texture.SetPixels(col_grid);
        //texture.Apply();
    }

    //This is inefficient but I don't care
    void update_colour()
    {
        for(int x = 0; x < gridx; x++ )
        {
            for(int y = 0; y < gridy; y++)
            {
                col_grid[y * gridx + x] = grid[x, y].col;
            }
        }
    }

    void move_grid()
    {
        for (int r = 0; r < gridx; r++)
        {
            for(int c = 0; c < gridy; c++)
            {

                grid[r, c].vel += new Vector2(0, 0.4f);
                move_cell(r, c);
            }
        }

        //reset
        for (int x = 0; x < gridx; x++)
        {
            for (int y = 0; y < gridy; y++)
            {
                grid[x, y].updated = false;
            }
        }

        update_colour();
        texture.SetPixels(col_grid);
        texture.Apply();
    }

    void move_cell(int r, int c)
    {
        if (grid[r, c].updated)
        {
            return;
        }

        int type = grid[r, c].type;

        if(type == 1)
        {
            sand_move(r, c);
        }
    }

    void sand_move(int x, int y)
    {
        //May need to replace check with swap
        int xVel = (int)grid[x, y].vel.x;
        int yVel = (int)grid[x, y].vel.y;

        //The direction
        int ysign = (int)Mathf.Sign(yVel);
        int xsign = (int)Mathf.Sign(xVel);

        int newx = x;
        int newy = y;



        if (ysign != 0)
        {
            for (int movey = 0; movey < Mathf.Abs(yVel); movey++)
            {
                if (swap(newx, newy, newx , newy + ysign))
                {
                    newy += ysign;
                }
                else if (swap(newx, newy, newx + 1, newy + ysign))
                {
                    newy += ysign;
                    newx += 1;
                }
                else if (swap(newx, newy, newx - 1, newy + ysign))
                {
                    newy += ysign;
                    newx -= 1;
                }
                else
                {
                    grid[newx, newy].vel.y = 0;
                    break;
                }
            }

        }

        if (xsign != 0)
        {
            for (int movex = 0; movex < Mathf.Abs(xVel); movex += 1)
            {
                if (swap(newx, newy, newx + xsign, newy))
                {
                    newx += xsign;


                                            //friction
                    if(check(newx, newy + 1, new List<int> { 1 }))
                    {

                        grid[newx, newy].vel.x -= 0.05f * xsign;
                        if(Mathf.Abs(grid[newx, newy].vel.x) < 0)
                        {
                            grid[newx, newy].vel.x = 0;
                        }
                    }
                }
                else
                {

                    if(in_bound(newx + xsign, newy))
                    {
                        grid[newx + xsign, newy].vel.x = grid[newx, newy].vel.x * 0.8f;
                        grid[newx, newy].vel.x = 0;
                    }
                    break;
                }
            }
        }

        grid[newx, newy].updated = true;
    }


    bool swap(int x1, int y1, int x2, int y2)
    {
        if (in_bound(x1, y1) && in_bound(x2, y2))
        {

            if(grid[x2, y2].type == 0)
            {
                grid[x1, y1].updated = true;
                Cell temp = grid[x1, y1];
                grid[x1, y1] = grid[x2, y2];
                grid[x2, y2] = temp;
                return true;
            }
        }

        return false;
    }


    bool swap(int x1, int y1, int x2, int y2, List<int> swapable)
    {
        if (in_bound(x1, y1) && in_bound(x2, y2))
        {
            if(swapable.Contains(grid[x2, y2].type))
            {
                Cell temp = grid[x1, y1];
                grid[x1, y1] = grid[x2, y2];
                grid[x2, y2] = temp;
                return true;
            }
        }

        return false;
    }


    bool in_bound(int x, int y)
    {
        return 0 <= x && x < gridx && 0 <= y && y < gridy;
    }


    bool check(int x, int y, List<int> states)
    {
        if (0 <= x && x < gridx && 0 <= y && y < gridy && states.Contains(grid[x, y].type))
        {
            return true;
        }

        return false;
    }



    bool check(int x, int y)
    {
        if (0 <= x && x < gridx && 0 <= y && y < gridy && grid[x, y].type == 0)
        {
            return true;
        }

        return false;
    }
}
