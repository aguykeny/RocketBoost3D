using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    private int currentHealth;

    private void OnEnable()
    {
        currentHealth = enemyData.health;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle enemy death
        gameObject.SetActive(false);
    }
}
