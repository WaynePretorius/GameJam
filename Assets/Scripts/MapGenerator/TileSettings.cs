using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// All settings in the tilesettings is used accorfing to the information needed for the tiles of the map. Can be used with different tiles
/// to randomize the scenes
/// </summary>

[CreateAssetMenu]
public class TileSettings : ScriptableObject
{
    //references
    public MapGrid[] grids;
    
    //variables for the mapsettings
    public int iterations = 5;
    public int mapWidth = 40;
    public int mapHeight = 40;
    public int minChanceForSpawning = 15;
    public int MaxChanceForSpawning = 35;

    //the grid where the map will be tiled
    [System.Serializable]
    public struct MapGrid
    {
        public string gridName;
        public int sortOrder;

        public Tilemap tilemap;
        public List<Tile> mapTiles;

        //sets the order in layer according to the layer
        public void SortOrder()
        {
            tilemap.GetComponent<TilemapRenderer>().sortingLayerID = sortOrder;
        }
    }
}

