using UnityEngine;

public class EnergyPickup : MonoBehaviour
{
    [SerializeField] private float speedBoost = 2f;   // hệ số tăng tốc
    [SerializeField] private float duration = 5f;     // thời gian buff

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                StartCoroutine(player.SpeedBoost(speedBoost, duration));
            }
            Destroy(gameObject); // xóa energy sau khi nhặt
        }
    }
}
