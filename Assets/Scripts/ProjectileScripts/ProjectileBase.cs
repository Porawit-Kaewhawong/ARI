using UnityEngine;

// ======================================================
// คลาสแม่แบบของโปรเจกไทล์ทุกประเภท
// ใช้ abstract class → ต้องให้ลูกคลาส override Launch()
// ======================================================
public abstract class ProjectileBase : MonoBehaviour
{
    // Rigidbody2D เพื่อใช้ฟิสิกส์
    // ประกาศ private → Encapsulation: ซ่อนข้อมูลไม่ให้แก้ตรงๆ จากภายนอก
    private Rigidbody2D rb;

    // ความแรงของกระสุน
    // ใช้ public property → สามารถอ่าน/เขียนค่าได้อย่างควบคุม
    public float Force { get; set; }

    // ======================================
    // Awake: เรียกตอนโหลดออบเจ็กต์
    // ใช้ดึง Rigidbody2D ของตัวเองมาเก็บไว้ private
    // ======================================
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // เก็บ Rigidbody2D ไว้ใช้ภายใน
    }

    // ======================================
    // Launch: เมธอด abstract
    // ลูกคลาสต้อง override ฟังก์ชันนี้ → Polymorphism + Abstraction
    // ======================================
    public abstract void Launch(Vector2 direction);

    // ======================================
    // ApplyForce: ฟังก์ชันช่วยเหลือให้ลูกคลาสเรียกใช้
    // protected → ลูกคลาสเรียกใช้ได้ แต่ภายนอกเรียกไม่ได้
    // ใช้ Encapsulation + Abstraction
    // ======================================
    protected void ApplyForce(Vector2 direction)
    {
        // ใส่แรงยิงจริง
        rb.AddForce(direction * Force, ForceMode2D.Impulse);
    }
}