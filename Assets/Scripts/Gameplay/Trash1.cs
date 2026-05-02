using UnityEngine;

public class Trash1 : MonoBehaviour
{
    public void Remove(GameObject obj)
    {
        if (obj.GetComponent<Suri>() && !obj.GetComponent<Suri>().UnTouched())
        {
            Destroy(obj);
            return;
        }
        IngredientType ingredientType = obj.GetComponent<IngredientType>();
        if (ingredientType && (ingredientType.ingredientType == GameManager.IngredientType.Fries || ingredientType.ingredientType == GameManager.IngredientType.Chicken))
        {
            Destroy(obj);
            return;
        }
    }
}
