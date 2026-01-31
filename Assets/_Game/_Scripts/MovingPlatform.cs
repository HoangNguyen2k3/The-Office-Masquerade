using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    public Vector3 targetOffset; // Khoảng cách nâng lên (VD: Y = 3)
    public float speed = 2f;
    private Vector3 startPos;
    private Vector3 endPos;

    void Start() {
        startPos = transform.position;
        endPos = startPos + targetOffset;
    }

    void Update() {
        // Di chuyển sàn bằng hàm PingPong
        float time = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(startPos, endPos, time);
    }

    // --- LOGIC QUAN TRỌNG: BÁM DÍNH NHÂN VẬT ---
    private void OnCollisionEnter2D(Collision2D collision) {
        // Nếu Player chạm vào sàn, biến Player thành con của sàn
        if (collision.gameObject.CompareTag("Player")) {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        // Khi Player nhảy ra khỏi sàn, hủy mối quan hệ cha con
        if (collision.gameObject.CompareTag("Player")) {
            collision.transform.SetParent(null);
        }
    }
}