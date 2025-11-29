using UnityEngine;

public class ExitGame : MonoBehaviour
{
    // ฟังก์ชันเรียกเมื่อกดปุ่ม Exit
    public void QuitGame()
    {
        Application.Quit();      // ปิดเกมจริง ๆ (จะทำงานใน Build)
    }
}