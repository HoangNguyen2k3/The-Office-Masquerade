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

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float musicVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    public bool bool_firstTutor = false;

    private void Awake() {
        // Khởi tạo Singleton
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        // Apply volume settings
        SetMusicVolume(musicVolume);
        SetSFXVolume(sfxVolume);

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
        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    // Điều chỉnh âm lượng nhạc nền
    public void SetMusicVolume(float volume) {
        musicVolume = volume;
        if (musicSource != null) {
            musicSource.volume = volume;
        }
    }

    // Điều chỉnh âm lượng SFX
    public void SetSFXVolume(float volume) {
        sfxVolume = volume;
        if (sfxSource != null) {
            sfxSource.volume = volume;
        }
    }

    // Lấy volume hiện tại
    public float GetMusicVolume() => musicVolume;
    public float GetSFXVolume() => sfxVolume;
}