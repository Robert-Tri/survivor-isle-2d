using UnityEngine;

public class HealEnemySurvivor : EnemySurvivor
{
    [SerializeField] private GameObject healObject;
    [SerializeField] protected float damage = 10f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.TakeDamage(damage);

        }
    }
    protected override void Die()
    {
        if (healObject != null)
        {
            GameObject energy = Instantiate(healObject, transform.position, Quaternion.identity);
            Destroy(energy, 5f); // Hủy đối tượng năng lượng sau 5 giây
        }
        base.Die();
    }
}
