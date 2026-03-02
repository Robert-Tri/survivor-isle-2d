using UnityEngine;

public class EnergyEnemySurvivor : EnemySurvivor
{
    [SerializeField] private GameObject energyObject;
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
        if (energyObject != null) 
        {
            GameObject energy = Instantiate(energyObject, transform.position, Quaternion.identity);
            Destroy(energy, 5f); // Hủy đối tượng năng lượng sau 5 giây
        }
        base.Die();
    }


}
