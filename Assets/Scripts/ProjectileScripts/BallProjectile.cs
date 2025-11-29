using System.Threading;
using UnityEngine;

// โปรเจกไทล์แบบลูกบอล สืบทอดจาก ProjectileBase
public class BallProjectile : ProjectileBase
{
    public float lifeTime = 3f;   // เวลาที่จะทำลายตัวเอง
    public int damage = 10;       // ดาเมจของกระสุน

    void Start()
    {
        Destroy(gameObject, lifeTime);   // ทำลายตัวเองหลัง X วินาที
    }

    // OVERRIDE method จาก abstract class
    public override void Launch(Vector2 direction)
    {
        ApplyForce(direction);
    }

    // ทำดาเมจตอนชนกับมอนสเตอร์
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Monster monster = collision.gameObject.GetComponent<Monster>();

        if (monster != null)
        {
            monster.TakeDamage(damage);  // ส่งดาเมจให้มอนสเตอร์
            Destroy(gameObject);         // ทำลายกระสุนเมื่อโดนเป้าหมาย
        }
    }
}