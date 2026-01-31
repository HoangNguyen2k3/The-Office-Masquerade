using UnityEngine;

public class SlowZone : MonoBehaviour {
    public float slowMultiplier = 0.4f;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
            other.GetComponent<PlayerController>().moveSpeed *= slowMultiplier;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player"))
            other.GetComponent<PlayerController>().moveSpeed /= slowMultiplier;
    }
}