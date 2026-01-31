using UnityEngine;

public class GuardAI : MonoBehaviour {
    [Header("Movement")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public Transform[] waypoints;
    public float leashRange = 8f; // Khoảng cách tối đa so với điểm xuất phát

    [Header("Detection")]
    public float detectionRange = 5f;

    private Vector2 startPosition;
    private int currentWaypoint;
    private Transform player;
    private bool isChasing = false;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startPosition = transform.position;
    }

    void Update() {
        // 1. Kiểm tra khoảng cách
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        float distFromStart = Vector2.Distance(transform.position, startPosition);

        // 2. Điều kiện đuổi: Gần player + Player đang ở map này + Chưa đi quá xa nhà
        if (distToPlayer < detectionRange && player.gameObject.activeInHierarchy && distFromStart < leashRange)
            isChasing = true;
        else
            isChasing = false;

        if (isChasing) Chase();
        else Patrol();
    }

    void Patrol() {
        if (waypoints.Length == 0) return;
        Transform target = waypoints[currentWaypoint];
        transform.position = Vector2.MoveTowards(transform.position, target.position, patrolSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.2f)
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;

        Flip(target.position.x - transform.position.x);
    }

    void Chase() {
        Vector2 targetPos = new Vector2(player.position.x, transform.position.y); // Chỉ đuổi trên sàn
        transform.position = Vector2.MoveTowards(transform.position, targetPos, chaseSpeed * Time.deltaTime);
        Flip(player.position.x - transform.position.x);
    }

    void Flip(float direction) {
        if (direction > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (direction < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Player")) GameplayManager.Ins.Lose();
    }

    void OnDrawGizmos() {
        // Vẽ tầm nhìn (Đỏ) và phạm vi giới hạn (Vàng)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Application.isPlaying ? (Vector3)startPosition : transform.position, leashRange);
    }
}