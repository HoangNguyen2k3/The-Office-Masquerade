using UnityEngine;

public class DroneShooter : MonoBehaviour {
    public float detectionRange = 7f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 2f;

    private float nextFireTime;
    private Transform player;

    void Start() { player = GameObject.FindGameObjectWithTag("Player").transform; }

    void Update() {
        float dist = Vector2.Distance(transform.position, player.position);

        if (dist < detectionRange && player.gameObject.activeInHierarchy) {
            if (Time.time >= nextFireTime) {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void Shoot() {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Vector2 dir = (player.position - firePoint.position).normalized;
        bullet.GetComponent<Rigidbody2D>().linearVelocity = dir * 8f;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}