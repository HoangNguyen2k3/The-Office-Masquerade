using UnityEngine;

public class SawMover : MonoBehaviour {
    [Header("Movement")]
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 2f;

    [Header("Rotation")]
    public float rotateSpeed = 360f; // độ / giây

    Vector3 target;

    void Start() {
        target = pointB.position;
    }

    void Update() {
        // Xoay
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

        // Di chuyển qua lại
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.05f) {
            target = (target == pointA.position) ? pointB.position : pointA.position;
        }
    }
}
