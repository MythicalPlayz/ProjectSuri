using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject selectedGameObject;
    public GameObject orderManagerObject;
    private OrderManager orderManager;
    private UiManager uiManager;

    public int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI moneyText;
    public float timer = 300f;
    public bool isGameActive = true;
    public float money = 100f;

    private AudioSource audioSource;

    public Camera mainCamera;
    public Camera ramsisCamera;

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
        uiManager = GameObject.FindFirstObjectByType<UiManager>();
        audioSource = GetComponent<AudioSource>();
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
        else if (isGameActive)
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
        mainCamera.gameObject.SetActive(false);
        ramsisCamera.gameObject.SetActive(true);
        uiManager.GameOver();
        uiManager.DisplayRamsisMessage(GetRamsisMessage());
    }

    private string GetRamsisMessage()
    {
        if (money < 0)
            return "WHAT THE F%#K MAN!\n You are in Debt!\n I don't care how you did cause this is embaressing.";
        else if (score >= 1000)
            return "Golden Ramsis: Congrats Buddy You have proven to me that you are the master of Suri";
        else if (score >= 500)
            return "Golden Ramsis: Not Bad Kid!\n You still have a long way to go.";
        else if (score >= 250)
            return "Golden Ramsis: That is the worst F%#king Service I have seen with my own eyes.\n Get Better.";
        else
            return "Golden Ramsis: ARE YOU A F%#KING IDIOT SURI!\n NOT A SINGLE CUSTOMER YOU GOT RIGHT!";
    }

    public void ChangeMoney(float value)
    {
        money += value;
        moneyText.text = "Money: " + money.ToString("F2") + " EGP";
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
