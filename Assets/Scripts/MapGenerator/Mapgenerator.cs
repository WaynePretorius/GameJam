using UnityEngine;
using UnityEngine.Tilemaps;

public class Mapgenerator : MonoBehaviour
{
    //variables
    [SerializeField] private int iterations = 5;
    [SerializeField] private int RandomFillPercent = 100;

    //References
    [SerializeField] Transform parentObject;
    [SerializeField] TileSettings tileSettings;
    [SerializeField] Vector2 coordinate;

    private GameObject background;

    private void Start()
    {
        GenerateChunk();
        GenGroundTiles();
    }

    private void GenerateChunk()
    {
        //Generate the chunk
        TileChunk chunk = new TileChunk(RandomFillPercent, iterations, parentObject, tileSettings);
        chunk.GenerateChunk();
    }


    //For the borders of the map
    private void GenGroundTiles()
    {
        int groundWidth = 450;
        int groundLength = 450;

        int xPos = groundWidth / 2;
        int yPos = groundLength / 2;

        background = new GameObject(tileSettings.grids[4].gridName);
        background.transform.parent = parentObject;
        background.transform.position = new Vector3Int(xPos, yPos, 0);
        background.AddComponent<Tilemap>();
        background.AddComponent<TilemapRenderer>();
        background.GetComponent<TilemapRenderer>().sortingOrder = tileSettings.grids[4].sortOrder;

        int[,] groundMap = new int[groundWidth, groundLength];

        for (int x = 0; x < groundWidth; x++)
        {
            for (int y = 0; y < groundLength; y++)
            {
                background.GetComponent<Tilemap>().SetTile(new Vector3Int(x, y, 0), tileSettings.grids[4].mapTiles[0]);
            }
        }
    }
}
