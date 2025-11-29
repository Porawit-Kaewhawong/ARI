using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour
{
    [SerializeField] private int maxHealth = 50;
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

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (healthBar != null)
            healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            ScoreManager.instance.AddScore(10);
            Die();
        }
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

    // เริ่มลดเลือดทุกวิ
    public void StartTakingDamageOverTime(int amount, float interval)
    {
        if (damageCoroutine == null)
            damageCoroutine = StartCoroutine(DamageOverTime(amount, interval));
    }

    // หยุดลดเลือด
    public void StopTakingDamageOverTime()
    {
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
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