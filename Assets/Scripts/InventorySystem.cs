using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int startCount = 5;

    private int suriBreakCount;
    private int frozenFriesCount;
    private int frozenChickenCount;

    private int cheeseCount;
    private int spicyCount;
    private int tomatoCount;
    private int pepperCount;

    private GameManager gameManager;
    public float orderDelay = 30f;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        suriBreakCount = startCount;
        frozenFriesCount = startCount;
        frozenChickenCount = startCount;
        cheeseCount = startCount;
        spicyCount = startCount;
        tomatoCount = startCount;
        pepperCount = startCount;
    }

    public void OrderMore(string str)
    {
        if (gameManager.isGameActive)
        {
            StartCoroutine(WaitForDelivery(str));
        }
    }

    IEnumerator WaitForDelivery(string str)
    {
        Debug.Log("Ordering more " + str + ", will arrive in " + orderDelay + " seconds.");
        yield return new WaitForSeconds(orderDelay);
        switch (str)
        {
            case "SuriBread":
                suriBreakCount += startCount;
                break;
            case "Fries":
                frozenFriesCount += startCount;
                break;
            case "Chicken":
                frozenChickenCount += startCount;
                break;
            case "Cheese":
                cheeseCount += startCount;
                break;
            case "Spicy":
                spicyCount += startCount;
                break;
            case "Tomato":
                tomatoCount += startCount;
                break;
            case "Pepper":
                pepperCount += startCount;
                break;
        }
        Debug.Log(str + " delivery has arrived! Current count: " + GetIngredientCount(str));
    }

    public int GetIngredientCount(string str)
    {
        switch (str)
        {
            case "SuriBread":
                return suriBreakCount;
            case "Fries":
                return frozenFriesCount;
            case "Chicken":
                return frozenChickenCount;
            case "Cheese":
                return cheeseCount;
            case "Spicy":
                return spicyCount;
            case "Tomato":
                return tomatoCount;
            case "Pepper":
                return pepperCount;
            default:
                Debug.LogWarning("InventorySystem: Invalid ingredient type requested: " + str);
                return 0;
        }
    }

    public void UseIngredient(string str)
    {
        switch (str)
        {
            case "SuriBread":
                suriBreakCount--;
                break;
            case "Fries":
                frozenFriesCount--;
                break;
            case "Chicken":
                frozenChickenCount--;
                break;
            case "Cheese":
                cheeseCount--;
                break;
            case "Spicy":
                spicyCount--;
                break;
            case "Tomato":
                tomatoCount--;
                break;
            case "Pepper":
                pepperCount--;
                break;
            default:
                Debug.LogWarning("InventorySystem: Invalid ingredient type requested: " + str);
                break;
        }
    }

    public void AddIngrdient(string str)
    {
        switch (str)
        {
            case "SuriBread":
                suriBreakCount++;
                break;
            case "Fries":
                frozenFriesCount++;
                break;
            case "Chicken":
                frozenChickenCount++;
                break;
        }
    }
}
