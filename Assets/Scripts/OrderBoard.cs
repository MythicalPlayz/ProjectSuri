using UnityEngine;

public class OrderBoard : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject OL1;
    public GameObject OL2;
    public GameObject OL3;
    public GameObject OL4;

    public bool isOL1Occupied = false;
    public bool isOL2Occupied = false;
    public bool isOL3Occupied = false;
    public bool isOL4Occupied = false;

    public void PlaceReciept(GameObject reciept)
    {
        if (!isOL1Occupied)
        {
            reciept.transform.SetParent(OL1.transform);
            reciept.transform.position = OL1.transform.position;
            reciept.transform.rotation = OL1.transform.rotation;
            reciept.GetComponent<OrderReciept>().id = 1;
            isOL1Occupied = true;
        }
        else if (!isOL2Occupied)
        {
            reciept.transform.SetParent(OL2.transform);
            reciept.transform.position = OL2.transform.position;
            reciept.transform.rotation = OL2.transform.rotation;
            reciept.GetComponent<OrderReciept>().id = 1;
            isOL2Occupied = true;
        }
        else if (!isOL3Occupied)
        {
            reciept.transform.SetParent(OL3.transform);
            reciept.transform.position = OL3.transform.position;
            reciept.transform.rotation = OL3.transform.rotation;
            reciept.GetComponent<OrderReciept>().id = 1;
            isOL3Occupied = true;
        }
        else if (!isOL4Occupied)
        {
            reciept.transform.SetParent(OL4.transform);
            reciept.transform.position = OL4.transform.position;
            reciept.transform.rotation = OL4.transform.rotation;
            reciept.GetComponent<OrderReciept>().id = 1;
            isOL4Occupied = true;
        }
        reciept.GetComponent<OrderReciept>().orderBoard = gameObject;
        //else
        //{
        //    Debug.Log("All order slots are occupied. Cannot place new receipt.");
        //}
    }
    public void FreeSpace(int id)
    {
        switch (id)
        {
            case 1:
                isOL1Occupied = false;
                break;
            case 2:
                isOL2Occupied = false;
                break;
            case 3:
                isOL3Occupied = false;
                break;
            case 4:
                isOL4Occupied = false;
                break;
            default:
                Debug.Log("Invalid order slot ID. Must be between 1 and 4.");
                break;
        }
    }
}
