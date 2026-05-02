using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Register : MonoBehaviour
{
   private OrderManager orderManager;

    public bool onCooldown = false;
    public float cooldownTime = 3f;
    public int pendingOrdersToAccept = 0;
    public Queue<GameObject> pendingCustomers = new Queue<GameObject>();
    private void Start()
    {
        orderManager = GameObject.FindFirstObjectByType<OrderManager>();
    }

    public void RegisterOrder()
    {
        if (onCooldown) {

            //Debug.Log("On Cooldown");
            return;
        }

        if (pendingOrdersToAccept == 0)
        {
            //Debug.Log("No pending orders to accept.");
            return;
        }
        GameObject customer = pendingCustomers.Peek();
        bool status = orderManager.GenerateOrder(customer);
        if (status)
        {
            customer.GetComponent<Customer>().state = 2;
            pendingOrdersToAccept--;
            pendingCustomers.Dequeue();
            onCooldown = true;
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownTime); // Cooldown duration of 5 seconds
        onCooldown = false;
    }

}
