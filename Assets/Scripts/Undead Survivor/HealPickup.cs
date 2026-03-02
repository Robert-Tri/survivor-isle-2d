using UnityEngine;

public class HealPickup : MonoBehaviour
{
    [SerializeField] private float healAmount = 20f; // lượng máu hồi

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Heal(healAmount);
            }
            Destroy(gameObject); // xóa heal sau khi nhặt
        }
    }
}
