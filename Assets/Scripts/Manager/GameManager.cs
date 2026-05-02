using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public bool isGameOver = false;
    public float money = 100f;

    public Animator ramsisAnimator;

    private AudioSource audioSource;

    public Camera mainCamera;
    public Camera ramsisCamera;

    public bool isPaused = false;
    public AudioSource lofi;
    public int highScore = 0;

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
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        timer = PlayerPrefs.GetInt("GameTime", 300);
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

    public void PauseGame(bool pause)
    {
        if (pause)
        {
            isPaused = true;
            isGameActive = false;
            Time.timeScale = 0f;
            lofi.Pause();
        }
        else
        {
            isPaused = false;
            isGameActive = true;
            Time.timeScale = 1f;
            lofi.UnPause();
        }
    }

    public void EndGame()
    {
        isGameActive = false;
        isGameOver = true;
        // Time.timeScale = 0f;
        mainCamera.gameObject.SetActive(false);
        ramsisCamera.gameObject.SetActive(true);
        uiManager.GameOver();
        if (money >- 0 && score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
        lofi.Stop();
        StartCoroutine(Wait5Sec());
    }

    IEnumerator Wait5Sec()
    {
        yield return new WaitForSecondsRealtime(5);
        uiManager.DisplayRamsisMessage(GetRamsisMessage());
    }


    private string GetRamsisMessage()
    {
        if (money < 0)
        {
            ramsisAnimator.SetInteger("AnimationType", 0);
            return "WHAT THE F%#K MAN!\n You are in Debt!\n I don't care how you did cause this is embaressing.";
        }
        else if (score >= 1000) 
        {
            ramsisAnimator.SetInteger("AnimationType", 4);
            return "Golden Ramsis: Congrats Buddy You have proven to me that you are the master of Suri";
        }
            
        else if (score >= 500) {
            ramsisAnimator.SetInteger("AnimationType", 3);
            return "Golden Ramsis: Not Bad Kid!\n You still have a long way to go.";
        }
           
        else if (score >= 250) 
        {
            ramsisAnimator.SetInteger("AnimationType", 2);
            return "Golden Ramsis: That is the worst F%#king Service I have seen with my own eyes.\n Get Better.";
        }
        else 
        {
            ramsisAnimator.SetInteger("AnimationType", 1);
            return "Golden Ramsis: ARE YOU A F%#KING IDIOT SURI!\n TERRIBLE SCORE\nGET OUT!";
        }
            
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
