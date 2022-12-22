using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileChunk 
{
    //Variables
    private int[,] map;
    private int randomFillPercent;
    private int iterations;
    private int mapWidth;
    private int mapHeight;
    private int minRange;
    private int maxRange;
    private int seedNumber;
    private string seedName;

    //References
    private TileSettings tileSettings;
    
    private GameObject tileMesh;
    private GameObject groundLayerOBJ;
    private GameObject grassLayerOBJ;
    private GameObject stoneLayerOBJ;
    private GameObject bushLayer;

    public TileChunk(int randomFillPercent, int iterations, Transform parentObject, TileSettings tileSettings)
    {
        //sets the variables of the chunk
        this.randomFillPercent = randomFillPercent;
        this.iterations = iterations;
        this.tileSettings = tileSettings;

        mapWidth = tileSettings.mapWidth;
        mapHeight = tileSettings.mapHeight;

        Vector2 currentPos = new Vector2(mapWidth, mapHeight);

        //create the gameobject that will have the grid
        tileMesh = new GameObject(Tags.MAP_TERRAIN_NAME);
        tileMesh.AddComponent<Grid>();
        tileMesh.transform.position = new Vector2(currentPos.x, currentPos.y);
        tileMesh.transform.parent = parentObject.transform;
    }

    public void GenerateChunk()
    {
        //gets the seednumber for the map
        seedNumber = PlayerPrefs.GetInt(Tags.PPREFS_RANDOM_MAP_NUMBER);

        //sets the map as new. When the function is called, the map is spawned
        map = new int[mapWidth, mapHeight];

        MoveGrid();

        RandomFillChunk();

        for(int i = 0; i < iterations; i++)
        {
            Iterations();
        }

        DrawChunk();
    }

    //instantiates and creates new gameobject containting tilemeshes and renderers, applying the to a parent as to not overpopulate the inspector and sets the position of the gameobject
    private void MoveGrid()
    {
        List<GameObject> gridNames = AddGameObjects();

        //The for loop makes up for repetitive code.  Should more objects be added, add them to the list through AddGameObjects and set the objects AddValueToGameObjects.
        for (int i = 0; i < gridNames.Count; i++)
        {
            gridNames[i] = new GameObject(tileSettings.grids[i].gridName);
            gridNames[i].transform.parent = tileMesh.transform;
            gridNames[i].transform.position = tileMesh.transform.position;
            gridNames[i].AddComponent<Tilemap>();
            gridNames[i].AddComponent<TilemapRenderer>();
            gridNames[i].GetComponent<TilemapRenderer>().sortingOrder = tileSettings.grids[i].sortOrder;
        }

        AddValueToGameObjects(gridNames);
    }

    //Adds all the gameobjects to a list
    private List<GameObject> AddGameObjects()
    {
        List<GameObject> addToList = new List<GameObject>();
        addToList.Add(groundLayerOBJ);
        addToList.Add(grassLayerOBJ);
        addToList.Add(stoneLayerOBJ);
        addToList.Add(bushLayer);

        return addToList;
    }

    //sets the game objects to the added features of the objects in the list
    private void AddValueToGameObjects(List<GameObject> list)
    {
        groundLayerOBJ = list[0];
        grassLayerOBJ = list[1];
        stoneLayerOBJ = list[2];
        bushLayer = list[3];
    }

    //randomly fills the map. The borders are set as 1, then the next tiles will start generating randomly, giving it a different sets of heights
    private void RandomFillChunk()
    {
        //set the number to string value
        seedName = seedNumber.ToString();

        //get the hashcode to randomly generate according to the name
        System.Random mapName = new System.Random(seedName.GetHashCode());

        //iterate through the map. Except for the borders which will be one, randomly make heights(1) and lows(2)
        for(int x = 0; x < mapWidth; x++)
        {
            for(int y = 0; y < mapHeight; y++)
            {
                if (x == 0 || x == mapWidth - 1 || y == 0 || y == mapHeight - 1)
                {
                    map[x, y] = 1;
                }
                else
                {
                    map[x, y] = (mapName.Next(0, 100) < randomFillPercent) ? 1 : 0;
                }
            }
        }
    }

    //looks through the map, and changes the map so it takes shape depending on how many blocks are around it
    private void Iterations()
    {
        //iterate through the map, and start putting 1 next to 1, and 0 next to 0, depending on how many heights are around the point
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                int neighbourTileSet = GetSurroundingBlock(x, y);

                if (neighbourTileSet > 4)
                {
                    map[x, y] = 1;
                }
                else if (neighbourTileSet < 4)
                {
                    map[x, y] = 0;
                }
            }
        }
    }

    //looks at the surronding blocks and sets if it is part of the border. Looking at the surrounding tiles, the heights that was generated, returns how many borders the tile has
    private int GetSurroundingBlock(int gridX, int gridY)
    {
        int borderCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < mapWidth && neighbourY >= 0 && neighbourY < mapHeight)
                {
                    if (neighbourX != gridX || neighbourY != gridY)
                    {

                        borderCount += map[neighbourX, neighbourY];
                    }
                }
                else
                {
                    borderCount++;
                }
            }
        }
        return borderCount;
    }

    //with the map set to 1 or 0 on after the map has been filled, set specified tiles on layers for the different grids.  Counts and maxcounts is to skip tiles for the fill of the map
    private void DrawChunk()
    {
        if (map != null)
        {
            ClearGrids();

            int smallObstacleCount = 0;
            int smallOBJMaxCount = MaxCount();

            int largeObstacleCount = 0;
            int largeOBJMaxCount = MaxCount();

            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    groundLayerOBJ.GetComponent<Tilemap>().SetTile(new Vector3Int(x, y, 0), tileSettings.grids[0].mapTiles[0]);
                    GenerateFloorTiles(x, y);
                    GenerateSmallObstacles(ref smallObstacleCount, ref smallOBJMaxCount, x, y);
                    GenerateLargeObstacles(ref largeObstacleCount, ref largeOBJMaxCount, x, y);
                }
            }
        }
    }

    //clears the grids of the maps
    private void ClearGrids()
    {
        groundLayerOBJ.GetComponent<Tilemap>().ClearAllTiles();
        grassLayerOBJ.GetComponent<Tilemap>().ClearAllTiles();
        stoneLayerOBJ.GetComponent<Tilemap>().ClearAllTiles();
        bushLayer.GetComponent<Tilemap>().ClearAllTiles();
    }

    //generates the tiles that has to be placed depending on the map
    private void GenerateFloorTiles(int x, int y)
    {
        Tile floorTiles = ReturnTile(tileSettings.grids[1].mapTiles);

        Tile thisTile = (map[x, y] == 0) ? floorTiles : null;
        grassLayerOBJ.GetComponent<Tilemap>().SetTile(new Vector3Int(x, y, 0), thisTile);

    }

    //changes the count of for the next trees/stones randomly
    private int MaxCount()
    {
        return Random.Range(minRange, maxRange);
    }

    //adds the obstacles to the map(rocks etc)
    private void GenerateSmallObstacles(ref int count, ref int maxCount, int width, int height)
    {
        //if the mapheight is high
        if (map[width, height] == 1)
        {
            //if the amounts of heights are more than maxcount
            if (count > maxCount)
            {
                //add the tile
                Tile stoneTile = ReturnTile(tileSettings.grids[2].mapTiles);
                stoneLayerOBJ.GetComponent<Tilemap>().SetTile(new Vector3Int(width, height, 0), stoneTile);
                count = 0;
                maxCount = MaxCount();
            }
            else
            {
                count++;
            }
        }
    }

    //adds the larger obstacles to the map(trees, cars etc)
    private void GenerateLargeObstacles(ref int count, ref int maxCount, int width, int height)
    {
        if (map[width, height] == 0)
        {
            if (count > maxCount)
            {
                Tile bushTile = ReturnTile(tileSettings.grids[3].mapTiles);
                bushLayer.GetComponent<Tilemap>().SetTile(new Vector3Int(width, height, 0), bushTile);
                count = 0;
                maxCount = MaxCount();
            }
            else
            {
                count++;
            }
        }
    }

    //returns a random tile that is placed
    private Tile ReturnTile(List<Tile> tileToReturn)
    {
        int currentTile = Random.Range(0, tileToReturn.Count);
        return tileToReturn[currentTile];
    }
}
