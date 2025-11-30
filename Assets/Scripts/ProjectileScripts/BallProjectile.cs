using System.Threading;
using UnityEngine;

// ======================================================
// โปรเจกไทล์แบบลูกบอล (BallProjectile)
// สืบทอดจาก ProjectileBase → Inheritance
// ======================================================
public class BallProjectile : ProjectileBase
{
    // ======================================
    // เวลาที่กระสุนจะทำลายตัวเอง
    // ======================================
    public float lifeTime = 3f;

    // ดาเมจของกระสุน
    public int damage = 10;

    // ======================================
    // Start: เรียกตอนเริ่มเกม
    // ตั้งให้กระสุนทำลายตัวเองอัตโนมัติหลัง lifeTime วินาที
    // ======================================
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // ======================================
    // OVERRIDE: Launch จาก ProjectileBase (abstract)
    // ใช้ ApplyForce() ที่เป็น protected ของคลาสแม่
    // → Polymorphism + Inheritance + Abstraction
    // ======================================
    public override void Launch(Vector2 direction)
    {
        ApplyForce(direction);
    }

    // ======================================
    // OnCollisionEnter2D: ตรวจจับการชนกับ Monster
    // ทำดาเมจให้มอนสเตอร์และทำลายกระสุน
    // ======================================
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // เช็คว่าชน Monster หรือไม่
        Monster monster = collision.gameObject.GetComponent<Monster>();

        if (monster != null)
        {
            monster.TakeDamage(damage);  // ส่งดาเมจให้ Monster
            Destroy(gameObject);         // ทำลายกระสุนเมื่อโดนเป้าหมาย
        }
    }
}