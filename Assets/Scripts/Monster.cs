using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour
{
    [SerializeField] private int maxHealth = 50;
    private int currentHealth;

    [Header("UI")]
    public HealthBar healthBar;

    [Header("Patrol")]
    public Transform leftPoint;   // จุดซ้ายสุด
    public Transform rightPoint;  // จุดขวาสุด
    public float moveSpeed = 2f;

    private bool movingRight = true;

    private Coroutine damageCoroutine;

    void Awake()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
            healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        if (leftPoint == null || rightPoint == null) return;

        // กำหนดทิศทางและ target
        Transform targetPoint = movingRight ? rightPoint : leftPoint;
        Vector3 direction = (targetPoint.position - transform.position).normalized;

        // เคลื่อนที่
        transform.position += direction * moveSpeed * Time.deltaTime;

        // เช็คถ้าใกล้จุดปลายทาง
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            movingRight = !movingRight; // เปลี่ยนทิศทาง
            Flip(); // พลิกตัวละครตามทิศทาง
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // ระบบเลือดเดิม
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
        // ตรวจสอบว่าชนะเกมหรือยัง
        if (ScoreManager.instance.score >= 60)
        {
            if (WinManager.instance != null)
                WinManager.instance.ShowWinUI();
        }

        Destroy(gameObject); // ทำลาย Monster
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        if (healthBar != null)
            healthBar.SetHealth(currentHealth);
    }

    public void StartTakingDamageOverTime(int amount, float interval)
    {
        if (damageCoroutine == null)
            damageCoroutine = StartCoroutine(DamageOverTime(amount, interval));
    }

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