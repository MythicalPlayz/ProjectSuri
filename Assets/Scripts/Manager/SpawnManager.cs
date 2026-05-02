using System.Collections;
using UnityEditor;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameManager GameManager;
    public GameObject customerPrefab;
    public GameObject npcPrefab;
    public GameObject[] start;
    public GameObject end;
    public float isCustomer = 0.5f;
    public float delay = 5f;
    public float spawnInterval = 15f;

    void Start()
    {
        GameManager = GameObject.FindAnyObjectByType<GameManager>();
        InvokeRepeating("SpawnCustomer", delay,spawnInterval); // Adjusted to use a coroutine method
    }

    void Update()
    {
        if (GameManager.isGameOver)
        {
            CancelInvoke("SpawnCustomer"); // Correct way to stop InvokeRepeating
        }
    }

    void SpawnCustomer()
    {
        
        Instantiate(customerPrefab, start[Random.Range(0, start.Length)].transform.position, Quaternion.identity);
    }
}
