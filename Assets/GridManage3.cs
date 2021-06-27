using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManage3 : MonoBehaviour
{
    const int gridx = 198 * 2;
    const int gridy = 108 * 2;

    // Start is called before the first frame update
    Texture2D texture;

    Grid grid;
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


    // Update is called once per frame
    void FixedUpdate()
    {

        move_grid();
    }

    Vector2 mouse_pos_old = new Vector2(0, 0);
    Vector2 mouse_pos_new = new Vector2(0, 0);
    void mouse_control()
    {

        Vector2 mousepos = Input.mousePosition / new Vector2(Screen.width, Screen.height) * new Vector2(gridx, gridy);

        mouse_pos_old = mouse_pos_new;
        mouse_pos_new = mousepos;

        if (Input.GetMouseButton(0))
        {
            Vector2 mouse_vel = (mouse_pos_new - mouse_pos_old) / Time.deltaTime / new Vector2(Screen.width, Screen.height) * new Vector2(gridx, -gridy) / 70;
            if (0 <= (int)(mousepos.x) && (int)(mousepos.x) < gridx && 0 <= (int)(mousepos.y) && (int)(mousepos.y) < gridy)
            {

                for (int i = 0; i < 10; i++)
                {
                    float brightness = Random.Range(0, -0.1f);
                    Color colour = new Color(139 / 255f + brightness, 69 / 255f + brightness, 19 / 255f + brightness, 1);
                    SandCell sandCell = new SandCell(colour, 1, mouse_vel);

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

                    if (grid.in_bound(circle_posx, circle_posy) && grid.grid[circle_posx, circle_posy].type != 1)
                    {
                        grid.grid[circle_posx, circle_posy] = sandCell;
                    }
                }
            }
        }


        if (Input.GetMouseButton(1))
        {
            int mousex = (int)mousepos.x;
            int mousey = gridy - 1 - (int)mousepos.y;

            Vector2 mouse_vel = (mouse_pos_new - mouse_pos_old) / Time.deltaTime / new Vector2(Screen.width, Screen.height) * new Vector2(gridx, -gridy) / 70;
            for (int i = 0; i < 10; i++)
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

                if (grid.in_bound(circle_posx, circle_posy) && grid.grid[circle_posx, circle_posy].type != 2)
                {
                    WaterCell water = new WaterCell(new Color(0.2f, 0.2f, 0.9f), 2, mouse_vel);
                    grid.grid[circle_posx, circle_posy] = water;
                }
            }
        }

    }


    void init_grid()
    {

        texture = new Texture2D(gridx, gridy);
        texture.filterMode = FilterMode.Point;

        grid = new Grid(gridx, gridy);

        //grid[0, 0] = sandCell;
        //grid[gridx / 2, gridy / 2] = sandCell;

        //update_colour();
        //texture.SetPixels(col_grid);
        //texture.Apply();
    }

    bool update_from_left = false;
    void move_grid()
    {

        if (update_from_left)
        {
            for (int r = 0; r < gridx; r++)
            {
                for (int c = 0; c < gridy; c++)
                {

                    grid.grid[r, c].vel += new Vector2(0, 0.4f);
                    grid.grid[r, c].move(ref grid, r, c);
                }
            }
        }
        else
        {
            for (int r = gridx - 1; r >= 0; r--)
            {
                for (int c = gridy - 1; c >= 0; c--)
                {

                    grid.grid[r, c].vel += new Vector2(0, 0.4f);
                    grid.grid[r, c].move(ref grid, r, c);
                }
            }
        }

        update_from_left = !update_from_left;

        //reset
        for (int x = 0; x < gridx; x++)
        {
            for (int y = 0; y < gridy; y++)
            {
                grid.grid[x, y].updated = false;
            }
        }

        grid.update_colour();
        texture.SetPixels(grid.col_grid);
        texture.Apply();
    }
}
