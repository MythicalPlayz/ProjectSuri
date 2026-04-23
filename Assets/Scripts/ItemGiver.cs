using UnityEngine;

public class ItemGiver : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject itemToGive;

    void Start()
    {
        if (itemToGive == null)
        {
            Debug.LogError("ItemGiver: No item assigned to give!");
        }
    }

    public GameObject GiveItem(GameObject hand)
    {
        if (hand.transform.childCount > 0)
        {
            Debug.LogWarning("ItemGiver: Hand already has an item, cannot give another!");
            return null;
        }
        GameObject item = Instantiate(itemToGive, hand.transform.position, Quaternion.identity);
        item.transform.SetParent(hand.transform);
        return item;
    }
}
