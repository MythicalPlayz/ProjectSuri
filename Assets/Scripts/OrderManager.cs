using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public int OrderIDCounter = 0;
    public List<Order> Orders = new List<Order>();
    public int activeOrders = 0;
    public int maxOrders = 4;

    [SerializeField] GameObject ordersBoard;
    [SerializeField] GameObject orderRecieptPrefab;

    public class Order
    {
        public int id;
        public float timeOfCreation;
        public Status stat;
        Dictionary<string, bool> ingredients;

        public enum Status { pending, completed, failed }

        public Order(int id)
        {
            this.id = id;
            this.timeOfCreation = Time.time;
            this.stat = Status.pending;
            this.ingredients = new Dictionary<string, bool>
            {
                { "SuriBread", true },
                { "Fries", false },
                { "Chicken", false },
                { "Ketchup", false },
                { "Mustard", false },
                { "Mayo", false },
                { "Garlic", false },
                { "Tomato", false },
                { "Cheese", false },
                { "Spicy", false },
                { "Pepper", false }
            };
        }

        public void GenerateRandomOrder()
        {
            bool friesIncluded = false;
            bool chickenIncluded = false;
            foreach (var ingredient in new List<string>(ingredients.Keys))
            {
                if (ingredient == "SuriBread")
                    continue; // Always include SuriBread

                bool include = Random.value > 0.5f;

                if (ingredient == "Fries")
                    friesIncluded = include;
                if (ingredient == "Chicken")
                    chickenIncluded = include;

                this.ingredients[ingredient] = include;
            }

            // Ensure at least one of Fries or Chicken is included
            if (!friesIncluded && !chickenIncluded)
            {
                if (Random.value > 0.5f)
                {
                    this.ingredients["Fries"] = true;
                }
                else
                {
                    this.ingredients["Chicken"] = true;
                }
            }
        }

        public string[] GetOrderDetails()
        {
            string orderIdStr = $"Order: #{id}";
            string timeStr = $"Time: {timeOfCreation}";
            string statusStr = $"Status: {stat}";
            string ingredientsStr = "Ingredients:\n";
            foreach (var ingredient in ingredients)
            {
                if (ingredient.Value)
                {
                    ingredientsStr += $"- {ingredient.Key}\n";
                }
            }
            return new string[] { orderIdStr, timeStr, statusStr, ingredientsStr };
        }
    }

    public void GenerateOrder()
    {
        if (activeOrders >= maxOrders)
        {
            Debug.Log("Maximum active orders reached. Cannot generate new order.");
            return;
        }
        activeOrders++;
        Order newOrder = new Order(OrderIDCounter);
        newOrder.GenerateRandomOrder();
        Orders.Add(newOrder);
        OrderIDCounter++;
        string[] stra = newOrder.GetOrderDetails();
        CreateRecipt(newOrder.id, newOrder.timeOfCreation, stra[3]);
    }

    private void CreateRecipt(int id, float time, string text)
    {
        GameObject reciept = Instantiate(orderRecieptPrefab);
        reciept.transform.SetParent(ordersBoard.transform, false);
        ordersBoard.GetComponent<OrderBoard>().PlaceReciept(reciept);
        reciept.GetComponent<OrderReciept>().Initialize(id, time, text);
    }
}