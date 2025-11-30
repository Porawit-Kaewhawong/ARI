using UnityEngine;
using TMPro; // สำหรับ TextMeshPro UI

public class ScoreManager : MonoBehaviour
{
    // ทำเป็น Singleton → เรียกใช้ได้จากทุกที่ในฉาก
    public static ScoreManager instance;

    // คะแนนปัจจุบัน
    public int score = 0;

    // ลิงก์ UI Text ใน Inspector เพื่ออัปเดตคะแนนบนหน้าจอ
    public TMP_Text scoreText;

    // ======================================
    // Awake: เรียกเมื่อโหลดออบเจ็กต์
    // ตั้งค่า Singleton ป้องกันการซ้ำของ ScoreManager
    // ======================================
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject); // ถ้ามี ScoreManager ซ้ำ → ลบ
    }

    // ======================================
    // Start: เรียกตอนเริ่มเกม
    // อัปเดต UI ครั้งแรกเพื่อให้ตรงกับค่า score
    // ======================================
    private void Start()
    {
        UpdateScoreUI();
    }

    // ======================================
    // AddScore: เพิ่มคะแนน
    // เรียกจากที่ไหนก็ได้ เช่น ศัตรูตาย → score เพิ่มขึ้น
    // ======================================
    public void AddScore(int amount)
    {
        score += amount;      // เพิ่มคะแนน
        UpdateScoreUI();      // อัปเดต UI ให้ทันที
    }

    // ======================================
    // UpdateScoreUI: อัปเดตข้อความบนหน้าจอ
    // ซ่อนรายละเอียดการแสดง UI → Abstraction
    // ======================================
    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }
}