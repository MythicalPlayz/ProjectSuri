using UnityEngine;

public class Takeout : MonoBehaviour
{
    public GameObject loc1;
    public GameObject loc2;
    public GameObject loc3;
    public GameObject loc4;

    private bool isTaken1 = false;
    private bool isTaken2 = false;
    private bool isTaken3 = false;
    private bool isTaken4 = false;
    private int availableLocations = 4;

    public void TakeLocation(GameObject suri)
    {
        if (!suri.GetComponent<Suri>())
            return;

        Suri details = suri.GetComponent<Suri>();
        if (!details.bag)
            return;

        if (availableLocations <= 0)
            return;

        if (!isTaken1)
        {
            isTaken1 = true;
            availableLocations--;
            suri.transform.SetParent(loc1.transform);
            suri.transform.position = loc1.transform.position;
            suri.transform.rotation = loc1.transform.rotation;
            details.takeout = gameObject;
            details.tID = 1;
        }
        else if (!isTaken2)
        {
            isTaken2 = true;
            availableLocations--;
            suri.transform.SetParent(loc2.transform);
            suri.transform.position = loc2.transform.position;
            suri.transform.rotation = loc2.transform.rotation;
            details.takeout = gameObject;
            details.tID = 2;
        }
        else if (!isTaken3)
        {
            isTaken3 = true;
            availableLocations--;
            suri.transform.SetParent(loc3.transform);
            suri.transform.position = loc3.transform.position;
            suri.transform.rotation = loc3.transform.rotation;
            details.takeout = gameObject;
            details.tID = 3;
        }
        else if (!isTaken4)
        {
            isTaken4 = true;
            availableLocations--;
            suri.transform.SetParent(loc4.transform);
            suri.transform.position = loc4.transform.position;
            suri.transform.rotation = loc4.transform.rotation;
            details.takeout = gameObject;
            details.tID = 4;
        }
    }

    public void FreeLocation(int ID)
    {
        switch (ID)
        {
            case 1:
                isTaken1 = false;
                break;
            case 2:
                isTaken2 = false;
                break;
            case 3:
                isTaken3 = false;
                break;
            case 4:
                isTaken4 = false;
                break;
        }
        availableLocations++;
    }
}
