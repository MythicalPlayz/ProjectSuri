using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Button startGame;
    public Button howToPlay;
    public Button options;
    public Button credits;
    public Button exitGame;

    public GameObject howToPlayPanel;
    public GameObject optionsPanel;
    public GameObject creditsPanel;

    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider gameTimeSlider;
    public TextMeshProUGUI gameTimeValueText;
    public TextMeshProUGUI musicVolumeValueText;
    public TextMeshProUGUI sfxVolumeValueText;
    public Button backOptionsButton;

    public Button backCreditsButton;
    public Button backHowButton;

    public TextMeshProUGUI highscoreText;
    private SettingsManager settingsManager;

    private Coroutine musicCoroutine;
    private Coroutine sfxCoroutine;
    private Coroutine gameTimeCoroutine;

    private string originalMusicText;
    private string originalSFXText;
    private string originalGameTimeText;

    void Start()
    {
        settingsManager = GetComponent<SettingsManager>();
        startGame.onClick.AddListener(StartGame);
        exitGame.onClick.AddListener(ExitGame);
        options.onClick.AddListener(OpenOptions);
        credits.onClick.AddListener(OpenCredits);
        howToPlay.onClick.AddListener(OpenHowToPlay);

        originalMusicText = musicVolumeValueText.text;
        originalSFXText = sfxVolumeValueText.text;
        originalGameTimeText = gameTimeValueText.text;

        howToPlayPanel.SetActive(false);
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);

        backOptionsButton.onClick.AddListener(() => BackToMenu(optionsPanel, options));
        backCreditsButton.onClick.AddListener(() => BackToMenu(creditsPanel, credits));
        backHowButton.onClick.AddListener(() => BackToMenu(howToPlayPanel, howToPlay));
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startGame.gameObject);
        highscoreText.text = "High Score\n" + PlayerPrefs.GetInt("HighScore", 0).ToString();

        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
        gameTimeSlider.value = PlayerPrefs.GetInt("GameTime", 300);

    }

    void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    void ExitGame()
    {
        Application.Quit();
    }

    void OpenOptions()
    {
        optionsPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(musicVolumeSlider.gameObject);
        EnableMenuButton(false);
    }

    void OpenCredits()
    {
        creditsPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(backCreditsButton.gameObject);
        EnableMenuButton(false);
    }

    void BackToMenu(GameObject panel, Button returnButton)
    {
        panel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(returnButton.gameObject);
        EnableMenuButton(true);
    }


    void OpenHowToPlay()
    {
        howToPlayPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(backHowButton.gameObject);
        EnableMenuButton(false);
    }

    public void ChangeMusic(float value)
    {
        string displayed = Math.Round(value * 100).ToString() + "%";
        musicVolumeValueText.text = displayed;
        settingsManager.UpdateMusicVolume(value);

        // Stop the previous timer if the user is still dragging
        if (musicCoroutine != null) StopCoroutine(musicCoroutine);

        musicCoroutine = StartCoroutine(ReturnValue(musicVolumeValueText, originalMusicText));
    }

    public void ChangeSFX(float value)
    {
        string displayed = Math.Round(value * 100).ToString() + "%";
        sfxVolumeValueText.text = displayed;
        settingsManager.UpdateSFXVolume(value);

        if (sfxCoroutine != null) StopCoroutine(sfxCoroutine);

        sfxCoroutine = StartCoroutine(ReturnValue(sfxVolumeValueText, originalSFXText));
    }

    public void ChangeGameTime(float value)
    {
        // Cast the float to an int locally
        int intValue = Mathf.RoundToInt(value);
        string displayed = intValue.ToString() + "s";
        gameTimeValueText.text = displayed;
        settingsManager.UpdateGameTime(intValue);

        if (gameTimeCoroutine != null) StopCoroutine(gameTimeCoroutine);

        gameTimeCoroutine = StartCoroutine(ReturnValue(gameTimeValueText, originalGameTimeText));
    }

    IEnumerator ReturnValue(TextMeshProUGUI text, string message)
    {
        yield return new WaitForSeconds(1f);
        text.text = message;
    }

    void EnableMenuButton(bool enabled)
    {
        startGame.interactable = enabled;
        howToPlay.interactable = enabled;
        options.interactable = enabled;
        credits.interactable = enabled;
        exitGame.interactable = enabled;
    }
}
