using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DAE.Gamesystem;

[ExecuteInEditMode]

public class UIbutton : MonoBehaviour
{
    public bool GenerateboardButton; //"run" or "generate" for example
    public bool clearboardButton; //supports multiple buttons

    public GenerationShapes MapShape = GenerationShapes.Hexagon;

    
    [SerializeField]
    public int Rows = 8;

    [SerializeField]
    public int Columns = 8;

    float size = 1f;

    public GameObject Parent;

    public GameObject Hex;

    public enum GenerationShapes
    {
        Hexagon,
        Rectangle,
        Triangle,
        Parrallelogram
    }

    private void Update()
    {
        if (GenerateboardButton)
            GenerateBoard();
        else if (clearboardButton)
            ClearBoard();
        GenerateboardButton = false;
        clearboardButton = false;

    }

    void GenerateBoard()
    {
        InitTiles();
    }

    void ClearBoard()
    {
        foreach (Transform child in Parent.transform)
        {
            GameObject.DestroyImmediate(child.gameObject);
        }
    }

    private void InitTiles()
    {
        if (MapShape == GenerationShapes.Hexagon)
        {
            InitHexShapeBoard();
        }
        if (MapShape == GenerationShapes.Rectangle)
        {
            //InitRectangleShapeBoard();
        }
        if (MapShape == GenerationShapes.Triangle)
        {
            InitTriangleShapeBaord();
        }

        if (MapShape == GenerationShapes.Parrallelogram)
        {
            InitParallShapeBoard();
        }
    }

    //private void InitRectangleShapeBoard()
    //{
    //    Vector3 pos = Vector3.zero;
    //    int mapSize = Mathf.Max(Rows, Columns);

    //    for (int r = top; r <= bottom; r++)
    //    { // pointy top
    //        int r_offset = floor(r / 2.0); // or r>>1
    //        for (int q = left - r_offset; q <= right - r_offset; q++)
    //        {

    //        }
    //    }
    //}


    private void InitHexShapeBoard()
    {
        Vector3 pos = Vector3.zero;
        int mapSize = Mathf.Max(Rows, Columns);

        for (int q = -mapSize; q <= mapSize; q++)
        {
            int r1 = Mathf.Max(-mapSize, -q - mapSize);
            int r2 = Mathf.Min(mapSize, -q + mapSize);
            for (int r = r1; r <= r2; r++)
            {

                var x = size * ((3f / 2f) * q);             
                var y = size * (Mathf.Sqrt(3f) / 2f * q + Mathf.Sqrt(3f) * r);

                var position = new Vector3(x, 0, y);

                GameObject Tile = GameObject.Instantiate(Hex, position, Hex.transform.rotation, Parent.transform);
                Tile.name = $"AC: [q={q},r={r}, s{-q-r}] || WP: [x={x}, y={y}]";
                
            }
        }
    }

    private void InitParallShapeBoard()
    {

        Vector3 pos = Vector3.zero;
        int mapSize = Mathf.Max(Rows, Columns);

        for (int q = 0; q <= Rows; q++)
        {
            for (int r = 0; r <= Columns; r++)
            {
                var x = size * ((3f / 2f) * q);
                var y = size * (Mathf.Sqrt(3f) / 2f * q + Mathf.Sqrt(3f) * r);

                var position = new Vector3(x, 0, y);

                GameObject Tile = GameObject.Instantiate(Hex, position, Hex.transform.rotation, Parent.transform);
                Tile.name = $"AC: [q={q},r={r}, s{-q - r}] || WP: [x={x}, y={y}]";
            }
        }
    }

    private void InitTriangleShapeBaord()
    {
        Vector3 pos = Vector3.zero;
        int mapSize = Mathf.Max(Rows, Columns);

        for (int q = 0; q <= mapSize; q++)
        {
            for (int r = 0; r <= mapSize - q; r++)
            {

                var x = size * ((3f / 2f) * q);
                var y = size * (Mathf.Sqrt(3f) / 2f * q + Mathf.Sqrt(3f) * r);

                var position = new Vector3(x, 0, y);

                GameObject Tile = GameObject.Instantiate(Hex, position, Hex.transform.rotation, Parent.transform);
                Tile.name = $"AC: [q={q},r={r}, s{-q - r}] || WP: [x={x}, y={y}]";
            }
        }
    }
}


