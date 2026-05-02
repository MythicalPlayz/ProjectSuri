using UnityEngine;

public class HandleChildChange : MonoBehaviour
{

    private Fryer fryerScript;
    public bool firstHan = true;
    private void Start()
    {
        fryerScript = gameObject.transform.GetComponentInParent<Fryer>();
    }


    private void OnTransformChildrenChanged()
    {
        if (transform.childCount > 1) //billboard UI
        {
            //added
        }
        else
        {
            fryerScript.availableSlots++;
            if (firstHan)
                fryerScript.usingHan1 = false;
            else
                fryerScript.usingHan2 = false;
        }
    }
}
