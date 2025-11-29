using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private int maxHealth = 50; // เลือดสูงสุด
    private int currentHealth;

    [Header("UI")]
    public HealthBar healthBar; // ลิงก์ Health Bar ของมอนสเตอร์นี้

    void Awake()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
            healthBar.SetMaxHealth(maxHealth);
    }

    // ฟังก์ชันรับดาเมจ
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (healthBar != null)
            healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            ScoreManager.instance.AddScore(10); // เพิ่มคะแนนเมื่อมอนสเตอร์ตาย
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    // ฟังก์ชันเพิ่มเลือด ถ้าต้องการ
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        if (healthBar != null)
            healthBar.SetHealth(currentHealth);
    }
}