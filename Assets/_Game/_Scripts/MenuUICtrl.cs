using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUICtrl : MonoBehaviour {
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

    [Header("Scene Settings")]
    public string gameSceneName = "GamePlay";

    void Start() {
        if (btnSettings != null)
            btnSettings.onClick.AddListener(OnSettingsButtonClicked);

        if (btnQuit != null)
            btnQuit.onClick.AddListener(OnQuitButtonClicked);

        if (btnCloseSettings != null)
            btnCloseSettings.onClick.AddListener(OnCloseSettingsClicked);

        // Setup Music Slider
        if (musicVolumeSlider != null) {
            musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
            // Load giá trị từ AudioManager
            if (AudioManager.Instance != null)
                musicVolumeSlider.value = AudioManager.Instance.GetMusicVolume();
        }

        // Setup SFX Slider  
        if (sfxVolumeSlider != null) {
            sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
            // Load giá trị từ AudioManager
            if (AudioManager.Instance != null)
                sfxVolumeSlider.value = AudioManager.Instance.GetSFXVolume();
        }

        ShowMainMenu();
    }

    public void OnPlayButtonClicked() {
        SceneManager.LoadScene(gameSceneName);
    }

    public void OnSettingsButtonClicked() {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    public void OnCloseSettingsClicked() {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void OnMusicVolumeChanged(float value) {
        if (AudioManager.Instance != null) {
            AudioManager.Instance.SetMusicVolume(value);
        }
    }

    public void OnSFXVolumeChanged(float value) {
        if (AudioManager.Instance != null) {
            AudioManager.Instance.SetSFXVolume(value);
        }
    }

    public void OnQuitButtonClicked() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void ShowMainMenu() {
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);

        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
