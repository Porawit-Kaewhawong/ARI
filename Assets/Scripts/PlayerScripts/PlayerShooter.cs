using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour
{
    // จุดที่กระสุนถูกสร้างขึ้น
    public Transform shootPoint;

    // กระสุนที่ใช้ (เป็น Base Class → ใช้หลัก OOP: Polymorphism + Abstraction)
    public ProjectileBase projectile;

    // แรงยิงกระสุน
    public float shootForce = 10f;

    [Header("Health Settings")]
    // ค่าเลือดสูงสุด
    public int maxHealth = 100;

    // เลือดปัจจุบัน (ซ่อนเป็น private → OOP: Encapsulation)
    private int currentHealth;

    [Header("UI")]
    // UI แสดงเลือด
    public HealthBar healthBar;

    // ไว้เก็บ Coroutine การโดนดาเมจซ้ำ
    private Coroutine damageCoroutine;


    // ======================================
    // Awake: เรียกตอนเริ่ม ก่อน Start
    // ใช้ตั้งค่าเริ่มต้นของ Shooter
    // ======================================
    void Awake()
    {
        currentHealth = maxHealth; // ตั้งค่าเลือดตอนเริ่ม

        // ตั้งค่า UI แสดงเลือด
        if (healthBar != null)
            healthBar.SetMaxHealth(maxHealth);
    }


    // ======================================
    // Update: เช็คปุ่มกดทุกเฟรม
    // ย้ายซ้าย/ขวาเพื่อยิงกระสุน
    // ======================================
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            Shoot(Vector2.right);   // ยิงไปทางขวา

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Shoot(Vector2.left);    // ยิงไปทางซ้าย
    }


    // ======================================
    // ฟังก์ชันยิงกระสุน
    // ใช้ Instantiate สร้างกระสุนจาก prefab
    // ใช้หลัก OOP (Polymorphism): ProjectileBase สามารถเป็นกระสุนหลายแบบได้
    // ======================================
    void Shoot(Vector2 direction)
    {
        ProjectileBase newProjectile = Instantiate(projectile, shootPoint.position, Quaternion.identity);
        newProjectile.Force = shootForce; // ส่งค่าแรงยิง
        newProjectile.Launch(direction);  // ยิงกระสุนตามทิศ
    }


    // ======================================
    // TakeDamage: ลดเลือดของ Shooter
    // ใช้ Encapsulation เพราะ currentHealth เป็น private
    // ======================================
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        // อัพเดท UI เลือด
        if (healthBar != null)
            healthBar.SetHealth(currentHealth);

        // ถ้าเลือดหมด ให้ตาย
        if (currentHealth <= 0)
            Die();
    }


    // ======================================
    // Die: ทำงานเมื่อ Shooter ตาย
    // ลบตัว Player ออกจากฉาก
    // ======================================
    private void Die()
    {
        Debug.Log("Shooter is dead!");
        Destroy(gameObject); // ทำลายตัวเกม
    }


    // ======================================
    // Heal: ฟื้นฟูเลือด
    // ใช้ Encapsulation ป้องกันเลือดเกิน maxHealth
    // ======================================
    public void Heal(int amount)
    {
        currentHealth += amount;

        // กันไม่ให้เลือดเกิน max
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        // อัพเดท UI
        if (healthBar != null)
            healthBar.SetHealth(currentHealth);
    }


    // ======================================
    // OnTriggerEnter2D:
    // ถ้าเข้าไปชน Monster → เริ่มลดเลือดทุก 1 วินาที
    // ======================================
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // เช็คว่าเป็น Monster หรือไม่
        if (collision.GetComponent<Monster>() != null)
        {
            // เริ่มลดเลือดต่อเนื่อง (ถ้ายังไม่เริ่มมาก่อน)
            if (damageCoroutine == null)
                damageCoroutine = StartCoroutine(DamageOverTime(10, 1f));
        }
    }


    // ======================================
    // OnTriggerExit2D:
    // เมื่อออกจาก Monster → หยุดการลดเลือด
    // ======================================
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Monster>() != null)
        {
            // หยุดลดเลือดต่อเนื่อง
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }


    // ======================================
    // DamageOverTime:
    // ลดเลือดทุกจำนวนวินาทีด้วย Coroutine
    // ======================================
    private IEnumerator DamageOverTime(int amount, float interval)
    {
        while (true)
        {
            TakeDamage(amount);          // ลดเลือด
            yield return new WaitForSeconds(interval); // รอเป็นช่วงๆ
        }
    }
}