using UnityEngine;

public class WindTrap : MonoBehaviour {
    public Vector2 windForce;

    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            other.GetComponent<Rigidbody2D>().AddForce(windForce);
        }
    }
}