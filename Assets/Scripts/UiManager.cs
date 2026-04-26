using System.Collections;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public GameObject gameMenu;
    public GameObject gameOverMenu;
    public GameObject pauseMenu;


    private GameManager gameManager;
    public float textDisplayDelay = 0.066f;

    [SerializeField] private TextMeshProUGUI ramsisMessageText;
    [SerializeField] private TextMeshProUGUI finalScoreText;


    private void Start()
    {
        gameMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
    }

    //public void OnApplicationPause(bool pause)
    //{

    //}

    public void ChangePause(bool isPaused)
    {
        if (isPaused)
        {
            pauseMenu.SetActive(true);
            gameMenu.SetActive(false);
        }
        else
        {
            pauseMenu.SetActive(false);
            gameMenu.SetActive(true);
        }
    }

    public void GameOver()
    {
        gameMenu.SetActive(false);
        gameOverMenu.SetActive(true);
    }

    public void DisplayRamsisMessage(string message)
    {
        StartCoroutine(DisplayMessageDynamically(message, ramsisMessageText));
        StartCoroutine(DisplayMessageDynamically("Final Score: " + gameManager.score, finalScoreText));
    }

    IEnumerator DisplayMessageDynamically(string message, TextMeshProUGUI textObject)
    {
        textObject.maxVisibleCharacters = 0;
        textObject.text = message;

        for (int i = 0; i < message.Length; i++)
        {
            textObject.maxVisibleCharacters = i + 1;

            // FIX: Use Realtime so it still types out even when Time.timeScale = 0
            yield return new WaitForSecondsRealtime(textDisplayDelay);
        }

        // -1 ensures all characters are visible when the loop is done
        textObject.maxVisibleCharacters = message.Length + 1;
    }
}

