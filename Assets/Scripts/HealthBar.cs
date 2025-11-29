using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider; // ลิงก์ Slider ใน Inspector

    // ตั้งค่าสุขภาพสูงสุด
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    // อัปเดตค่า Health
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}