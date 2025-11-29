using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Transform shootPoint;       // จุดเกิดกระสุน
    public ProjectileBase projectile;  // รับทุกชนิดของ ProjectileBase (polymorphism)
    public float shootForce = 10f;     // ตั้งแรงยิง

    void Update()
    {
        // ถ้ากดปุ่มลูกศรขวา → ยิงขวา
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            Shoot(Vector2.right);      // เรียกยิงทิศขวา
        }

        // ถ้ากดปุ่มลูกศรซ้าย → ยิงซ้าย
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            Shoot(Vector2.left);       // เรียกยิงทิศซ้าย
        }
    }

    void Shoot(Vector2 direction)
    {
        // สร้าง ProjectBase ตัวใหม่ (จะเป็นลูกบอลเพราะเราตั้งใน Inspector)
        ProjectileBase newProjectile = Instantiate(projectile, shootPoint.position, Quaternion.identity);

        // ตั้งค่าแรงยิง
        newProjectile.Force = shootForce;

        // เรียกใช้ Launch() — Polymorphism จะตัดสินว่าใช้ของคลาสไหน
        newProjectile.Launch(direction);
    }
}