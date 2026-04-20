using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IngredientsHolding : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Dictionary<string, GameObject> locations = new Dictionary<string, GameObject>{ };

    public GameObject ketLoc;
    public GameObject musLoc;
    public GameObject garLoc;
    public GameObject mayLoc;

    private void Start()
    {
        locations.Add("Ketchup", ketLoc);
        locations.Add("Mustard", musLoc);
        locations.Add("Garlic", garLoc);
        locations.Add("Mayo", mayLoc);
    }

    public void AddIngredient(GameObject Ingredient)
    {
        Debug.Log("Adding Ingredient:" + Ingredient.name);
        string name = Ingredient.name; //.Replace("(Clone)", "").Trim();
        if (locations.ContainsKey(name))
        {
            Ingredient.transform.SetParent(locations[name].transform);
            Ingredient.transform.position = locations[name].transform.position;
        }
    }

    public void AddSuriBread() { }
}
