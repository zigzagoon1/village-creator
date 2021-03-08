using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawnManager : MonoBehaviour
{
    public Vector3 playerPos;
    public float playerPosX;
    public float playerPosZ;
    public GameObject treePrefab;

    private float treeSpawnRangeX;
    private float treeSpawnRangeZ;

    void Start()
    {
        playerPos = GameObject.Find("PlayerController").transform.position;
        playerPosX = playerPos.x;
        playerPosZ = playerPos.z;
        SpawnTrees(Random.Range(1, 10));
    }
    void SpawnTrees(int number)
    {
        for (int num = 0; num < number; num++)
        {
            Instantiate(treePrefab, new Vector3(Random.Range(playerPosX - 50, playerPosX + 50), 0, Random.Range(playerPosZ - 50, playerPosZ + 50)), treePrefab.transform.rotation);
            
        }
    }
    void Update()
    {
        
    }
}
