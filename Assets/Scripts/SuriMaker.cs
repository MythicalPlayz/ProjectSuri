using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SuriMaker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject suriObj;
    public GameObject suriLoc;
    private bool hasSuri = false;
    private Dictionary<string, int> validIngredients;

    private InventorySystem inventorySystem;
    private string[] limitedIngredients = new string[] { "Cheese", "Tomato", "Spicy", "Pepper" };

    void Start()
    {
        ResetIngredients();
        inventorySystem = FindFirstObjectByType<InventorySystem>();
    }

    private void ResetIngredients()
    {
        validIngredients = new Dictionary<string, int>();
        validIngredients.Add("Fries", 0);
        validIngredients.Add("Chicken", 0);
        validIngredients.Add("Ketchup", 0);
        validIngredients.Add("Mustard", 0);
        validIngredients.Add("Mayo", 0);
        validIngredients.Add("Garlic", 0);
        validIngredients.Add("Tomato", 0);
        validIngredients.Add("Cheese", 0);
        validIngredients.Add("Spicy", 0);
        validIngredients.Add("Pepper", 0);

    }

    public void MakeSuri(GameObject selectedGameObject)
    {
        Debug.Log("Making Suri with:" + selectedGameObject.name);
        if ((hasSuri && selectedGameObject.GetComponent<Suri>() != null) || !hasSuri && selectedGameObject.GetComponent<Suri>() == null)
        {
            return;
        }
        if (selectedGameObject.GetComponent<Suri>() != null && !hasSuri)
        {
            suriObj = selectedGameObject;
            selectedGameObject.transform.SetParent(suriLoc.transform);
            suriObj.transform.position = suriLoc.transform.position;
            suriObj.GetComponent<Suri>().maker = gameObject;
            if (!suriObj.GetComponent<Suri>().UnTouched())
                suriObj.GetComponent<Suri>().Wrap(false);
            hasSuri = true;
            ResetIngredients();
            return;
        }
        if (selectedGameObject.GetComponent<IngredientType>() != null && hasSuri)
        {
            string ingredientType = selectedGameObject.GetComponent<IngredientType>().ingredientType.ToString();
            if (ingredientType == "Fries" || ingredientType == "Chicken")
            {
                Destroy(selectedGameObject);
            }
            if (validIngredients.ContainsKey(ingredientType) && validIngredients[ingredientType] == 0)
            {
                if (limitedIngredients.Contains(ingredientType))
                {
                    int ingredientCount = inventorySystem.GetIngredientCount(ingredientType);
                    if (ingredientCount <= 0)
                    {
                        Debug.LogWarning("SuriMaker: No " + ingredientType + " left in inventory to add to Suri!");
                        return;
                    }
                    if (ingredientCount == 1)
                    {
                        InventoryVisible inventory = selectedGameObject.GetComponent<InventoryVisible>();
                        if (inventory)
                        {
                            inventory.TurnOff();
                        }
                        inventorySystem.OrderMore(ingredientType);
                    }
                }
                inventorySystem.UseIngredient(ingredientType);
                suriObj.GetComponent<Suri>().AddIngredient(ingredientType);
                validIngredients[ingredientType] = 1;
            }
        }

    }

    public void RemoveSuri()
    {
        suriObj = null;
        hasSuri = false;
    }
}
