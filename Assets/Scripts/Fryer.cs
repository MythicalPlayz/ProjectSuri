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

    [SerializeField] private GameObject handle1P;
    [SerializeField] private GameObject handle2P;
    [SerializeField] private GameObject handle1A;
    [SerializeField] private GameObject handle2A;

    public HandleBar h1;
    public HandleBar h2;

    public bool usingHan1 = false;
    public bool usingHan2 = false;



    void Start()
    {
        handle1A.SetActive(false);
        handle2A.SetActive(false);
        h1.gameObject.SetActive(false);
        h2.gameObject.SetActive(false);
    }

    public void FryFood(GameObject holding)
    {
        if (!holding.CompareTag("FrozenFood"))
            return;

        if (availableSlots <= 0)
            return;


        GameObject currentLocation;
        GameObject currentHandleB;
        GameObject currentHandleA;

        if (!usingHan1)
        {
            currentLocation = currentFoodHan1;
            currentHandleB = handle1P;
            currentHandleA = handle1A;
            usingHan1 = true;
        }
        else if (!usingHan2)
        {
            currentLocation = currentFoodHan2;
            currentHandleB = handle2P;
            currentHandleA = handle2A;
            usingHan2 = true;
        }
        else
        {
            // No available handles, return early
            return;
        }


        if (holding.gameObject.GetComponent<FrozenFood>().type == FrozenFood.FrozenType.Fries || holding.gameObject.GetComponent<FrozenFood>().type == FrozenFood.FrozenType.Chicken)
        {
            GameObject sObj = (holding.gameObject.GetComponent<FrozenFood>().type == FrozenFood.FrozenType.Fries) ? pObj : cObj;
            float sTime = (sObj == pObj) ? pTime : cTime;
            currentHandleA.SetActive(true);
            currentHandleB.SetActive(false);
            GameObject canvas = currentLocation.transform.GetChild(0).gameObject;
            HandleBar handleBar = canvas.GetComponent<HandleBar>();
            if (handleBar)
            {
                handleBar.gameObject.SetActive(true);
                handleBar.maxTime = sTime;
                handleBar.StartTimer();
            }
            currentLocation.GetComponent<AudioSource>().Play();
            StartCoroutine(FryingProcess(sObj, sTime, currentLocation, currentHandleB, currentHandleA));
            availableSlots--;
            Destroy(holding);
        }
    }

    IEnumerator FryingProcess(GameObject food, float timeToFry, GameObject currentFoodLocation, GameObject handleB, GameObject handleA)
    {
        yield return new WaitForSeconds(timeToFry);
        handleA.SetActive(false);
        handleB.SetActive(true);
        GameObject friedFood = SpawnObject(food);
        friedFood.transform.SetParent(currentFoodLocation.transform);
        friedFood.transform.position = currentFoodLocation.transform.position;
        currentFoodLocation.GetComponent<AudioSource>().Stop();
    }

    GameObject SpawnObject(GameObject food)
    {
        GameObject g = Instantiate(food, transform.position, Quaternion.identity);
        return g;
    }
}
