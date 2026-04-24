using System.Collections;
using UnityEngine;

public class SuriGrill : MonoBehaviour
{
    public GameObject suriLoc;
    private GameObject suriObj;

    public float grillTime = 5f;
    
    public void Grill(GameObject suri)
    {
        if (suriObj != null) return; // Already grilling something

        if (!suri.GetComponent<Suri>())
            return;

        if (!suri.GetComponent<Suri>().wrapped)
            return;

        suriObj = suri;
        suriObj.transform.SetParent(suriLoc.transform); // Parent to the grill for organization
        suriObj.transform.position = suriLoc.transform.position;
        suriObj.SetActive(false);
        StartCoroutine(GrillCoroutine());
    }

    IEnumerator GrillCoroutine()
    {
        yield return new WaitForSeconds(grillTime);
        if (suriObj != null)
        {
            // Here you can add code to change the suri's state to "grilled" or similar
            Debug.Log("Suri has been grilled!");
            suriObj.GetComponent<Suri>().AddGrillMarks();
            suriObj.SetActive(true); // Make the suri visible again after grilling
        }
    }

    private void FixedUpdate()
    {
        if (suriObj != null)
        {
            if (suriObj.transform.parent != suriLoc.transform)
            {
                // The suri has been removed from the grill, stop grilling
                StopAllCoroutines();
                suriObj = null;
            }
        }
    }
}
