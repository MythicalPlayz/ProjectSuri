using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject selectedGameObject;

    public enum InteractableType
    {
        Register,
        SuriHolder,
        PotatoFreezer,
        ChickenFreezer,
        Fryer,
        ItemHolding,
        SuriMaker,
        SuriGrill,
        Wrapper,
        TakeOut,
    }

    public enum IngredientType
    {
        SuriBread,
        Fries,
        Chicken,
        Ketchup,
        Mustard,
        Mayo,
        Garlic,
        Tomato,
        Cheese,
        Spicy,
        Pepper,
    }

    public void ChangeHighlightedObject(GameObject gameObject)
    {
        if (gameObject == selectedGameObject)
            return; // No change, do nothing

        if (gameObject == null)
        {
            if (selectedGameObject != null)
            {
                selectedGameObject.GetComponent<Outline>().enabled = false;
                selectedGameObject = null;
            }
            return; // No object to highlight, exit the method
        }
        if (selectedGameObject != null)
        {
            selectedGameObject.GetComponent<Outline>().enabled = false;
        }
        selectedGameObject = gameObject;
        selectedGameObject.GetComponent<Outline>().enabled = true;
    }
}
