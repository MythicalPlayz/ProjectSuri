using UnityEngine;

public class SuriBagger : MonoBehaviour
{
   public void BagSuri(GameObject suri)
    {
        Suri suriComponent = suri.GetComponent<Suri>();
        if (suriComponent == null || !suriComponent.grillMarks)
            return;
        suriComponent.bag = true;
        suriComponent.bagObj.SetActive(true);
    }
}
