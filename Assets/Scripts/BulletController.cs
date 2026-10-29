using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private float bulletLifeTime = 1f;
    [SerializeField] private int bulletDamage = 1;

    private void OnEnable()
    {
        // Start the coroutine to deactivate the bullet after its lifetime
        Invoke(nameof(deactivateBullet), bulletLifeTime);
    }

    private void deactivateBullet()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        
       deactivateBullet();
       //TODO: Particles
       //TODO: if bullet hits an enemy, reduce enemy health
       //TODO: if bullet hits obstacle, destroy obstacle
       //TODO: if bullet hits player, reduce player health
       
    }
}