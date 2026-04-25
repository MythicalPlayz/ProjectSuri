using System.Collections;
using UnityEngine;

public class Register : MonoBehaviour
{
   private OrderManager orderManager;

    public bool onCooldown = false;
    public float cooldownTime = 10f;

    private void Start()
    {
        orderManager = GameObject.FindFirstObjectByType<OrderManager>();
    }

    public void RegisterOrder()
    {
        if (onCooldown) {

            Debug.Log("On Cooldown");
            return;
        }
        orderManager.GenerateOrder();
        onCooldown = true;
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownTime); // Cooldown duration of 5 seconds
        onCooldown = false;
    }

}
