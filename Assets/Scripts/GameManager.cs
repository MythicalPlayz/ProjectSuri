using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject selectedGameObject;
    public GameObject orderManagerObject;
    private OrderManager orderManager;

    public int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public float timer = 300f;
    public bool isGameActive = true;

    public enum InteractableType
    {
        Register,
        SuriHolder,
        PotatoFreezer,
        ChickenFreezer,
        Fryer,
        ItemHolding,
        SuriMaker,
        SuriGrill,
        Wrapper,
        TakeOut,
        Trash,
        OrdersBoard,
    }

    public enum IngredientType
    {
        SuriBread,
        Fries,
        Chicken,
        Ketchup,
        Mustard,
        Mayo,
        Garlic,
        Tomato,
        Cheese,
        Spicy,
        Pepper,
    }

    public void ChangeHighlightedObject(GameObject gameObject)
    {
        if (!isGameActive)
            return;

        if (gameObject == selectedGameObject)
            return; // No change, do nothing

        if (gameObject == null)
        {
            if (selectedGameObject != null)
            {
                selectedGameObject.GetComponent<Outline>().enabled = false;
                selectedGameObject = null;
            }
            return; // No object to highlight, exit the method
        }
        if (selectedGameObject != null)
        {
            selectedGameObject.GetComponent<Outline>().enabled = false;
        }
        selectedGameObject = gameObject;
        selectedGameObject.GetComponent<Outline>().enabled = true;
    }

    private void Start()
    {
        orderManager = orderManagerObject.GetComponent<OrderManager>();
        score = 0;
        scoreText.text = "Score: " + score;
    }

    public void UpdateScore(int score)
    {
        this.score += score;
        scoreText.text = "Score:" + this.score;
    }

    public void UpdateTimer()
    {
        //timer = time;
        //int minutes = Mathf.FloorToInt(timer / 60f);
        //int seconds = Mathf.FloorToInt(timer % 60f);
        //timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        timer -= Time.deltaTime;
        if (timer >= 0)
            timerText.text = ((int)timer).ToString();
        else
        {
            EndGame();
             timerText.text = "0";
        }
    }

    public void Update()
    {
        UpdateTimer();
    }

    public void EndGame()
    {
        isGameActive = false;
        Time.timeScale = 0f;
    }
}
