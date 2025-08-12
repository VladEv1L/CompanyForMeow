using System.Collections.Generic;
using UnityEngine;

public class DropGenerator : MonoBehaviour
{
    public static DropGenerator instance;
    public GameObject[] waypoints;
    public GameObject[] dropVariants;
    [Range(1, 100)]
    public int spawnProbability;

    private List<int> usedWaypoints = new List<int>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        GenerateDrops();
    }

    public void GenerateDrops()
    {
        int minDrops = 5;
        int maxDrops = 15;


        int numOfDrops = Random.Range(minDrops, maxDrops + 1);

        for (int i = 0; i < numOfDrops; ++i)
        {

            if (Random.Range(0, 100) < spawnProbability)
            {

                int waypointIndex = GetUnusedWaypointIndex();


                if (waypointIndex == -1) break;


                int dropVariantIndex = Random.Range(0, dropVariants.Length);
                GameObject selectedDrop = dropVariants[dropVariantIndex];


                Vector3 position = waypoints[waypointIndex].transform.position;
                Instantiate(selectedDrop, position, Quaternion.identity);

            }
        }
    }

    private int GetUnusedWaypointIndex()
    {
        while (true)
        {
            int randomIndex = Random.Range(0, waypoints.Length);
            if (!usedWaypoints.Contains(randomIndex))
            {
                usedWaypoints.Add(randomIndex);
                return randomIndex;
            }

            if (usedWaypoints.Count >= waypoints.Length)
                return -1;
        }
    }
}