using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> AppleLocations = new List<GameObject>();

    [SerializeField]private int width;
    [SerializeField]private int height;

    public void SetSpawnPoints(int mapWidth, int mapHeight)
    {
        //divide the height and width to get accurate spawnpoints for the apples
        width = mapWidth / 2;
        height = mapHeight / 2;
    }

    private void Start()
    {
        SpawnAppleLocations();
    }

    private void SpawnAppleLocations()
    {
        foreach(var spawnPoint in AppleLocations)
        {
            //make random positions for the apples to spawn
            int xPos = Random.Range(-(width - 1), width);
            int yPos = Random.Range(-(height - 1), height);

            //set their spawnpoints to the random numbers
            spawnPoint.transform.position = new Vector3Int(xPos, yPos);
        }
    }
}
