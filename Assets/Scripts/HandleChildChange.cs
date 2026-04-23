using UnityEngine;

public class HandleChildChange : MonoBehaviour
{

    private Fryer fryerScript;

    private void Start()
    {
        fryerScript = gameObject.transform.GetComponentInParent<Fryer>();
    }


    private void OnTransformChildrenChanged()
    {
        if (transform.childCount > 0)
        {

        }
        else
        {
            fryerScript.availableSlots++;
        }
    }
}
