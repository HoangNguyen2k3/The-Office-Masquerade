using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour {
    public static GameplayManager Ins;
    public GameObject ui_choosePast;
    public GameObject ui_choosePresent;
    public GameObject ui_Win;
    public GameObject ui_Lose;
    private void Awake() {
        if (Ins == null) Ins = this;
        else Destroy(gameObject);
    }

    public void Lose() {
        Debug.Log("Game Over!");
        ui_Lose.SetActive(true);
        // Có thể thêm hiệu ứng Slow motion hoặc bảng thông báo ở đây
        //Invoke("RestartLevel", 0.5f);

    }

    public void Win() {
        Debug.Log("Mission Accomplished!");
        ui_Win.SetActive(true);
        // Chuyển sang scene tiếp theo hoặc hiện bảng thắng
    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu() {
        SceneManager.LoadScene("Menu");
    }
}