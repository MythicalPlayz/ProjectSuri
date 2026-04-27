using System.Collections;
using UnityEngine;

public class InventoryVisible : MonoBehaviour
{
    public string inventoryName;
    InventorySystem inventorySystem;

    public GameObject foodDisappear;

    void Start()
    {
        inventorySystem = FindFirstObjectByType<InventorySystem>();
    }

    public void TurnOff()
    {
        foodDisappear.SetActive(false);
        StartCoroutine(TurnOn());
    }

    IEnumerator TurnOn()
    {
        yield return new WaitForSeconds(inventorySystem.orderDelay);
        foodDisappear.SetActive(true);
    }
}
