using System.Collections;
using UnityEngine;

public enum Character
{
    Player,
    Enemy
}

public class Health : MonoBehaviour
{
    public Character character;
    public float maxHealth;
    public bool isExplosive;
    public GameObject explosionPrefab;
    public float destroyWaitTime = 0f;
    public HealthBar healthBar;

    [SerializeField] private float health = 20f;

    private bool isDead;

    private void Start()
    {
        health = maxHealth;
        if(healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    public void TakeDamage(float amt)
    {
        health -= amt;

        if(healthBar != null)
        {
            healthBar.SetHealth(health);
        }

        if (health <= 0f)
        {
            Die();
        }

    }

    void Die()
    {
        if (isExplosive)
        {
            if (explosionPrefab != null)
            {
                GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                if(explosion != null) { Destroy(explosion, 2f); }
            }
        }

        isDead = true;

        if(character == Character.Enemy)
        {
            Destroy(this.gameObject, destroyWaitTime);
        }
        
    }
    
    
    public float GetHealth()
    {
        return health;
    }

    public bool IsDead()
    {
        return isDead;
    }
}
