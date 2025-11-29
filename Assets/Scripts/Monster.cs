using UnityEngine;

public class Monster : MonoBehaviour
{
    // เลือดของมอนสเตอร์ (Encapsulation แบบง่าย)
    [SerializeField] private int health = 50;

    // ฟังก์ชันรับดาเมจ
    public void TakeDamage(int amount)
    {
        health -= amount;                 // ลดเลือดตามดาเมจที่รับ
        Debug.Log("Monster HP: " + health);

        if (health <= 0)
        {
            Die();                        // ถ้าเลือดหมดก็ทำลายตัวเอง
        }
    }

    private void Die()
    {
        Destroy(gameObject);              // ทำลายมอนสเตอร์
    }
}