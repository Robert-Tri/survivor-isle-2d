using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private AudioManager audioManager;
    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        audioManager = FindAnyObjectByType<AudioManager>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            EnemyBullet bullet = collision.GetComponent<EnemyBullet>();
            if (bullet != null)
            {
                PlayerController player = GetComponent<PlayerController>();
                if (player != null)
                {
                    player.TakeDamage(10f); // Giảm HP của người chơi khi trúng đạn
                    Destroy(collision.gameObject); // Hủy viên đạn sau khi trúng
                }
            }
        }
        else if (collision.CompareTag("Energy"))
        {
            gameManager.AddEnergy();
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("HealHpItem"))
        {
            Destroy(collision.gameObject);
            PlayerController player = GetComponent<PlayerController>();
            if (player != null)
            {
                player.Heal(10f); 
            }
        }
        else if (collision.CompareTag("BoxBoss"))
        {
            gameManager.GameWin();
            Destroy(collision.gameObject);
        }
    }
}
