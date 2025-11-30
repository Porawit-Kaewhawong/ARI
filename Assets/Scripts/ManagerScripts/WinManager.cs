using UnityEngine;

public class WinManager : MonoBehaviour
{
    // ทำเป็น Singleton เพื่อให้เรียกใช้ที่ไหนก็ได้ในฉาก (OOP: Encapsulation + Singleton pattern)
    public static WinManager instance;

    // อ้างอิงไปยัง Win Panel ที่จะเปิดตอนชนะเกม
    public GameObject winPanel; // ลิงก์ Panel ที่จะแสดงตอนชนะเกม


    // ======================================
    // Awake: เรียกเมื่อเริ่มโหลดออบเจ็กต์
    // ทำระบบ Singleton ป้องกันการมี WinManager ซ้ำ
    // ======================================
    void Awake()
    {
        // ถ้ายังไม่มี instance → ให้ตัวนี้เป็นตัวหลัก
        if (instance == null) instance = this;
        else Destroy(gameObject); // ถ้ามีซ้ำ → ลบทิ้งเพื่อไม่ให้ซ้อนกัน

        // ปิด Panel ตอนเริ่มเกม เพื่อไม่ให้แสดงทันที
        if (winPanel != null)
            winPanel.SetActive(false);
    }


    // ======================================
    // ShowWinUI:
    // ฟังก์ชันเปิด UI เมื่อผู้เล่นชนะเกม
    // สามารถเรียกจากที่ไหนก็ได้ เช่น ศัตรูตายหมด, เก็บไอเท็มครบ
    // ======================================
    public void ShowWinUI()
    {
        if (winPanel != null)
            winPanel.SetActive(true);  // แสดงหน้าชนะเกม
    }
}