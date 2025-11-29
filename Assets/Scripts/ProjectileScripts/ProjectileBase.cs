using UnityEngine;

// คลาสแม่แบบของโปรเจกไทล์ทุกประเภท
public abstract class ProjectileBase : MonoBehaviour
{
    // Rigidbody2D เพื่อใช้ฟิสิกส์ (encapsulation: private)
    private Rigidbody2D rb;

    // ความแรงของกระสุน (มี public getter/setter)
    public float Force { get; set; }

    // Awake จะถูกเรียกก่อน Start
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   // เก็บ Rigidbody2D ไว้ในตัวแปร private
    }

    // เมธอด abstract ต้องให้ลูก override
    public abstract void Launch(Vector2 direction);

    // ฟังก์ชันที่ใช้โดยลูกคลาสได้ (protected)
    protected void ApplyForce(Vector2 direction)
    {
        rb.AddForce(direction * Force, ForceMode2D.Impulse);  // ใส่แรงยิงจริง
    }
}