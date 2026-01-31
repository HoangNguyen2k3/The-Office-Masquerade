using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUICtrl : MonoBehaviour
{
    [Header("UI Buttons")]
    public Button btnPlay;
    public Button btnSettings;
    public Button btnQuit;
    public Button btnCloseSettings; 

    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;

    [Header("Settings Sliders")]
    public Slider musicVolumeSlider; 
    public Slider sfxVolumeSlider;   

    [Header("Audio Sources")]
    public AudioSource backgroundMusicSource;
    public AudioSource sfxSource;

    [Header("Scene Settings")]
    public string gameSceneName = "SampleScene"; 

    void Start()
    {
        if (btnPlay != null)
            btnPlay.onClick.AddListener(OnPlayButtonClicked);
        
        if (btnSettings != null)
            btnSettings.onClick.AddListener(OnSettingsButtonClicked);
        
        if (btnQuit != null)
            btnQuit.onClick.AddListener(OnQuitButtonClicked);

        if (btnCloseSettings != null)
            btnCloseSettings.onClick.AddListener(OnCloseSettingsClicked);

        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        }

        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
        }

        ShowMainMenu();
    }

    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void OnSettingsButtonClicked()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    public void OnCloseSettingsClicked()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void OnMusicVolumeChanged(float value)
    {
        if (backgroundMusicSource != null)
            backgroundMusicSource.volume = value;
        
        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();
    }

    public void OnSFXVolumeChanged(float value)
    {
        if (sfxSource != null)
            sfxSource.volume = value;
        
        PlayerPrefs.SetFloat("SFXVolume", value);
        PlayerPrefs.Save();
    }

    public void OnQuitButtonClicked()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    void ShowMainMenu()
    {
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);
        
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
