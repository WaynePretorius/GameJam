using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> AppleLocations = new List<GameObject>();
    [SerializeField] Apples applePrefab;

    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private int applePrice = 1;

    [SerializeField] private float appleSpawnTime = 2.5f;

    public int ApplePrice
    {
        get { return applePrice; }
    }

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
        StartCoroutine(SpawnApples());
    }

    //spawn apples in locations
    private IEnumerator SpawnApples()
    {
        //create reference for the current apple prefab
        Apples currentApple;
        //the location where the apples will be looked for
        int location = Random.Range(0, AppleLocations.Count);
        
        if (!AppleLocations[location].GetComponentInChildren<Apples>())
        {
            //spawn the apples and set it to the current apple bein looked for
            Apples apple = Instantiate<Apples>(applePrefab, AppleLocations[location].transform.position, AppleLocations[location].transform.rotation);
            apple.transform.parent = AppleLocations[location].transform;
            currentApple = apple;
        }
        else
        {
            //set the apple to the current apples
            currentApple = AppleLocations[location].GetComponentInChildren<Apples>();
        }

        if (!currentApple.HasApples)
        {
            //add apples if there is none
            currentApple.CreateApples();
        }
        //wait for the alotted time
        yield return new WaitForSeconds(appleSpawnTime);

        //start again
        StartCoroutine(SpawnApples());
    }
}
