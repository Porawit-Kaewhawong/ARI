using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // ลิงก์ Slider ของ UI ใน Inspector
    public Slider slider;

    // ======================================
    // SetMaxHealth: ตั้งค่าสุขภาพสูงสุด
    // ใช้ตอนเริ่มเกมหรือรีเซ็ต Monster/Player
    // ======================================
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health; // กำหนดค่ามากที่สุดของ Slider
        slider.value = health;    // ตั้งค่าเริ่มต้นเต็ม
    }

    // ======================================
    // SetHealth: อัปเดตค่า Health ของ Slider
    // ใช้ทุกครั้งที่ TakeDamage หรือ Heal
    // ======================================
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}