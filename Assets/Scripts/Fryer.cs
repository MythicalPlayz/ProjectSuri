using System.Collections;
using UnityEngine;

public class Fryer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float pTime = 10f;
    public float cTime = 20f;

    public GameObject pObj;
    public GameObject cObj;

    public int availableSlots = 2;
    [SerializeField] private GameObject currentFoodHan1;
    [SerializeField] private GameObject currentFoodHan2;


    public void FryFood(GameObject holding)
    {
        if (!holding.CompareTag("FrozenFood"))
            return;

        if (availableSlots <= 0)
            return;

        if (holding.gameObject.GetComponent<FrozenFood>().type == FrozenFood.FrozenType.Fries)
        {
            StartCoroutine(FryingProcess(pObj, pTime));
            availableSlots--;
            Destroy(holding);
        }
        else if (holding.gameObject.GetComponent<FrozenFood>().type == FrozenFood.FrozenType.Chicken)
        {
            StartCoroutine(FryingProcess(cObj, cTime));
            availableSlots--;
            Destroy(holding);
        }
    }

    IEnumerator FryingProcess(GameObject food, float timeToFry)
    {
        yield return new WaitForSeconds(timeToFry);
        GameObject friedFood = SpawnObject(food);
        if (currentFoodHan1.transform.childCount == 0)
        {
            friedFood.transform.SetParent(currentFoodHan1.transform);
            friedFood.transform.position = currentFoodHan1.transform.position;
        }
        else if (currentFoodHan2.transform.childCount == 0)
        {
            friedFood.transform.SetParent(currentFoodHan2.transform);
            friedFood.transform.position = currentFoodHan2.transform.position;
        }
    }

    GameObject SpawnObject(GameObject food)
    {
        GameObject g = Instantiate(food, transform.position, Quaternion.identity);
        return g;
    }
}
