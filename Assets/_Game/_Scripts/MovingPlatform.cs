using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    public Vector3 targetOffset;
    public float speed = 2f;
    private Vector3 startPos;
    private Vector3 endPos;

    void Start() {
        startPos = transform.position;
        endPos = startPos + targetOffset;
    }

    void Update() {
        float time = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(startPos, endPos, time);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.transform.SetParent(null);
        }
    }
}