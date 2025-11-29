using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour
{
    public Transform shootPoint;
    public ProjectileBase projectile;
    public float shootForce = 10f;

    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("UI")]
    public HealthBar healthBar;

    private Coroutine damageCoroutine;

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

    // ลดเลือด Shooter
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (healthBar != null)
            healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Debug.Log("Shooter is dead!");
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

    // ==============================
    // ตรวจจับการชน Monster
    // ==============================
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Monster>() != null)
        {
            // เริ่มลดเลือดทุก 1 วิ
            if (damageCoroutine == null)
                damageCoroutine = StartCoroutine(DamageOverTime(10, 1f));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Monster>() != null)
        {
            // หยุดลดเลือดเมื่อออกจากตัว Monster
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }

    private IEnumerator DamageOverTime(int amount, float interval)
    {
        while (true)
        {
            TakeDamage(amount);
            yield return new WaitForSeconds(interval);
        }
    }
}