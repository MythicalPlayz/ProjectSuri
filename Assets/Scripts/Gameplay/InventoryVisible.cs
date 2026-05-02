using System.Collections;
using UnityEngine;

public class InventoryVisible : MonoBehaviour
{
    public string inventoryName;
    InventorySystem inventorySystem;

    public GameObject foodDisappear;
    public HandleBar handleBar;

    void Start()
    {
        inventorySystem = FindFirstObjectByType<InventorySystem>();
        if (handleBar)
        {
            handleBar.maxTime = inventorySystem.orderDelay;
            handleBar.gameObject.SetActive(false);
        }
    }

    public void TurnOff()
    {
        if (handleBar)
        { 
            handleBar.Reset();
            handleBar.StartTimer();
        }
        foodDisappear.SetActive(false);
        StartCoroutine(TurnOn());
    }

    IEnumerator TurnOn()
    {
        yield return new WaitForSeconds(inventorySystem.orderDelay);
        foodDisappear.SetActive(true);
    }
}
