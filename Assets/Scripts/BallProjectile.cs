using UnityEngine;

// โปรเจกไทล์แบบลูกบอล สืบทอดจาก ProjectileBase
public class BallProjectile : ProjectileBase
{
    // เวลาที่ลูกบอลจะถูกทำลาย (ตั้งค่าใน Inspector)
    public float lifeTime = 1.5f;

    void Start()
    {
        // ทำลายตัวเองหลังจากผ่านไป lifeTime วินาที
        Destroy(gameObject, lifeTime);
    }

    // OVERRIDE method จาก abstract class
    public override void Launch(Vector2 direction)
    {
        // ใช้ฟังก์ชัน ApplyForce ของคลาสแม่
        ApplyForce(direction);  // ส่งทิศทางเข้าไป
    }
}