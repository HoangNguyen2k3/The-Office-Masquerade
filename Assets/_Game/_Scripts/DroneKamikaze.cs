using UnityEngine;

public class DroneKamikaze : MonoBehaviour {
    public float detectionRange = 6f;
    public float chargeSpeed = 10f;
    public float explosionRadius = 2f;
    public GameObject explosionVFX;

    private Transform player;
    private bool isCharging = false;
    private Vector2 startPos;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startPos = transform.position;
    }

    void Update() {
        float dist = Vector2.Distance(transform.position, player.position);

        if (dist < detectionRange && player.gameObject.activeInHierarchy)
            isCharging = true;
        else if (Vector2.Distance(transform.position, startPos) > 6f || !player.gameObject.activeInHierarchy)
            isCharging = false; // Quay về nếu quá xa hoặc player swap

        if (isCharging)
            transform.position = Vector2.MoveTowards(transform.position, player.position, chargeSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player") || isCharging && col.CompareTag("Ground"))
            Explode();
    }

    void Explode() {
        if (explosionVFX) Instantiate(explosionVFX, transform.position, Quaternion.identity);

        // Check xem Player có nằm trong bán kính nổ không
        if (Vector2.Distance(transform.position, player.position) <= explosionRadius)
            GameplayManager.Ins.Lose();

        Destroy(gameObject);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}