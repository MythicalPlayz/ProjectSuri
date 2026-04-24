using System.Collections.Generic;
using UnityEngine;

public class SuriMaker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject suriObj;
    public GameObject suriLoc;
    private bool hasSuri = false;
    public string[] validIngredients = new string[] { "Ketchup", "Mustard", "Garlic", "Mayo" };



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
            hasSuri = true;
            return;
        }
        if (selectedGameObject.GetComponent<IngredientType>() != null && hasSuri)
        {
            string ingredientType = selectedGameObject.GetComponent<IngredientType>().ingredientType.ToString();
            if (System.Array.Exists(validIngredients, ingredient => ingredient == ingredientType))
            {
                suriObj.GetComponent<Suri>().AddIngredient(ingredientType);
            }
        }

    }
}
