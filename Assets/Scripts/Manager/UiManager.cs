using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public GameObject gameMenu;
    public GameObject gameOverMenu;
    public GameObject gameReportMenu;
    public GameObject pauseMenu;


    private GameManager gameManager;
    public float textDisplayDelay = 0.066f;

    [SerializeField] private TextMeshProUGUI ramsisMessageText;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    private InputAction pause;

    public Button resumeButton;
    public Button restartButton;
    public Button mainMenuButton;
    public Image navBall;


    private void Start()
    {
        gameMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        gameReportMenu.SetActive(false);
        pauseMenu.SetActive(false);
        navBall.gameObject.SetActive(true);
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
        pause = InputSystem.actions.FindAction("Pause");
        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(MainMenu);
        resumeButton.onClick.AddListener(UnPause);
    }

    private void Update()
    {
        // 1. Get the input
        bool pauseValue = pause.WasPressedThisFrame();

        // 2. Only check for the button press here
        if (pauseValue)
        {
            if (!gameManager.isPaused && !gameManager.isGameOver)
            {
                gameManager.PauseGame(true);
                ChangePause(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                navBall.gameObject.SetActive(false);
            }
        }
    }

    void UnPause()
    {
        if (gameManager.isPaused && !gameManager.isGameOver)
        {
            gameManager.PauseGame(false);
            ChangePause(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Locked;
            navBall.gameObject.SetActive(true);
        }
    }

    public void ChangePause(bool isPaused)
    {
        if (isPaused)
        {
            pauseMenu.SetActive(true);
            gameMenu.SetActive(false);
            EventSystem.current.SetSelectedGameObject(resumeButton.gameObject);
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
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        navBall.gameObject.SetActive(false);
        StartCoroutine(Wait5Sec());
    }

    IEnumerator Wait5Sec()
    {
        yield return new WaitForSecondsRealtime(5);
        gameOverMenu.SetActive(false);
        gameReportMenu.SetActive(true);
    }

    public void DisplayRamsisMessage(string message)
    {
        StartCoroutine(DisplayMessageDynamically(message, ramsisMessageText));
        StartCoroutine(DisplayMessageDynamically("Final Score: " + (gameManager.money >= 0 ? gameManager.score : 0), finalScoreText));
        StartCoroutine(ReturnToMainMenu());
    }

    IEnumerator DisplayMessageDynamically(string message, TextMeshProUGUI textObject)
    {
        textObject.maxVisibleCharacters = 0;
        textObject.text = message;

        for (int i = 0; i < message.Length; i++)
        {
            textObject.maxVisibleCharacters = i + 1;
            yield return new WaitForSecondsRealtime(textDisplayDelay);
        }

        textObject.maxVisibleCharacters = message.Length + 1;
        if (textObject == finalScoreText)
        {
            yield return new WaitForSecondsRealtime(3);
            StartCoroutine(ReturnToMenu());
        }
    }

    IEnumerator ReturnToMenu()
    {
        finalScoreText.maxVisibleCharacters = 0;
        string message = "Returning to Main Menu...";
        finalScoreText.text = message;

        for (int i = 0; i < message.Length; i++)
        {
            finalScoreText.maxVisibleCharacters = i + 1;
            yield return new WaitForSecondsRealtime(textDisplayDelay);
        }

        finalScoreText.maxVisibleCharacters = message.Length + 1;
    }

    IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSecondsRealtime(10);
        MainMenu();
    }

    void LoadScene(string sceneName = null)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    void RestartGame()
    {
        LoadScene();
    }

    void MainMenu()
    {
        LoadScene("MainMenu");
    }
}

