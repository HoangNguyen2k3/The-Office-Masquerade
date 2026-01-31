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

    [Header("Coyote Time & Jump Buffer")]
    public float coyoteTime = 0.15f;      // Thời gian cho phép nhảy sau khi rời platform
    public float jumpBufferTime = 0.15f;  // Thời gian buffer input nhảy
    private float coyoteTimeCounter;       // Đếm ngược coyote time
    private float jumpBufferCounter;       // Đếm ngược jump buffer

    [Header("Mask Swap System")]
    private bool isPast = true;
    // Thêm hiệu ứng hình ảnh cho mặt nạ (tùy chọn)
    public GameObject maskInPlayerPast;
    public GameObject maskInPresentPlayer;

    void Start() {
        jumpsRemaining = maxJumps;
        GameplayManager.Ins.OpenPastMap();
    }

    void Update() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        // Coyote Time Logic
        if (isGrounded) {
            coyoteTimeCounter = coyoteTime;  // Reset coyote time khi đang đứng trên mặt đất
            jumpsRemaining = maxJumps;       // Reset số lần nhảy khi chạm đất
        }
        else {
            coyoteTimeCounter -= Time.deltaTime;  // Đếm ngược khi rời khỏi mặt đất
        }

        // Jump Buffer Logic - ghi nhận input nhảy
        if (Input.GetKeyDown(KeyCode.Space)) {
            jumpBufferCounter = jumpBufferTime;
        }
        else {
            jumpBufferCounter -= Time.deltaTime;
        }

        // Thực hiện nhảy nếu: có buffer input VÀ (còn coyote time HOẶC còn lần nhảy)
        if (jumpBufferCounter > 0f) {
            if (coyoteTimeCounter > 0f || jumpsRemaining > 0) {
                Jump();
                jumpBufferCounter = 0f;  // Reset buffer sau khi nhảy
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
        coyoteTimeCounter = 0f;  // Reset coyote time sau khi nhảy để tránh exploit

        // Trigger hiệu ứng nhảy trong Animator (nếu có)
        //anim.SetTrigger("jumpTrigger");
    }

    void ToggleMask() {
        isPast = !isPast;

        // Hoán đổi Map
        if (isPast) {
            GameplayManager.Ins.OpenPastMap();
        }
        else {
            GameplayManager.Ins.OpenPresentMap();
        }
        GameplayManager.Ins.ui_choosePast.SetActive(isPast);
        GameplayManager.Ins.ui_choosePresent.SetActive(!isPast);
        maskInPlayerPast.SetActive(isPast);
        maskInPresentPlayer.SetActive(!isPast);
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