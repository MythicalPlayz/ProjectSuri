using System.Collections;
using UnityEditor;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameManager GameManager;
    public GameObject customerPrefab;
    public GameObject start;

    void Start()
    {
        GameManager = GameObject.FindAnyObjectByType<GameManager>();
        InvokeRepeating("SpawnCustomer", 10,30); // Adjusted to use a coroutine method
    }

    void Update()
    {
        if (!GameManager.isGameActive)
        {
            CancelInvoke("SpawnCustomer"); // Correct way to stop InvokeRepeating
        }
    }

    void SpawnCustomer()
    {
        Instantiate(customerPrefab, start.transform.position, Quaternion.identity);
    }
}
