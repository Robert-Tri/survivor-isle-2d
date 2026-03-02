using UnityEngine;

public class BasicEnemySurvivor : EnemySurvivor
{
    [SerializeField] protected float damageHp = 5f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.TakeDamage(damageHp);

        }
    }

}
