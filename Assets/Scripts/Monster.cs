using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour
{
    // ================================
    // ค่าเลือดสูงสุด → private ใช้ Encapsulation
    // ================================
    [SerializeField] private int maxHealth = 50;

    // เลือดปัจจุบัน → private
    private int currentHealth;

    [Header("UI")]
    // แถบเลือด Monster
    public HealthBar healthBar;

    [Header("Patrol")]
    // จุดซ้ายสุดและขวาสุดของการเดิน
    public Transform leftPoint;
    public Transform rightPoint;

    // ความเร็วในการเดิน
    public float moveSpeed = 2f;

    // ทิศทางเดินปัจจุบัน (true = ขวา, false = ซ้าย)
    private bool movingRight = true;

    // Coroutine สำหรับระบบดาเมจต่อเนื่อง
    private Coroutine damageCoroutine;


    // ======================================
    // Awake: เรียกเมื่อโหลดออบเจ็กต์
    // ตั้งค่าเลือดเริ่มต้นและ UI
    // ======================================
    void Awake()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
            healthBar.SetMaxHealth(maxHealth);
    }

    // ======================================
    // Update: เรียกทุกเฟรม
    // ทำการ Patrol
    // ======================================
    void Update()
    {
        Patrol();
    }

    // ======================================
    // Patrol: เดินไปมาระหว่าง leftPoint และ rightPoint
    // ======================================
    void Patrol()
    {
        if (leftPoint == null || rightPoint == null) return;

        // เลือก target ตามทิศทาง
        Transform targetPoint = movingRight ? rightPoint : leftPoint;
        Vector3 direction = (targetPoint.position - transform.position).normalized;

        // เคลื่อนที่
        transform.position += direction * moveSpeed * Time.deltaTime;

        // ถ้าใกล้จุดปลายทาง → เปลี่ยนทิศทาง
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            movingRight = !movingRight;
            Flip(); // พลิกตัวละครตามทิศ
        }
    }

    // ======================================
    // Flip: พลิกตัวละครตามทิศทาง
    // ======================================
    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // ======================================
    // TakeDamage: ลดเลือด Monster
    // ใช้ Encapsulation → ป้องกันแก้ currentHealth จากข้างนอกโดยตรง
    // ======================================
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        // อัปเดต UI
        if (healthBar != null)
            healthBar.SetHealth(currentHealth);

        // ถ้าเลือดหมด → เพิ่มคะแนนผู้เล่นและตาย
        if (currentHealth <= 0)
        {
            ScoreManager.instance.AddScore(10);
            Die();
        }
    }

    // ======================================
    // Die: ทำลาย Monster และตรวจสอบว่าผู้เล่นชนะเกมหรือยัง
    // ======================================
    private void Die()
    {
        // ถ้าคะแนน >= 60 → แสดง Win UI
        if (ScoreManager.instance.score >= 60)
        {
            if (WinManager.instance != null)
                WinManager.instance.ShowWinUI();
        }

        Destroy(gameObject); // ทำลาย Monster
    }

    // ======================================
    // Heal: ฟื้นฟูเลือด
    // ใช้ Encapsulation → ป้องกันเลือดเกิน maxHealth
    // ======================================
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        if (healthBar != null)
            healthBar.SetHealth(currentHealth);
    }

    // ======================================
    // StartTakingDamageOverTime: เริ่มลดเลือดทุก interval วินาที
    // ใช้ Coroutine
    // ======================================
    public void StartTakingDamageOverTime(int amount, float interval)
    {
        if (damageCoroutine == null)
            damageCoroutine = StartCoroutine(DamageOverTime(amount, interval));
    }

    // ======================================
    // StopTakingDamageOverTime: หยุดลดเลือดต่อเนื่อง
    // ======================================
    public void StopTakingDamageOverTime()
    {
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    // ======================================
    // DamageOverTime: Coroutine ลดเลือดทุก interval วินาที
    // ======================================
    private IEnumerator DamageOverTime(int amount, float interval)
    {
        while (true)
        {
            TakeDamage(amount);
            yield return new WaitForSeconds(interval);
        }
    }
}