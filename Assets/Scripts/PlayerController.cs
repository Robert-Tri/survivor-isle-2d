using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum MovementMode { TopDown, Platformer }
public class PlayerController : MonoBehaviour
{
    public string currentScene;

    public MovementMode movementMode = MovementMode.TopDown;
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float jumpForce = 15f;      
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isGrounded;
    private GameManager gameManager;
    private AudioManager audioManager;
    [SerializeField] protected float maxHp = 100f;
    protected float currentHp;
    [SerializeField] private Image hpBar;

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene().name;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        gameManager = FindAnyObjectByType<GameManager>();
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    void Start()
    {
        currentHp = maxHp;

    }

    void Update()
    {
        if (gameManager != null)
        {
            if (gameManager.IsGameOver() || gameManager.IsGameWin())
            {
                return; // Không xử lý nếu game đã kết thúc
            } 
        }

        switch (movementMode)
        {
            case MovementMode.TopDown:
                HandleTopDownMovement();
                UpdateHpBar();
                break;
            case MovementMode.Platformer:
                HandlePlatformerMovement();
                HandleJump();
                UpdateAnimation();
                break;
        }
    }

    private void HandleTopDownMovement() 
    { 
        Vector2 playerInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb.linearVelocity = playerInput.normalized * moveSpeed;
        if (playerInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (playerInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        if(playerInput != Vector2.zero)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }

    private void HandlePlatformerMovement()
    {
        float move = Input.GetAxis("Horizontal"); // A/D hoặc phím mũi tên
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

        if (move > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (move < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void HandleJump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            audioManager.PlayJumpSound();
        }
    }

    private void UpdateAnimation()
    {
        bool isRunning = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        bool isJumping = !isGrounded;
        animator.SetBool("IsRunning", isRunning);
        animator.SetBool("IsJumping", isJumping);
    }

    public void SetMovementMode(MovementMode mode)
    {
        movementMode = mode;
        if (movementMode == MovementMode.Platformer)
        {
            rb.gravityScale = 1; // Bật trọng lực cho chế độ Platformer
        }
        else
        {
            rb.gravityScale = 0; // Tắt trọng lực cho chế độ TopDown
        }
    }

    public virtual void TakeDamage(float damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
        UpdateHpBar();
        if (currentHp <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        if (gameManager != null)
        {
            if (currentScene == "MarioTrialsScene")
            {
                Destroy(gameObject);
            }
            else if (currentScene == "UndeadSurvivorScene")
            {
                gameManager.GameOver();
                Destroy(gameObject);
            }
        }
    }

    protected void UpdateHpBar()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = currentHp / maxHp;
        }
    }

    public IEnumerator SpeedBoost(float multiplier, float time)
    {
        moveSpeed *= multiplier; // tăng tốc
        yield return new WaitForSeconds(time);
        moveSpeed /= multiplier; // trả về tốc độ ban đầu
    }

    public void Heal(float amount)
    {
        currentHp += amount;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp); // không vượt quá maxHp
        UpdateHpBar(); // cập nhật thanh máu UI
    }

}
