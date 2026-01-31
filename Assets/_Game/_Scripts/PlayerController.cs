using UnityEngine;

public class PlayerController : MonoBehaviour {
    [Header("Movement Settings")]
    public float moveSpeed = 8f;
    public float jumpForce = 12f;
    private float horizontalInput;

    [Header("Double Jump")]
    public int maxJumps = 2; // Tối đa 2 lần nhảy
    private int jumpsRemaining;

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator anim;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;

    [Header("Mask Swap System")]
    public GameObject pastMap;
    public GameObject presentMap;
    private bool isPast = true;
    // Thêm hiệu ứng hình ảnh cho mặt nạ (tùy chọn)
    public SpriteRenderer characterSprite;
    public Color pastColor = Color.white;
    public Color presentColor = Color.cyan;

    void Start() {
        jumpsRemaining = maxJumps;
    }

    void Update() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        if (isGrounded) {
            jumpsRemaining = maxJumps; // Reset số lần nhảy khi chạm đất
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (isGrounded || jumpsRemaining > 0) {
                Jump();
            }
        }

        if (Input.GetMouseButtonDown(1)) {
            ToggleMask();
        }

        UpdateAnimations();
    }

    void FixedUpdate() {
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
    }

    void Jump() {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

        jumpsRemaining--;

        // Trigger hiệu ứng nhảy trong Animator (nếu có)
        //anim.SetTrigger("jumpTrigger");
    }

    void ToggleMask() {
        isPast = !isPast;

        // Hoán đổi Map
        pastMap.SetActive(isPast);
        presentMap.SetActive(!isPast);
        GameplayManager.Ins.ui_choosePast.SetActive(isPast);
        GameplayManager.Ins.ui_choosePresent.SetActive(!isPast);
        if (characterSprite != null) {
            characterSprite.color = isPast ? pastColor : presentColor;
        }
        Debug.Log("Swapped Mask! Current World: " + (isPast ? "Past" : "Present"));
    }

    void UpdateAnimations() {
        anim.SetBool("isRunning", horizontalInput != 0);
        anim.SetBool("isGrounded", isGrounded);
        //        anim.SetFloat("yVelocity", rb.linearVelocity.y);

        if (horizontalInput > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (horizontalInput < 0) transform.localScale = new Vector3(-1, 1, 1);
    }
    private void OnDrawGizmosSelected() {
        if (groundCheck != null) {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        }
    }
}