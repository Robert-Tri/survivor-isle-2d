using UnityEngine;

public class BoomEnemySurvivor : EnemySurvivor
{
    [SerializeField] private GameObject explosionPrefabs;
    [SerializeField] private float damage = 10f;

    private void CreateExplosion()
    {
        if (explosionPrefabs != null)
        {
            Instantiate(explosionPrefabs, transform.position, Quaternion.identity);
        }
    }

    protected override void Die()
    {
        CreateExplosion();
        base.Die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.TakeDamage(damage);
            CreateExplosion();
            Die();
        }
    }
}
