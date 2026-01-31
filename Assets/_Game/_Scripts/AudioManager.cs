using UnityEngine;

public class AudioManager : MonoBehaviour {
    // Singleton để gọi từ mọi nơi
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Default Clips")]
    public AudioClip backgroundMusic;
    public AudioClip swapMaskSFX;
    public AudioClip winningSFX;
    public AudioClip loseSFX;
    public AudioClip runSFX;
    public AudioClip jumpSFX;

    private void Awake() {
        // Khởi tạo Singleton
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ âm thanh không bị ngắt khi đổi cảnh
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        // Phát nhạc nền ngay khi game bắt đầu
        if (backgroundMusic != null) {
            PlayMusic(backgroundMusic);
        }
    }

    // Hàm phát nhạc nền (Looping)
    public void PlayMusic(AudioClip clip) {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    // Hàm phát hiệu ứng âm thanh (Play One Shot)
    public void PlaySFX(AudioClip clip) {
        sfxSource.PlayOneShot(clip);
    }
}