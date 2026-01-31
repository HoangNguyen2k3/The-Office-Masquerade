using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class IntroCutscene : MonoBehaviour {
    [Header("Story Text")]
    [TextArea(3, 5)]
    public string[] storyTexts = new string[] {
        "Trong một tòa nhà cổ kính bị lãng quên...",
        "Người ta đồn rằng có một rương kho báu được cất giấu từ thế kỷ trước.",
        "Nhiều kẻ đã cố tìm kiếm, nhưng tất cả đều thất bại...",
        "Cho đến khi \"Hắn\" xuất hiện.",
        "Một tên trộm sở hữu hai chiếc mặt nạ kỳ bí.",
        "Mặt nạ Quá Khứ - mở cánh cửa về thời xưa cũ.",
        "Mặt nạ Hiện Tại - đưa hắn trở về thực tại.",
        "Với sức mạnh du hành thời gian...",
        "Liệu hắn có thể vượt qua mọi chướng ngại để đoạt lấy kho báu?"
    };

    [Header("UI References")]
    public TextMeshProUGUI storyTextUI;        // Text hiển thị story
    public CanvasGroup textCanvasGroup;        // Để fade text
    public CanvasGroup maskPanelCanvasGroup;   // Panel chứa 2 mặt nạ
    public Image fadeOverlay;                   // Black overlay để fade

    [Header("Cinemachine Cameras")]
    public CinemachineCamera treasureVCam;     // Virtual Camera nhìn vào rương
    public CinemachineCamera playerVCam;       // Virtual Camera follow player

    [Header("Timing Settings")]
    public float typewriterSpeed = 0.05f;       // Tốc độ đánh chữ
    public float textDisplayTime = 2f;          // Thời gian hiển thị mỗi dòng
    public float cameraPanDuration = 3f;        // Thời gian chờ camera blend
    public float maskDisplayTime = 2f;          // Thời gian hiển thị mặt nạ
    public float fadeSpeed = 1f;                // Tốc độ fade

    [Header("Player Reference")]
    public PlayerController playerController;

    private bool cutsceneFinished = false;
    private bool isShowingMaskPanel = false;    // Đánh dấu đang ở phase mask panel
    private bool textPhaseCompleted = false;    // Đánh dấu đã xong phase text
    public CanvasGroup canvasAllTutor;
    void Start() {
        // Disable player control khi cutscene chạy
        if (playerController != null) {
            playerController.canMove = false;
        }

        // Ẩn các UI element ban đầu
        if (textCanvasGroup != null) textCanvasGroup.alpha = 0;
        if (maskPanelCanvasGroup != null) maskPanelCanvasGroup.alpha = 0;
        if (fadeOverlay != null) fadeOverlay.color = Color.black;

        // Bắt đầu với camera nhìn vào rương
        if (treasureVCam != null) treasureVCam.Priority = 20;
        if (playerVCam != null) playerVCam.Priority = 10;

        StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene() {
        // === PHASE 0: Fade in từ màn hình đen ===
        yield return StartCoroutine(FadeOverlay(1f, 0f));

        // === PHASE 1: Hiển thị text lore ===
        if (textCanvasGroup != null) {
            yield return StartCoroutine(FadeCanvasGroup(textCanvasGroup, 0f, 1f));
        }

        foreach (string text in storyTexts) {
            yield return StartCoroutine(TypewriterEffect(text));
            yield return new WaitForSeconds(textDisplayTime);
        }

        if (textCanvasGroup != null) {
            yield return StartCoroutine(FadeCanvasGroup(textCanvasGroup, 1f, 0f));
        }

        textPhaseCompleted = true;

        // === PHASE 2: Chuyển camera từ rương → player (dùng Cinemachine blend) ===
        SwitchToPlayerCamera();
        yield return new WaitForSeconds(cameraPanDuration);  // Chờ blend hoàn thành

        // === PHASE 3: Hiển thị 2 mặt nạ ===
        yield return StartCoroutine(ShowMaskPanel());

        // === PHASE 4: Bắt đầu gameplay ===
        EndCutscene();
    }

    IEnumerator ShowMaskPanel() {
        isShowingMaskPanel = true;

        if (maskPanelCanvasGroup != null) {
            yield return StartCoroutine(FadeCanvasGroup(maskPanelCanvasGroup, 0f, 1f));
            yield return new WaitForSeconds(maskDisplayTime);
            yield return StartCoroutine(FadeCanvasGroup(maskPanelCanvasGroup, 1f, 0f));
        }

        isShowingMaskPanel = false;
    }

    IEnumerator TypewriterEffect(string text) {
        if (storyTextUI == null) yield break;

        storyTextUI.text = "";
        foreach (char c in text) {
            storyTextUI.text += c;
            yield return new WaitForSeconds(typewriterSpeed);
        }
    }

    void SwitchToPlayerCamera() {
        // Chuyển priority để Cinemachine tự blend
        if (treasureVCam != null) treasureVCam.Priority = 10;
        if (playerVCam != null) playerVCam.Priority = 20;

        Debug.Log("Camera switching to player...");
    }

    IEnumerator FadeCanvasGroup(CanvasGroup cg, float from, float to) {
        float elapsed = 0f;
        float duration = 1f / fadeSpeed;

        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
        }
        cg.alpha = to;
    }

    IEnumerator FadeOverlay(float from, float to) {
        if (fadeOverlay == null) yield break;

        float elapsed = 0f;
        float duration = 1f / fadeSpeed;
        Color color = fadeOverlay.color;

        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(from, to, elapsed / duration);
            fadeOverlay.color = color;
            yield return null;
        }
        color.a = to;
        fadeOverlay.color = color;

        // Disable overlay khi hoàn toàn trong suốt
        if (to == 0f) fadeOverlay.gameObject.SetActive(false);
    }

    void EndCutscene() {
        cutsceneFinished = true;

        // Enable player control
        if (playerController != null) {
            playerController.canMove = true;
        }

        Debug.Log("Cutscene finished! Game started.");

        // Có thể disable script này sau khi xong
        this.enabled = false;
        GameplayManager.Ins.OpenIntroduce();
        canvasAllTutor.blocksRaycasts = false;
    }

    // Skip text phase khi click - nhưng vẫn hiện mask panel
    void Update() {
        if (cutsceneFinished) return;

        // Chỉ cho phép skip khi đang ở phase text (chưa xong text và chưa đang hiện mask)
        if (Input.GetMouseButtonDown(0) && !textPhaseCompleted && !isShowingMaskPanel) {
            SkipToMaskPanel();
        }
    }

    void SkipToMaskPanel() {
        StopAllCoroutines();

        // Ẩn text ngay lập tức
        if (textCanvasGroup != null) textCanvasGroup.alpha = 0;
        if (fadeOverlay != null) fadeOverlay.gameObject.SetActive(false);

        // Chuyển camera về player
        SwitchToPlayerCamera();

        textPhaseCompleted = true;

        // Tiếp tục với mask panel sau khi camera blend
        StartCoroutine(ContinueWithMaskPanel());
    }

    IEnumerator ContinueWithMaskPanel() {
        // Chờ camera blend xong
        yield return new WaitForSeconds(cameraPanDuration);

        // Hiển thị mask panel
        yield return StartCoroutine(ShowMaskPanel());

        // Kết thúc cutscene
        EndCutscene();
    }
}

