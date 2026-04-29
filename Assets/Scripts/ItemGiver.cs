using UnityEngine;

public class ItemGiver : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject itemToGive;
    public InventorySystem inventorySystem;
    public string itemName;

    void Start()
    {
        if (itemToGive == null)
        {
            Debug.LogError("ItemGiver: No item assigned to give!");
        }
        inventorySystem = FindFirstObjectByType<InventorySystem>();
    }

    public GameObject GiveItem(GameObject hand)
    {
        if (hand.transform.childCount > 0)
        {
            Debug.LogWarning("ItemGiver: Hand already has an item, cannot give another!");
            return null;
        }
        int itemCount = inventorySystem.GetIngredientCount(itemName);

        if (itemCount <= 0)
        {
            Debug.LogWarning("ItemGiver: No " + itemName + " left in inventory to give!");
            return null;
        }

        inventorySystem.UseIngredient(itemName);
        if (itemCount == 1)
        {
            Debug.LogWarning("Last One Ordering More");
            inventorySystem.OrderMore(itemName);
            if (gameObject.GetComponent<InventoryVisible>())
                gameObject.GetComponent<InventoryVisible>().TurnOff();
        }

        GameObject item = Instantiate(itemToGive, hand.transform.position, Quaternion.identity);
        item.transform.SetParent(hand.transform);
        return item;
    }

    public void RemoveItem(GameObject item)
    {
       if (itemToGive != null && item.name.Contains(itemToGive.name))
       {
            inventorySystem.AddIngrdient(itemName);
            Destroy(item);
       }
       else
       {
           Debug.LogWarning("ItemGiver: Attempted to remove an item that doesn't match the assigned item!");
       }
    }
}
