using UnityEngine;

public class TimedTrap : MonoBehaviour {
    public GameObject visualEffect;
    public Collider2D trapCollider;
    public float onTime = 2f;
    public float offTime = 2f;
    private float timer;
    private bool isOn = true;

    void Update() {
        timer += Time.deltaTime;
        if (isOn && timer >= onTime) Toggle(false);
        else if (!isOn && timer >= offTime) Toggle(true);
    }

    void Toggle(bool state) {
        isOn = state;
        timer = 0;
        visualEffect.SetActive(state);
        trapCollider.enabled = state;
    }
}