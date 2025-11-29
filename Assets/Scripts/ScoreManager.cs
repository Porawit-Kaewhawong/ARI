using UnityEngine;
using TMPro; // สำหรับ TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int score = 0;
    public TMP_Text scoreText; // ลิงก์ UI Text ใน Inspector

    private void Awake()
    {
        // Singleton ง่าย ๆ
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateScoreUI();
    }

    // ฟังก์ชันเพิ่มคะแนน
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    // อัปเดตข้อความ UI
    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }
}