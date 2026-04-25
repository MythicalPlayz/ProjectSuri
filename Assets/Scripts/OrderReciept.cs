using TMPro;
using UnityEngine;

public class OrderReciept : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int orderNumber;
    public float orderTime;
    public string orderDetails;
    public GameObject orderBoard;
    public int id;

    [SerializeField] private TextMeshPro orderText;

    public void Initialize(int number, float time, string details)
    {
        orderNumber = number;
        orderTime = time;
        orderDetails = details;
        LoadText();
    }

    private void LoadText()
    {
        orderText.text = "Order #" + (orderNumber+1) + "\n" +
                    "Time: " + orderTime.ToString("F2") + "s\n" +
                    "Details: " + orderDetails;
    }
}
