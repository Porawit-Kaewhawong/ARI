using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Transform shootPoint;
    public ProjectileBase projectile;
    public float shootForce = 10f;

    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("UI")]
    public HealthBar healthBar; // ลิงก์ Health Bar ของตัวละครนี้

    void Awake()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
            healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            Shoot(Vector2.right);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Shoot(Vector2.left);
    }

    void Shoot(Vector2 direction)
    {
        ProjectileBase newProjectile = Instantiate(projectile, shootPoint.position, Quaternion.identity);
        newProjectile.Force = shootForce;
        newProjectile.Launch(direction);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (healthBar != null)
            healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        if (healthBar != null)
            healthBar.SetHealth(currentHealth);
    }
}