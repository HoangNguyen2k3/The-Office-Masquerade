using DG.Tweening;
using UnityEngine;

public class UI_Animation : MonoBehaviour {
    public enum Type {
        None,
        Rotation,
        SheetAnim,
        TextLargeSmal
    }
    public Type type;
    [Header("-----Rotation-----")]
    public float rotationSpeed;
    private void Start() {
        if (type == Type.TextLargeSmal) {
            transform.DOScale(1.1f, 0.5f) // scale lên 1.2 trong 0.5s
        .SetEase(Ease.InOutSine)   // mượt mà
        .SetLoops(-1, LoopType.Yoyo); // lặp vô hạn qua lại (1 ↔ 1.2)
        }
    }
    // Update is called once per frame
    void Update() {
        if (type == Type.Rotation) {
            Rotation();
        }
    }

    void Rotation() {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + rotationSpeed * Time.deltaTime);
    }
}
