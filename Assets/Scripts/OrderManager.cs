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

    [SerializeField] float bonusBoost = 0.5f; // 50% bonus for completing orders quickly
    [SerializeField] float accuracyBoost = 0.5f; // 50% boost based on accuracy
    [SerializeField] float timeLimit = 60f; // Time limit for bonus eligibility in seconds
    [SerializeField] int defaultScore = 100;

    public class Order
    {
        public int id;
        public float timeOfCreation;
        public Status stat;
        public Dictionary<string, bool> ingredients;

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

    public int CompleteOrder(GameObject suri, OrderReciept orderd)
    {
        if (!suri.GetComponent<Suri>() || !suri.GetComponent<Suri>().bag)
        {
            Debug.Log("Not a valid Suri to complete an order.");
            return 0;
        }
        Suri s = suri.GetComponent<Suri>();
        int total = 0;
        float timeTaken = Time.time;
        Order order = Orders[orderd.orderNumber];

        order.stat = Order.Status.completed;
        float timediff = timeTaken - order.timeOfCreation;
        float accuracy = 0f;
        float totalIngredients = 10f;
        float correctIngredients = 0f;

        //

        if (order.ingredients["Fries"] == (s.mainType == 1 || s.mainType == 3))
        {
            correctIngredients++;
        }
        
        if (order.ingredients["Chicken"] == (s.mainType == 2 || s.mainType == 3))
        {
            correctIngredients++;
        }

        if (order.ingredients["Ketchup"] == s.ketchup)
        {
            correctIngredients++;
        }

        if (order.ingredients["Mustard"] == s.mustaard)
        {
            correctIngredients++;
        }

        if (order.ingredients["Mayo"] == s.mayo)
        {
            correctIngredients++;
        }

        if (order.ingredients["Garlic"] == s.garlic)
        {
            correctIngredients++;
        }

        if (order.ingredients["Tomato"] == s.tomato)
        {
            correctIngredients++;
        }

        if (order.ingredients["Cheese"] == s.cheese)
        {
            correctIngredients++;
        }

        if (order.ingredients["Spicy"] == s.spicy)
        {
            correctIngredients++;
        }

        if (order.ingredients["Pepper"] == s.pepper)
        {
            correctIngredients++;
        }

        accuracy = correctIngredients / totalIngredients;

        if (accuracy == 1f)
        {
            accuracy += accuracyBoost; // Full accuracy bonus
        }


        if (timediff < timeLimit)
        {
            accuracy += bonusBoost; // Bonus is scaled by accuracy
        }

        else if (timediff < timeLimit * 2)
        {
            // no boost
        }
        else
        {
            accuracy -= bonusBoost; // Penalty for taking too long
            accuracy = Mathf.Max(0f, accuracy); // Ensure accuracy doesn't go negative
        }

        total = (int)(defaultScore * accuracy);

        return total;
    }
}