using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    Dictionary<GameManager.IngredientType, bool> ingredients = new Dictionary<GameManager.IngredientType, bool>
    {
        { GameManager.IngredientType.SuriBread, true },
        { GameManager.IngredientType.Fries, false },
        { GameManager.IngredientType.Chicken, false },
        { GameManager.IngredientType.Ketchup, false },
        { GameManager.IngredientType.Mustard, false },
        { GameManager.IngredientType.Mayo, false },
        { GameManager.IngredientType.Garlic, false },
        { GameManager.IngredientType.Tomato, false },
        { GameManager.IngredientType.Cheese, false },
        { GameManager.IngredientType.Spicy, false },
        { GameManager.IngredientType.Pepper, false }
    };

    public Order()
    {
        GenerateRandomOrder();
    }

    public Order(params GameManager.IngredientType[] ingredients)
    {
        foreach (var ingredient in ingredients)
        {
            this.ingredients[ingredient] = true;
        }
    }

    void GenerateRandomOrder()
    {
        bool friesIncluded = false;
        bool chickenIncluded = false;

        foreach (var ingredient in ingredients.Keys)
        {
            if (ingredient == GameManager.IngredientType.SuriBread)
                continue; // Always include SuriBread

            bool include = Random.value > 0.5f;
            if (ingredient == GameManager.IngredientType.Fries)
                friesIncluded = include;
            if (ingredient == GameManager.IngredientType.Chicken)
                chickenIncluded = include;

            this.ingredients[ingredient] = include;
        }

        // Ensure at least one of Fries or Chicken is included
        if (!friesIncluded && !chickenIncluded)
        {
            if (Random.value > 0.5f)
            {
                this.ingredients[GameManager.IngredientType.Fries] = true;
            }
            else
            {
                this.ingredients[GameManager.IngredientType.Chicken] = true;
            }
        }
    }

    public void GetOrder()
    {
        string orderDetails = "Order: ";
        foreach (var ingredient in ingredients)
        {
            if (ingredient.Value) // If the ingredient is included in the order
            {
                orderDetails += ingredient.Key.ToString() + " ";
            }
        }
        //Debug.Log(orderDetails.Trim());
    }
}
