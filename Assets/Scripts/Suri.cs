using UnityEngine;

public class Suri : MonoBehaviour
{
    public int mainType; // 0 = none, 1 = fries, 2 = chicken, 3 = both
    public bool ketchup; // 0 = none, 1 = have; K
    public bool mustaard; // M
    public bool mayo; // Y
    public bool garlic; // G
    public bool tomato; // T
    public bool cheese; // C
    public bool spicy; // S
    public bool pepper; // P
    public bool wrapper; // W
    public bool grillMarks; // L
    public bool closed = false;

    public GameObject friesObj;
    public GameObject chickenObj;
    public GameObject ketchupObj;
    public GameObject mustardObj;
    public GameObject mayoObj;
    public GameObject garlicObj;
    public GameObject tomatoObj;
    public GameObject cheeseObj;
    public GameObject spicyObj;
    public GameObject pepperObj;
    public GameObject wrapperObj;
    public GameObject grillMarksObj;

    public GameObject maker;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainType = 0;
        ketchup = false;
        mustaard = false;
        mayo = false;
        garlic = false;
        tomato = false;
        cheese = false;
        spicy = false;
        pepper = false;
        wrapper = false;
        grillMarks = false;
        
        friesObj.SetActive(false);
        chickenObj.SetActive(false);
        ketchupObj.SetActive(false);
        mustardObj.SetActive(false);
        mayoObj.SetActive(false);
        garlicObj.SetActive(false);
        tomatoObj.SetActive(false);
        cheeseObj.SetActive(false);
        spicyObj.SetActive(false);
        pepperObj.SetActive(false);
        wrapperObj.SetActive(false);
        grillMarksObj.SetActive(false);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    //TEMP CODE PLEASE DELETE LATER, FOR TESTING PURPOSES ONLY
    //    if (mainType == 0) {
    //        friesObj.SetActive(false);
    //        chickenObj.SetActive(false);
    //    }
    //    else if (mainType == 1) {
    //        friesObj.SetActive(true);
    //        chickenObj.SetActive(false);
    //    }
    //    else if (mainType == 2) {
    //        friesObj.SetActive(false);
    //        chickenObj.SetActive(true);
    //    }
    //    else if (mainType == 3) {
    //        friesObj.SetActive(true);
    //        chickenObj.SetActive(true);
    //    }
    //    else
    //    {
    //        Debug.LogError("Invalid mainType value: " + mainType);
    //    }

    //    ketchupObj.SetActive(ketchup);
    //    mustardObj.SetActive(mustaard);
    //    mayoObj.SetActive(mayo);
    //    garlicObj.SetActive(garlic);
    //    tomatoObj.SetActive(tomato);
    //    cheeseObj.SetActive(cheese);
    //    spicyObj.SetActive(spicy);
    //    pepperObj.SetActive(pepper);
    //    wrapperObj.SetActive(wrapper);
    //    grillMarksObj.SetActive(grillMarks);

    //}

    public void AddIngredient(string ingredient)
    {
        switch (ingredient)
        {
            //case "Fries":
            //    mainType = 1;
            //    break;
            //case "Chicken":
            //    mainType = 2;
            //    break;
            case "Ketchup":
                ketchup = true;
                ketchupObj.SetActive(true);
                break;
            case "Mustard":
                mustaard = true;
                mustardObj.SetActive(true);
                break;
            case "Mayo":
                mayo = true;
                mayoObj.SetActive(true);
                break;
            case "Garlic":
                garlic = true;
                garlicObj.SetActive(true);
                break;
                // TODO More Ingredients here
        }
    }
}
