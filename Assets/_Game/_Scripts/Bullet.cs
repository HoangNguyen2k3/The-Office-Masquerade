using UnityEngine;

public class Bullet : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            GameplayManager.Ins.Lose();
            Destroy(gameObject);
        }
        if (other.CompareTag("Ground")) Destroy(gameObject); // Chạm tường thì biến mất
    }
}
