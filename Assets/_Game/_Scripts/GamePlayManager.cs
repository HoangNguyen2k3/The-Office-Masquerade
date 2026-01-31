using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameplayManager : MonoBehaviour {
    public static GameplayManager Ins;
    public GameObject ui_choosePast;
    public GameObject ui_choosePresent;
    public GameObject ui_Win;
    public GameObject ui_Lose;
    public Tilemap tilemapPast;
    public Tilemap tilemapPresent;
    public TilemapCollider2D tileMapPast;
    public TilemapCollider2D tileMapPresent;
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
    public void OpenPastMap() {
        tilemapPast.color = new Color(1f, 1f, 1f, 1f);
        tileMapPast.enabled = true;
        tilemapPresent.color = new Color(1f, 1f, 1f, 40 / 255f);
        tileMapPresent.enabled = false;
    }
    public void OpenPresentMap() {
        tilemapPast.color = new Color(1f, 1f, 1f, 40 / 255f);
        tileMapPast.enabled = false;
        tilemapPresent.color = new Color(1f, 1f, 1f, 1f);
        tileMapPresent.enabled = true;
    }
}