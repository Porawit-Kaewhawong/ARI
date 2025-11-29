using UnityEngine;

public class WinManager : MonoBehaviour
{
    public static WinManager instance;
    public GameObject winPanel; // ลิงก์ Panel ที่จะแสดงตอนชนะเกม

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        // ปิด Panel ตอนเริ่มเกม
        if (winPanel != null)
            winPanel.SetActive(false);
    }

    // ฟังก์ชันเรียกตอนชนะเกม
    public void ShowWinUI()
    {
        if (winPanel != null)
            winPanel.SetActive(true);
    }
}