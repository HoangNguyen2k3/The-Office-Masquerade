using UnityEngine;

public class Pendulum : MonoBehaviour {
    public float angle = 45f;
    public float speed = 2f;

    void Update() {
        float rotationZ = Mathf.Sin(Time.time * speed) * angle;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);
    }
}